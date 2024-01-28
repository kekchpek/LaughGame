using System.Threading.Tasks;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public interface IAbilitiesUpgradeManager
    {
        void SetPopup(IAbilitiesPopup abilitiesPopup);
        Task<bool> StartUpgrade();
    }
}