using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    // TODO Extend to use dialogue volume

    public enum AudioChannel { Master, Sfx, Music, Dialogue };

    public float MasterVolumePercent { get; protected set; }
    public float SfxVolumePercent { get; protected set; }
    public float MusicVolumePercent { get; protected set; }
    public float DialogueVolumePercent { get; protected set; }

    public static AudioManager Instance {
        get { return instance; }
    }

    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    static AudioManager instance;

    Transform audioListener;
    Transform playerT;

    SoundLibrary library;

    bool lowerVolume = false;

    [SerializeField]
    AudioSource musicSource;

    [Header("Music compressor")]
    [SerializeField]
    [Range(0, 1)]
    float duckingPercent = 0.6f;
    [SerializeField]
    [Range(0, 6)]
    float compressorAttack = 0.5f;
    [SerializeField]
    [Range (0, 6)]
    float compressorRelease = 1f;

    float musicDefaultVolume;
    float currentAttackLerpTime;

    void Awake () {

        if (instance != null) {
            Destroy (gameObject);
        }
        else if (instance == null) {
            instance = this;
        }

        library = GetComponent<SoundLibrary> ();

        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++) {
            GameObject newMusicSource = new GameObject ("Music Source " + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource> ();
            newMusicSource.transform.parent = transform;
        }
        GameObject newSfx2DSource = new GameObject ("2D Sfx Source");
        newSfx2DSource.transform.parent = transform;
        sfx2DSource = newSfx2DSource.AddComponent<AudioSource> ();

        AudioReverbFilter rb = sfx2DSource.gameObject.AddComponent<AudioReverbFilter> ();
        rb.reverbPreset = AudioReverbPreset.Room;
        rb.dryLevel = 1f;
        rb.reverbLevel = 0.4f;

        AudioHighPassFilter hp = sfx2DSource.gameObject.AddComponent<AudioHighPassFilter> ();
        hp.cutoffFrequency = 500f;
        hp.highpassResonanceQ = 0f;

        if (FindObjectOfType<Player> () != null) {
            playerT = FindObjectOfType<Player> ().transform;
        }
    }

    void Start () {
        MasterVolumePercent = SettingsManager.Instance.MasterVolume;
        SfxVolumePercent = SettingsManager.Instance.SfxVolume;
        MusicVolumePercent = SettingsManager.Instance.MusicVolume;
        DialogueVolumePercent = SettingsManager.Instance.DialogueVolume;

        musicDefaultVolume = musicSource.volume;
    }

    void Update () {
        if (sfx2DSource.isPlaying) {
            lowerVolume = true;
            currentAttackLerpTime = 0f;
        }
        else lowerVolume = false;

        currentAttackLerpTime += Time.deltaTime;
        if (currentAttackLerpTime > compressorAttack) currentAttackLerpTime = compressorAttack;
        float attackPercent = currentAttackLerpTime / compressorAttack;

        if (lowerVolume) musicSource.volume = Mathf.Lerp (musicSource.volume, musicDefaultVolume * duckingPercent, attackPercent);
        else musicSource.volume = Mathf.Lerp (musicSource.volume, musicDefaultVolume, Time.time * compressorRelease);
    }


    public void PlayMusic (AudioClip clip, float fadeDuration) {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play ();

        StartCoroutine (AnimateMusicCrossfade (fadeDuration));
    }

    public void PlaySound (AudioClip clip, Vector3 pos) {
        if (clip != null) AudioSource.PlayClipAtPoint (clip, pos, SfxVolumePercent * MasterVolumePercent);
    }

    public void PlaySound (string soundName, Vector3 pos) {
        PlaySound (library.GetClipFromName (soundName), pos);
    }

    public void PlaySound2D (string soundName) {
        if (sfx2DSource.isPlaying) sfx2DSource.Stop ();
        sfx2DSource.PlayOneShot (library.GetClipFromName (soundName), SfxVolumePercent * MasterVolumePercent);
    }

    void OnLevelWasLoaded (int index) {
        if (playerT == null) {
            if (FindObjectOfType<Player> () != null) {
                playerT = FindObjectOfType<Player> ().transform;
            }
        }
    }

    IEnumerator AnimateMusicCrossfade (float duration) {
        float percent = 0;

        while (percent < 1) {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp (0, MusicVolumePercent * MasterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp (MusicVolumePercent * MasterVolumePercent, 0, percent);
            yield return null;
        }
    }
}