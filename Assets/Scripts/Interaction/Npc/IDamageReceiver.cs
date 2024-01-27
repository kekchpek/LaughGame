namespace LaughGame.Interaction.Npc
{
    public interface IDamageReceiver
    {
        void SetDamagable(IDamagable damagable);
        void DoDamage(float damage);
    }
}