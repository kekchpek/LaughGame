using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

namespace LaughGame.Interaction.PlayerAnimations
{
    public class PlayerAnimationProvider : IPlayerAnimationProvider
    {

        private bool _isWalk;
        private string _currentSkill;

        private string _currentAnimation;
        private AnimationState.TrackEntryDelegate _completeHandler;

        public SkeletonAnimation SpineAnimation { get; set; }

        public void SetWalk(bool isWalk)
        {
            if (isWalk == _isWalk)
                return;
            _isWalk = isWalk;
            if (_currentAnimation == null)
            {
                UpdateStateAnimation();
            }
        }

        private void UpdateStateAnimation()
        {
            SetAnimation(_isWalk ? "Walk_Hero" : "Idle");
        }

        public void PlaySkill(string skill)
        {
            _currentSkill = skill;
            PlayAnimation(skill);
        }

        public void PlayDamage()
        {
            if (_currentSkill == null)
            {
                PlayAnimation("Damage");
            }
        }

        public void SetFaceValue(float faceValue)
        {
            if (SpineAnimation == null)
                return;
            var index = faceValue switch
            {
                < 15f => 1,
                < 30f => 2,
                < 45f => 3,
                _ => 4
            };
            SpineAnimation.state.SetAnimation(1, $"Helf_{index}", false);
        }

        public void SetAnimation(string animation)
        {
            if (SpineAnimation == null)
                return;
            SpineAnimation.state.SetAnimation(0, animation, true);
        }

        public void PlayAnimation(string animation)
        {
            if (SpineAnimation == null)
                return;
            _currentAnimation = animation;
            SpineAnimation.state.End -= _completeHandler;

            _completeHandler = trackEntry =>
            {
                if (trackEntry.Animation.Name != animation)
                {
                    return;
                }

                _currentSkill = null;
                SpineAnimation.state.Complete -= _completeHandler;
                _currentAnimation = null;
                UpdateStateAnimation();
            };
            SpineAnimation.state.SetAnimation(0, animation, false);
            SpineAnimation.state.Complete += _completeHandler;
        } 
    }
}