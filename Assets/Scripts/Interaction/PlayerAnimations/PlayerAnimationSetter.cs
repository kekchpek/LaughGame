using Spine.Unity;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.PlayerAnimations
{
    public class PlayerAnimationSetter : MonoBehaviour
    {

        [SerializeField]
        private SkeletonAnimation _spineAnimation;
        
        [Inject]
        public void Construct(IPlayerAnimationProvider playerAnimationProvider)
        {
            playerAnimationProvider.SpineAnimation = _spineAnimation;
        }
    }
}