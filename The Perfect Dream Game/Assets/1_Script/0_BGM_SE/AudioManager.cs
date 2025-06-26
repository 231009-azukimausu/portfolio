using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM List")]
    public List<AudioClip> bgmClips = new List<AudioClip>();
    private AudioSource bgmSourceA;
    private AudioSource bgmSourceB;
    private bool isUsingA = true;

    [Header("SE List")]
    public List<AudioClip> seClips = new List<AudioClip>();
    private AudioSource seSource;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SE_VOLUME_KEY = "SE_VOLUME";
    private float bgmVolume = 1.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSourceの初期化
            bgmSourceA = gameObject.AddComponent<AudioSource>();
            bgmSourceA.loop = true;

            bgmSourceB = gameObject.AddComponent<AudioSource>();
            bgmSourceB.loop = true;

            seSource = gameObject.AddComponent<AudioSource>();

            // 音量の復元
            bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1.0f);
            float seVol = PlayerPrefs.GetFloat(SE_VOLUME_KEY, 1.0f);

            bgmSourceA.volume = 0;
            bgmSourceB.volume = 0;
            SetBGMVolume(bgmVolume);
            SetSEVolume(seVol);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ===== BGMクロスフェード再生 =====
    public void CrossFadeToBGM(int index, float fadeTime = 1.0f)
    {
        if (index < 0 || index >= bgmClips.Count || bgmClips[index] == null)
        {
            Debug.LogWarning("BGM indexが不正 or null");
            return;
        }

        AudioSource fromSource = isUsingA ? bgmSourceA : bgmSourceB;
        AudioSource toSource = isUsingA ? bgmSourceB : bgmSourceA;
        isUsingA = !isUsingA;

        toSource.clip = bgmClips[index];
        toSource.volume = 0f;
        toSource.Play();
        toSource.loop = true;

        StartCoroutine(FadeRoutine(fromSource, toSource, fadeTime));
    }

    private IEnumerator FadeRoutine(AudioSource from, AudioSource to, float time)
    {
        float timer = 0f;

        while (timer < time)
        {
            float t = timer / time;
            from.volume = Mathf.Lerp(bgmVolume, 0f, t);
            to.volume = Mathf.Lerp(0f, bgmVolume, t);
            timer += Time.deltaTime;
            yield return null;
        }

        from.Stop();
        from.volume = 0f;
        to.volume = bgmVolume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);

        if (bgmSourceA.isPlaying) bgmSourceA.volume = volume;
        if (bgmSourceB.isPlaying) bgmSourceB.volume = volume;
    }

    public float GetBGMVolume() => bgmVolume;

    public void StopBGM()
    {
        bgmSourceA.Stop();
        bgmSourceB.Stop();
    }

    // ===== SE再生（シンプルなまま） =====
    public void PlaySE(int index)
    {
        if (index >= 0 && index < seClips.Count && seClips[index] != null)
        {
            seSource.PlayOneShot(seClips[index]);
        }
        else
        {
            Debug.LogWarning("SE indexが不正 or null");
        }
    }

    public void SetSEVolume(float volume)
    {
        seSource.volume = volume;
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, volume);
    }

    public float GetSEVolume() => seSource.volume;
}