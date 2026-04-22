using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BeatmapRecorder : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    public Beatmap targetBeatmap;

    [Header("Controls")]
    [SerializeField] InputAction recordAction;

    [Header("Settings")]
    public bool clearExistingNotesOnStart = true;

    double dspStartTime;
    bool isRecording;

    void OnEnable()
    {
        recordAction.Enable();
    }

    void OnDisable()
    {
        recordAction.Disable();
    }

    void Start()
    {
        if (targetBeatmap == null) return;

        if (clearExistingNotesOnStart)
        {
            targetBeatmap.notes.Clear();
        }

        double startDelay = 1.0;
        dspStartTime = AudioSettings.dspTime + startDelay;
        if (audioSource != null)
            audioSource.PlayScheduled(dspStartTime);

        isRecording = true;
    }

    void Update()
    {
        if (!isRecording) return;

        if (recordAction.WasPressedThisFrame())
        {
            float currentTime = (float)(AudioSettings.dspTime - dspStartTime);
            RecordNote(currentTime);
        }

        if (audioSource != null && !audioSource.isPlaying && AudioSettings.dspTime > dspStartTime)
        {
            StopRecording();
        }
    }

    void RecordNote(float time)
    {
        NoteData newNote = new NoteData();
        newNote.time = time;
        newNote.type = 0; 

        targetBeatmap.notes.Add(newNote);
        Debug.Log($"Recorded Note at {time:F3}s");

#if UNITY_EDITOR
        EditorUtility.SetDirty(targetBeatmap);
#endif
    }

    public void StopRecording()
    {
        isRecording = false;
        Debug.Log($"Recording Finished! Total notes: {targetBeatmap.notes.Count}");
        
#if UNITY_EDITOR
        AssetDatabase.SaveAssets(); 
#endif
    }
}