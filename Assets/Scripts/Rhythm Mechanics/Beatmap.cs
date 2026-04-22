using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[System.Serializable]
public class NoteData 
{
    public float time;
    public int type; 
}

[CreateAssetMenu(fileName = "NewBeatmap", menuName = "Rhythm/Beatmap")]
public class Beatmap : ScriptableObject 
{
    public List<NoteData> notes = new List<NoteData>();

    [Button("Clear All Beats", ButtonSizes.Medium)]
    public void ClearBeats()
    {
        notes.Clear();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}