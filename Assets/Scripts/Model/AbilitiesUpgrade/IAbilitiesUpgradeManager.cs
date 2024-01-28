using System.Threading.Tasks;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public interface IAbilitiesUpgradeManager
    {
        bool CanUpgrade();
        void SetPopup(IAbilitiesPopup abilitiesPopup);
        Task<bool> StartUpgrade();
    }
}