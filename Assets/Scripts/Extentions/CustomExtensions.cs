using UnityEngine;
using System;
public static class CustomExtensions
    {
        public static float Remap(this float value, 
            float valueRangeMin, float valueRangeMax, 
            float newRangeMin, float newRangeMax) 
        {
            return (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;
        }

        public static void ForEachChildRecursive(this Transform transform, Action<GameObject> action)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                action(child.gameObject);
                child.ForEachChildRecursive(action);
            }
        }
    }