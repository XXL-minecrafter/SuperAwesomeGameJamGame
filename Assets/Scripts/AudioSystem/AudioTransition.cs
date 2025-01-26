using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioTransition : MonoBehaviour
{
    [SerializeField] private Audio[] intensityLevels;
    [SerializeField] private Audio transitionSound;

    private AudioSource audioSource;

    private int currentIntensity;

    private Coroutine switchAudioOperation;

    private void Awake() => audioSource = gameObject.AddComponent<AudioSource>();

    private void Start()
    {
        // Pre-loads audio clips to prevent micro delay in transitions
        foreach (var clip in intensityLevels)
        {
            clip.Clip.LoadAudioData();
        }

        // Plays idle clip
        audioSource.clip = intensityLevels[currentIntensity].Clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void RaiseIntensityLevel()
    {
        if (switchAudioOperation is not null) StopCoroutine(switchAudioOperation);

        switch (currentIntensity)
        {
            // Idle -> Chewing
            case 0:
                switchAudioOperation = StartCoroutine(SwitchAudio(intensityLevels[++currentIntensity]));
                break;

            // Chewing -> Bing -> Chase
            case 1:
                switchAudioOperation = StartCoroutine(SwitchAudio(transitionSound));
                switchAudioOperation = StartCoroutine(SwitchAudio(intensityLevels[++currentIntensity], transitionSound.Clip.length));
                break;

            // Chase -> Chewing
            case 2:
                switchAudioOperation = StartCoroutine(SwitchAudio(intensityLevels[--currentIntensity]));
                break;
        }
    }

    private IEnumerator SwitchAudio(Audio clip, float waitDuration = -1)
    {
        yield return new WaitForSeconds(waitDuration == -1 ? clip.BeatDuration - (audioSource.time % clip.BeatDuration) : waitDuration);
        audioSource.clip = clip.Clip;
        audioSource.Play();
    }

    public int GetCurrentIntensity() => currentIntensity;
}

#if UNITY_EDITOR
[CustomEditor(typeof(AudioTransition))]
public class AudioTransitionEditor : Editor
{
    private AudioTransition audioTransition;

    private void OnEnable()
    {
        audioTransition = (AudioTransition)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.IntField("Current intensity:", audioTransition.GetCurrentIntensity());
        if (GUILayout.Button("Raise Intensity Level")) audioTransition.RaiseIntensityLevel();
        EditorGUILayout.EndHorizontal();
    }
}
#endif
