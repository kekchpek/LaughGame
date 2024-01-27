namespace LaughGame.Interaction.Npc
{
    public class PlayerDamageReceiver : IPlayerDamageReceiver
    {

        private IPlayerDamagable _playerDamagable;
        
        public void SetDamagable(IPlayerDamagable playerDamagable)
        {
            _playerDamagable = playerDamagable;
        }

        public void DoDamage(float damage)
        {
            _playerDamagable?.TakeDamage(damage);
        }
    }
}