using System.Collections.Generic;
using UnityEngine;

public class SoundsFxManager : MonoBehaviour
{
    public static SoundsFxManager Instance { get; private set; }


    [SerializeField] private List<AudioClip> soundEffects;

    private Dictionary<int, GameObject> loopingSounds = new Dictionary<int, GameObject>();

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlaySoundAtIndex(int soundIndex)
    {
       
        if (soundIndex < 0 || soundIndex >= soundEffects.Count)
        {
            Debug.LogWarning("Invalid sound index!");
            return;
        }

    
        GameObject tempAudioObject = new GameObject("TempAudio");
        AudioSource audioSource = tempAudioObject.AddComponent<AudioSource>();

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);

        audioSource.clip = soundEffects[soundIndex];
        audioSource.Play();

       
        Destroy(tempAudioObject, audioSource.clip.length);
    }

    public void PlayLoopingSound(int soundIndex)
    {
        if (soundIndex < 0 || soundIndex >= soundEffects.Count)
        {
            //Debug.Log("Invalid sound index!");
            return;
        }

        if (loopingSounds.ContainsKey(soundIndex))
        {
          //  Debug.Log("Looping sound is already playing!");
            return;
        }

        GameObject loopAudioObject = new GameObject($"LoopingAudio_{soundIndex}");
        AudioSource audioSource = loopAudioObject.AddComponent<AudioSource>();

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);

        audioSource.clip = soundEffects[soundIndex];
        audioSource.loop = true;
        audioSource.Play();

        loopingSounds[soundIndex] = loopAudioObject;
    }

    public void StopLoopingSound(int soundIndex)
    {
        if (!loopingSounds.ContainsKey(soundIndex))
        {
           // Debug.Log("No looping sound to stop with this index!");
            return;
        }

        GameObject loopAudioObject = loopingSounds[soundIndex];
        loopingSounds.Remove(soundIndex);

        Destroy(loopAudioObject);
    }

    public void UpdateAllVolumes()
    {
       
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);

        
        foreach (var loopingSound in loopingSounds)
        {
            AudioSource audioSource = loopingSound.Value.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);
            }
        }


        
        AudioSource[] allTempAudioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (var audioSource in allTempAudioSources)
        {
            if (audioSource.gameObject.name.StartsWith("TempAudio"))
            {
                audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);
            }
        }
    }


}
