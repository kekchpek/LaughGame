using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public interface IPlayerPositionSource
    {
        Vector3 Position { get; }
    }
}