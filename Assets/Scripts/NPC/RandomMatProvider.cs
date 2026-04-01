using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "Mat Provider", menuName = "Scriptable Objects/RandomMatProvider", order = 0)]
    public class RandomMatProvider : ScriptableObject
    {
        public List<Material> mats = new List<Material>();
        
        public bool HasMats => mats.Count > 0;

        public Material GetRandomMat()
        {
            if (mats.Count == 0)
                Debug.LogWarning("No materials found in RandomMatProvider");
            return mats[Random.Range(0, mats.Count)];
        }
    }
}