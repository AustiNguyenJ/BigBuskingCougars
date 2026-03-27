using UnityEngine;

public class DrumFeedback : MonoBehaviour
{
    public enum DrumAudioMode
    {
        ZoneClips,
        PitchedSingleClip
    }

    [Header("Refs")]
    public AudioSource audioSource;
    public DrumVisual drumVisual;

    [Header("Audio Mode")]
    public DrumAudioMode audioMode = DrumAudioMode.ZoneClips;

    [Header("Option 1: Different Clips")]
    public AudioClip centerClip;
    public AudioClip midClip;
    public AudioClip edgeClip;

    [Header("Option 2: Same Clip, Different Pitches")]
    public AudioClip baseHitClip;
    public float centerPitch = 0.9f;
    public float midPitch = 1.0f;
    public float edgePitch = 1.15f;

    [Header("Volume")]
    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();

        if (drumVisual == null)
            drumVisual = GetComponentInChildren<DrumVisual>();
    }

    public void Hit(float power, DrumSurfaceZones.ZoneResult zoneResult)
    {
        PlayZoneAudio(power, zoneResult.zone);

        if (drumVisual != null)
            drumVisual.PlayHitVisual(power, zoneResult);
    }

    void PlayZoneAudio(float power, DrumSurfaceZones.DrumZone zone)
    {
        if (audioSource == null) return;

        float volume = Mathf.Lerp(minVolume, maxVolume, power);

        switch (audioMode)
        {
            case DrumAudioMode.ZoneClips:
                PlayZoneClip(zone, volume);
                break;

            case DrumAudioMode.PitchedSingleClip:
                PlayPitchedClip(zone, volume);
                break;
        }
    }

    void PlayZoneClip(DrumSurfaceZones.DrumZone zone, float volume)
    {
        AudioClip clipToPlay = null;

        switch (zone)
        {
            case DrumSurfaceZones.DrumZone.Center:
                clipToPlay = centerClip;
                break;
            case DrumSurfaceZones.DrumZone.Mid:
                clipToPlay = midClip;
                break;
            case DrumSurfaceZones.DrumZone.Edge:
                clipToPlay = edgeClip;
                break;
        }

        if (clipToPlay != null)
            audioSource.PlayOneShot(clipToPlay, volume);
    }

    void PlayPitchedClip(DrumSurfaceZones.DrumZone zone, float volume)
    {
        if (baseHitClip == null || audioSource == null) return;

        float pitch = 1f;

        switch (zone)
        {
            case DrumSurfaceZones.DrumZone.Center:
                pitch = centerPitch;
                break;
            case DrumSurfaceZones.DrumZone.Mid:
                pitch = midPitch;
                break;
            case DrumSurfaceZones.DrumZone.Edge:
                pitch = edgePitch;
                break;
        }

        GameObject temp = new GameObject("TempDrumAudio");
        temp.transform.position = audioSource.transform.position;

        AudioSource tempSource = temp.AddComponent<AudioSource>();
        tempSource.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
        tempSource.spatialBlend = audioSource.spatialBlend;
        tempSource.volume = volume;
        tempSource.pitch = pitch;
        tempSource.PlayOneShot(baseHitClip);

        Destroy(temp, baseHitClip.length / Mathf.Abs(pitch));
    }
}