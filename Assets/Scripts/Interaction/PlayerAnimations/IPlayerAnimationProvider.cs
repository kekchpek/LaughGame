using Spine.Unity;

namespace LaughGame.Interaction.PlayerAnimations
{
    public interface IPlayerAnimationProvider
    {
        SkeletonAnimation SpineAnimation { set; }
        void SetWalk(bool isWalk);
        void PlaySkill(string skill);
        void PlayDamage();
    }
}