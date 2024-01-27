using System.Threading.Tasks;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public interface IAbilitiesPopup
    {
        Task Upgrade(IAbility[] abilitiesToChose);
    }
}