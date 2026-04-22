using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource musicSource;
    public float bpm = 120f;
    public float songPosition;
    public float songPositionInBeats;
    
    float secPerBeat;
    double dspSongTime;

    void Start()
    {
        secPerBeat = 60f / bpm;
        dspSongTime = AudioSettings.dspTime + 1.0;
        
        musicSource.PlayScheduled(dspSongTime);
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPositionInBeats = songPosition / secPerBeat;
    }
}