using UnityEngine;

namespace Utils
{
    public static class MathHelper
    {
        public static float Determinant(Vector3 a, Vector3 b)
        {
            return a.x * b.y + a.y * b.z + a.z * b.x - a.z * b.y - a.x * b.z - a.y * b.x;
        }
    }
}
