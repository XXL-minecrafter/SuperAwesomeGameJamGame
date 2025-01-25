using UnityEngine;


[System.Serializable]
public class Sound
{
    [field: SerializeField] public string Name { get; set; } = "EmptySound";

    [SerializeField] private AudioClip clip;

    [SerializeField][Range(0f, 1f)] private float volume = .5f;

    [Header("Pitch")]
    [SerializeField][Range(.1f, 5f)] private float pitch = 1f;
    [SerializeField] private bool useRandomPitch = false;

    [SerializeField][Range(.1f, 5f)] private float minimumPitch;

    [SerializeField][Range(.1f, 5f)] private float maximumPitch;

    [Header("Others")]
    [SerializeField][Range(0f, 1f)] private float spacialBlend;

    [SerializeField] private bool loop;

    [SerializeField] private bool playOnStart;

    private AudioSource source;

    public bool IsPlaying { get => source.isPlaying; }

    public void CreateSoundSource(GameObject on)
    {
        #region Set source variables
        source = on.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.spatialBlend = spacialBlend;
        #endregion

        if (playOnStart) Play();
    }

    public void Play()
    {
        if (useRandomPitch)
        {
            source.pitch = Random.Range(minimumPitch, maximumPitch);
        }

        source.Play();
    }

    public void Stop() => source.Stop();
}
