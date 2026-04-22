using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public struct OnDrumHit { }

public enum HitType
{
    Bad,
    Good,
    Perfect
}

public struct OnHitScore
{
    public HitType hitType;
}

public class HitJudge : MonoBehaviour
{
    public Conductor conductor;
    public Beatmap activeBeatmap;
    
    public float perfectWindow = 0.05f;
    public float goodWindow = 0.10f;
    public float missWindow = 0.20f;


    public Queue<NoteData> activeNotes = new Queue<NoteData>();

    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<OnDrumHit>(OnDrumHit);
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<OnDrumHit>(OnDrumHit);
    }

    void Start()
    {
        foreach (NoteData note in activeBeatmap.notes)
        {
            activeNotes.Enqueue(note);
        }
    }

    void Update()
    {
        // this handles missed notes
        NoteData nextNote = activeNotes.Peek();
        if (conductor.songPosition > nextNote.time + missWindow)
        {
            Debug.Log("Miss!");
            activeNotes.Dequeue();
        }
    }


    void OnDrumHit(OnDrumHit data)
    {
        NoteData nextNote = activeNotes.Peek();
        EvaluateHit(nextNote.time);
        
    }

    void EvaluateHit(float targetTime)
    {
        float hitDifference = Math.Abs(conductor.songPosition - targetTime);

        if (hitDifference <= perfectWindow)
        {
            activeNotes.Dequeue();
            GlobalEventAsset.Instance.TriggerEvent(new OnHitScore { hitType = HitType.Perfect });
        }
        else if (hitDifference <= goodWindow)
        {
            activeNotes.Dequeue();
            GlobalEventAsset.Instance.TriggerEvent(new OnHitScore { hitType = HitType.Good });
        }
        else if (hitDifference <= missWindow)
        {
            activeNotes.Dequeue();
            GlobalEventAsset.Instance.TriggerEvent(new OnHitScore { hitType = HitType.Bad});
        }
    }
}