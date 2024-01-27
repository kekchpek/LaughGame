using LaughGame.Assets.Scripts.Model.Abilities;

namespace LaughGame.Model.Abilities
{
    public interface IAbilitiesEntitiesProvider
    {
        void SetMovable(IMovable player);
        IMovable GetMovablePlayer();
    }
}