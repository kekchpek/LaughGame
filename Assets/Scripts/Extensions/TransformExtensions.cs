using UnityEngine;

namespace LaughGame.Extensions
{
    public static class TransformExtensions
    {
        public static Vector2 Pos2d(this Transform t)
        {
            return t.position;
        }
    }
}