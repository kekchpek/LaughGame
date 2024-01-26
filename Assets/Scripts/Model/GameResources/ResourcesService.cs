using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LaughGame.GameResources
{
    public class ResourcesService : IResourcesService
    {
        private readonly IResourcesMutableModel _model;

        public ResourcesService(
            IResourcesMutableModel model)
        {
            _model = model;
        }
        
        public bool TryToSpend(IEnumerable<(ResourceId resourceId, float amount)> resources)
        {
            var idsSet = HashSetPool<ResourceId>.Get();
            var resList = ListPool<(ResourceId id, float amount)>.Get(); // to not enumerate twice.
            try
            {
                foreach (var resData in resources)
                {
                    if (idsSet.Contains(resData.resourceId))
                        throw new ArgumentException("Resources to spend should be unique. " +
                                                    $"There is an attempt to spend {resData.resourceId} twice");
                    idsSet.Add(resData.resourceId);
                    var val = _model.GetResource(resData.resourceId).Value;
                    if (val < resData.amount)
                        return false;
                    resList.Add(resData);
                }

                foreach (var resData in resList)
                {
                    _model.SetResource(resData.id, _model.GetResource(resData.id).Value - resData.amount);
                }

                return true;
            }
            finally
            {
                ListPool<(ResourceId id, float amount)>.Release(resList);
                HashSetPool<ResourceId>.Release(idsSet);
            }
        }

        public void Reduce(ResourceId resourceId, float amount)
        {
            var val = _model.GetResource(resourceId).Value;
            _model.SetResource(resourceId, Mathf.Max(val - amount, 0f));
        }

        public void Add(ResourceId resourceId, float amount)
        {
            var val = _model.GetResource(resourceId).Value;
            _model.SetResource(resourceId, val + amount);
        }
    }
}