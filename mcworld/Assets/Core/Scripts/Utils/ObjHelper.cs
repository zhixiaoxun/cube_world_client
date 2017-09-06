using UnityEngine;

namespace Core.Utils
{
    public class ObjHelper
    {
        static public Transform GetChild(Transform transform, string boneName)
        {
            Transform child = transform.Find(boneName);
            if (child == null)
            {
                foreach (Transform c in transform)
                {
                    child = GetChild(c, boneName);
                    if (child != null) return child;
                }
            }
            return child;
        }
    }
}