using System;
using UnityEngine;
using System.Collections.Generic;

public class VisualManager : MonoBehaviour
{
    public Conductor conductor;
    public Beatmap activeBeatmap;
    public VisualNote notePrefab;
    public RectTransform noteContainer; 

    public float scrollSpeed = 2.5f;
    public float hitZoneY = -2.12f;
    public float spawnLeadTime = 2.0f; 

    public Queue<NoteData> spawnQueue = new Queue<NoteData>();

    void Start()
    {
        foreach (NoteData note in activeBeatmap.notes)
        {
            spawnQueue.Enqueue(note);
        }
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    void Update()
    {
        if (spawnQueue.Count == 0) return;

        float nextSpawnTime = spawnQueue.Peek().time - spawnLeadTime;

        if (conductor.songPosition >= nextSpawnTime)
        {
            SpawnNote(spawnQueue.Dequeue());
        }
    }

    void SpawnNote(NoteData noteData)
    {
        VisualNote newNote = Instantiate(notePrefab, noteContainer);
        newNote.Initialize(noteData.time, scrollSpeed, hitZoneY, conductor);
    }
}