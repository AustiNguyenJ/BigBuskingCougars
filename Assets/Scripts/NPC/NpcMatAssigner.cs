using System;
using ExternPropertyAttributes.Editor;
using NPC;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcMatAssigner : MonoBehaviour
{
    public SkinnedMeshRenderer npcRenderer;

    [SerializeField] private RandomMatProvider skinOptions;
    private Material[] sharedMats;

    public string skinMatName;

    private void Awake()
    {
        sharedMats = npcRenderer.sharedMaterials;
    }


    private void Start()
    {
        if (Validate.AnyNull(npcRenderer, skinOptions)) return;

        Material[] currentMats = npcRenderer.materials;

        if (!skinOptions.HasMats)
        {
            Debug.LogWarning("No available material options", gameObject);
            return;
        }

        for (int i = 0; i < sharedMats.Length; i++)
        {
            if (sharedMats[i] != null && sharedMats[i].name.Contains(skinMatName))
            {
                currentMats[i] = skinOptions.GetRandomMat();
            }
        }
        npcRenderer.materials = currentMats;
    }
}