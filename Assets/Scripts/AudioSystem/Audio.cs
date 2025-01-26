using UnityEngine;

[System.Serializable]
public struct Audio
{
    public AudioClip Clip;
    public float BeatDuration;

    public Audio(AudioClip clip, float beatDuration)
    {
        Clip = clip;
        BeatDuration = beatDuration;
    }
}
