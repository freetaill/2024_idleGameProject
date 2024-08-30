using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    // Static instance of the SoundManager which allows it to be accessed by any other script
    private static SoundManager _instance;

    // Public property to access the instance
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Search for an existing instance of the SoundManager
                _instance = FindObjectOfType<SoundManager>();

                if (_instance == null)
                {
                    // If no instance is found, create a new one
                    GameObject soundManager = new GameObject("SoundManager");
                    _instance = soundManager.AddComponent<SoundManager>();

                    // Optionally, make the instance persistent across scenes
                    DontDestroyOnLoad(soundManager);
                }
            }

            return _instance;
        }
    }

    // Ensure only one instance exists and destroy duplicates

    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource soundAudio;

    [SerializeField] private List<AudioClip> cacheAudioClipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipDic = new Dictionary<string, AudioClip>();

    private bool isSoundOnOff;
    private bool isBGMOnOff;
    private float soundVolume = 1.0f;
    private float bgmVolume = 1.0f;

    Coroutine soundCoolTimeCoroutine = null;


    public bool IsSoundOnOff { get => isSoundOnOff; set { isSoundOnOff = value; } }
    public bool IsBGMOnOff { get => isBGMOnOff; set { isBGMOnOff = value; } }
    public float SoundVolume { get { return soundVolume; } set { soundVolume = value; } }
    public float BGMVolume { get { return bgmVolume; } set { bgmVolume = value; } }

    private Camera cam;

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // This makes the SoundManager persist between scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }

        cam = Camera.main;

        IsSoundOnOff = true;
        IsBGMOnOff = true;
        SoundVolume = 1.0f;
        BGMVolume = 1.0f;

        LoadAudio();
    }
    private void LoadAudio()
    {
        for (int i = 0; i < cacheAudioClipList.Count; i++)
            clipDic.Add(cacheAudioClipList[i].name, cacheAudioClipList[i]);
    }


    public void SetSoundVolume(float volume)
    {
        SoundVolume = volume;
    }
    public void SetBGMVolume(float volume)
    {
        bgmAudio.volume = BGMVolume * volume;
    }

    public void Play(string name)
    {
        if (!IsSoundOnOff) return;
        if (clipDic.ContainsKey(name) == false)
            return;
        AudioClip clip = clipDic[name];
        if (Time.timeScale > 0)
            AudioSource.PlayClipAtPoint(clip, this.transform.position, SoundVolume);
        else
            soundAudio.PlayOneShot(clip, soundVolume);

    }

    public void PlayLimit(string name)
    {
        if (!IsSoundOnOff) return;
        if (clipDic.ContainsKey(name) == false)
            return;

        if (soundCoolTimeCoroutine != null) return;
        soundCoolTimeCoroutine = StartCoroutine(UpdateLimitCoolTime());

        AudioClip clip = clipDic[name];
        AudioSource.PlayClipAtPoint(clip, this.transform.position, SoundVolume);
        // soundAudio.PlayOneShot(clip,soundVolume);
    }
    IEnumerator UpdateLimitCoolTime()
    {
        yield return new WaitForSecondsRealtime(0.05f);

        soundCoolTimeCoroutine = null;
    }

    public void PlayBGM(string name)
    {
        if (clipDic.ContainsKey(name) == false)
            return;
        AudioClip clip = clipDic[name];

        bgmAudio.clip = clip;
        bgmAudio.volume = BGMVolume;
        if (!IsBGMOnOff) return;
        bgmAudio.Play();

    }
    public void PlayBGM()
    {
        bgmAudio.Play();
    }
    public void StopBGM()
    {
        bgmAudio.Pause();
    }

}


