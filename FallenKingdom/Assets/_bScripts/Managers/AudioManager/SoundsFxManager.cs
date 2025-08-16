using System.Collections.Generic;
using UnityEngine;

public class SoundsFxManager : MonoBehaviour
{
    public static SoundsFxManager Instance { get; private set; }


    [SerializeField] private List<AudioClip> soundEffects;

    private Dictionary<int, GameObject> loopingSounds = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> backgroundLoopingSounds = new Dictionary<int, GameObject>();


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


    public void PlayBackgroundLoopingSound(int soundIndex)
    {
        if (soundIndex < 0 || soundIndex >= soundEffects.Count)
        {
            return;
        }

        if (loopingSounds.ContainsKey(soundIndex))
        {
            return;
        }

        GameObject loopAudioObject = new GameObject($"BackgroundLoopingAudio_{soundIndex}");
        AudioSource audioSource = loopAudioObject.AddComponent<AudioSource>();

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        float backgroundVolume = Mathf.Clamp(soundVolume * 0.08f, 0f, 0.08f);

        audioSource.volume = backgroundVolume;
        audioSource.clip = soundEffects[soundIndex];
        audioSource.loop = true;
        audioSource.Play();

        backgroundLoopingSounds[soundIndex] = loopAudioObject;
    }

    public void StopBackgroundLoopingSound(int soundIndex)
    {
        if (!backgroundLoopingSounds.ContainsKey(soundIndex))
        {
            return;
        }

        GameObject backgroundLoopAudioObject = backgroundLoopingSounds[soundIndex];
        backgroundLoopingSounds.Remove(soundIndex);

        Destroy(backgroundLoopAudioObject);
    }

    public void UpdateAllVolumes()
    {
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);

        // Update volume of looping sounds
        foreach (var loopingSound in loopingSounds)
        {
            AudioSource audioSource = loopingSound.Value.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);
            }
        }

        // Update volume of background looping sounds
        foreach (var backgroundSound in backgroundLoopingSounds)
        {
            AudioSource audioSource = backgroundSound.Value.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                float backgroundVolume = Mathf.Clamp(soundVolume * 0.08f, 0f, 0.08f);
                audioSource.volume = backgroundVolume;
            }
        }

        // Update volume of temporary sounds
        AudioSource[] allTempAudioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (var audioSource in allTempAudioSources)
        {
            if (audioSource.gameObject.name.StartsWith("TempAudio"))
            {
                audioSource.volume = Mathf.Clamp(soundVolume, 0f, 1f);
            }
        }
    }

    public void SwitchBackgroundLoopingSound(int newSoundIndex)
    {
        
        foreach (var bgSound in backgroundLoopingSounds)
        {
            Destroy(bgSound.Value);
        }
        backgroundLoopingSounds.Clear();

      
        PlayBackgroundLoopingSound(newSoundIndex);
    }

}
