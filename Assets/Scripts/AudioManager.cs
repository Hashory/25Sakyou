using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource; // BGM用AudioSource
    [SerializeField] private AudioSource[] seSources; // SE用AudioSource複数

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips; // BGMクリップ
    [SerializeField] private AudioClip[] seClips; // SEクリップ

    private Dictionary<string, AudioClip> bgmDictionary; // 名前でBGMを管理
    private Dictionary<string, AudioClip> seDictionary; // 名前でSEを管理

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも残す
        }
        else
        {
            Destroy(gameObject);
        }

        // Dictionaryの初期化
        InitializeDictionaries();
    }
    private void Start()
    {
        //ここでBGMを鳴らそう
        PlayBGM("25Sakyou - Title v0.0.2_101428 (mp3cut.net)", true);
    }
    /// <summary>
    /// Dictionaryを初期化
    /// </summary>
    private void InitializeDictionaries()
    {
        bgmDictionary = new Dictionary<string, AudioClip>();
        seDictionary = new Dictionary<string, AudioClip>();

        // BGMクリップをDictionaryに登録
        foreach (var clip in bgmClips)
        {
            if (!bgmDictionary.ContainsKey(clip.name))
            {
                bgmDictionary.Add(clip.name, clip);
            }
        }

        // SEクリップをDictionaryに登録
        foreach (var clip in seClips)
        {
            if (!seDictionary.ContainsKey(clip.name))
            {
                seDictionary.Add(clip.name, clip);
            }
        }
    }
    /// <summary>
    /// 名前でBGMを再生
    /// </summary>
    public void PlayBGM(string bgmName, bool loop = true)
    {
        if (bgmDictionary.TryGetValue(bgmName, out var clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{bgmName}' not found in the dictionary.");
        }
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// 名前でSEを再生
    /// </summary>
    public void PlaySE(string seName)
    {
        if (seDictionary.TryGetValue(seName, out var clip))
        {
            foreach (var source in seSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.Play();
                    return;
                }
            }

            Debug.LogWarning("No available SE audio sources to play sound.");
        }
        else
        {
            Debug.LogWarning($"SE '{seName}' not found in the dictionary.");
        }
    }

    public float SetBGMVolume(float num)
    {
        float newVolume = Mathf.Clamp(bgmSource.volume + num, 0f, 1f);
        newVolume = Mathf.Round(newVolume * 10f) / 10f; // 小数第1位に丸める
        bgmSource.volume = newVolume;
        return newVolume;
    }

    public float GetBGMVolume()
    {
        float volume = Mathf.Clamp(bgmSource.volume, 0f, 1f);
        volume = Mathf.Round(bgmSource.volume * 10f) / 10f;
        return volume;
    }

    public float SetSEVolume(float num)
    {
        float newVolume = Mathf.Clamp(seSources[0].volume + num, 0f, 1f);
        newVolume = Mathf.Round(newVolume * 10f) / 10f; // 小数第1位に丸める
        foreach (var i in seSources)
        {
            i.volume = newVolume;
        }
        return newVolume;
    }

    public float GetSEVolume()
    {
        float volume = Mathf.Clamp(seSources[0].volume, 0f, 1f);
        volume = Mathf.Round(seSources[0].volume * 10f) / 10f;
        return volume;
    }
}
