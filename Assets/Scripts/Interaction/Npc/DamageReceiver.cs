namespace LaughGame.Interaction.Npc
{
    public class DamageReceiver : IDamageReceiver
    {

        private IDamagable _damagable;
        
        public void SetDamagable(IDamagable damagable)
        {
            _damagable = damagable;
        }

        public void DoDamage(float damage)
        {
            _damagable?.TakeDamage(damage);
        }
    }
}