using System;
using UnityEngine;

namespace Shared
{

    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour
    {

        private enum Orientation
        {
            Horizontal,
            Vertical
        }

        [SerializeField] private Orientation _orientation;
        
        [SerializeField] private float _value;
        [SerializeField] private float _maxValue;

        [SerializeField] private RectTransform _foregroundRect;

        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(value, 0f, _maxValue);
                UpdateState();
            }
        }
        
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = Mathf.Max(value, 0f);
                UpdateState();
            }
        }

        private void Update()
        {
            _maxValue = Mathf.Max(_maxValue, 0f);
            _value = Mathf.Clamp(_value, 0f, _maxValue);
            UpdateState();
        }

        private void UpdateState()
        {
            if (_foregroundRect)
            {
                var height = _value / _maxValue;

                _foregroundRect.anchorMax = _orientation switch
                {
                    Orientation.Horizontal => new Vector2(height, 1f),
                    Orientation.Vertical => new Vector2(1f, height),
                    _ => throw new Exception("Invalid orientation!"),
                };
                _foregroundRect.sizeDelta = Vector2.zero;
                _foregroundRect.anchoredPosition = Vector2.zero;
            }
        }
    }

}
