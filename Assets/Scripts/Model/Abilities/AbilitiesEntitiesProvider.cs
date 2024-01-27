using LaughGame.Model.Abilities;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class AbilitiesEntitiesProvider : IAbilitiesEntitiesProvider
    {
        private IMovable _movablePlayer;

        public void SetMovable(IMovable player)
        {
            _movablePlayer = player;
        }

        public IMovable GetMovablePlayer()
        {
            return _movablePlayer;
        }


    }
}