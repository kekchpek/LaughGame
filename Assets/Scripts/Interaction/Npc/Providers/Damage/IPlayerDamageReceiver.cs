namespace LaughGame.Interaction.Npc
{
    public interface IPlayerDamageReceiver
    {
        void SetDamagable(IPlayerDamagable playerDamagable);
        void DoDamage(float damage);
    }
}