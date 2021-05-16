using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public enum SoundNames
    {
        WhispCrash,
        StarDeparture,
        WhispArrival,

        LevelCompletion,
        Play,

        Click,
        DragCard,
        HoverCard,
        MergeCard
    }
    public enum MusicNames
    {
        MenuTheme,

        MainTheme,
        ActionTheme
    }

    private Dictionary<MusicNames, List<AudioSource>> MusicSources = new Dictionary<MusicNames, List<AudioSource>>();
    private Dictionary<SoundNames, List<AudioSource>> SoundSources = new Dictionary<SoundNames, List<AudioSource>>();

    public List<MusicSO> Musics = new List<MusicSO>();
    public List<SoundSO> Sounds = new List<SoundSO>();

    public GameObject MusicsContainer;
    public GameObject SoundsContainer;

    public float MusicVolume = -12f;

    public AudioMixer AudioMixer;
    public AudioMixerGroup AudioMixerGroup;
    public float FadingTime = 1f;

    private MusicSO _selectedMusic = null;
    private MusicSO _finishingMusic = null;
    private Coroutine _musicCoroutine;

    public static SoundController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        AudioMixer.SetFloat("Fading", MusicVolume);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlaySound(SoundNames.Click);
    }

    public void DoDatMusicWork()
    {
        PlayMusic(MusicNames.MenuTheme);
    }

    public void DoDatSoundWork()
    {
        StartCoroutine(testsound());
    }
    public IEnumerator testsound()
    {
        for (int i = 0; i < 100; i++)
        {
            PlaySound(SoundNames.WhispCrash);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
        }
    }

    public void DoDatMusicStop()
    {
        StopMusic();
    }

    // MUSICS
    public void PlayMusic(MusicNames musicID)
    {
        if (_selectedMusic != null)
        {
            StopMusic();
            _finishingMusic = _selectedMusic;
        }
        _selectedMusic = Musics.Find(music => music.MusicID == musicID);
        if (_selectedMusic == null)
        {
            Debug.LogError("The music to play has not been found -- " + musicID);
            return;
        }
        if (MusicSources.ContainsKey(musicID))
        {
            Debug.LogWarning("The music is already being played -- " + musicID);
            return;
        }

        if (_finishingMusic != null)
        {
            StartCoroutine(FadeMixerGroup.StartFade(AudioMixer, "Fading", FadingTime, -40f));
            StartCoroutine(ChangePlayingMusic());
        }
        else
        {
            _musicCoroutine = StartCoroutine(PlayingMusic(_selectedMusic));
        }
    }

    private IEnumerator ChangePlayingMusic()
    {
        yield return StoppingMusic();
        _musicCoroutine = StartCoroutine(PlayingMusic(_selectedMusic));
    }

    public IEnumerator PlayingMusic(MusicSO music)
    {
        MusicSources[music.MusicID] = new List<AudioSource>();

        foreach (ClipSO clip in music.MainLines)
        {
            AudioSource audio = SetupAudioMusic(clip);
            MusicSources[music.MusicID].Add(audio);
        }

        if (music.BarsToWait[0])
            yield return new WaitForSeconds(music.MainLines[0].clip.length);

        foreach (ClipSO clip in music.MelodicLines)
        {
            AudioSource audio = SetupAudioMusic(clip);
            MusicSources[music.MusicID].Add(audio);
        }

        if (music.BarsToWait[1])
            yield return new WaitForSeconds(music.MelodicLines[0].clip.length);

        foreach (ClipSO clip in music.AccompanimentLines)
        {
            AudioSource audio = SetupAudioMusic(clip);
            MusicSources[music.MusicID].Add(audio);
        }
    }

    public void StopMusic()
    {
        if (_selectedMusic == null) return;
        StopCoroutine(_musicCoroutine);
    }

    public IEnumerator StoppingMusic()
    {
        yield return new WaitForSeconds(FadingTime);

        if (_finishingMusic != null && MusicSources.ContainsKey(_finishingMusic.MusicID))
        {
            for (int i = 0; i < MusicSources[_finishingMusic.MusicID].Count; i++)
            {
                AudioSource audio = MusicSources[_finishingMusic.MusicID][i];
                Destroy(audio.gameObject);
            }
            MusicSources[_finishingMusic.MusicID].Clear();

            MusicSources.Remove(_finishingMusic.MusicID);
            _finishingMusic = null;
        }

        AudioMixer.SetFloat("Fading", MusicVolume);
    }

    public AudioSource SetupAudioMusic(ClipSO clipObject)
    {
        AudioSource audio = Instantiate(Resources.Load("Prefabs/AudioPrefab") as GameObject, MusicsContainer.transform).GetComponent<AudioSource>();
        audio.clip = clipObject.clip;
        audio.loop = true;
        audio.outputAudioMixerGroup = AudioMixerGroup;

        if (clipObject.Echo != null)
        {
            AudioEchoFilter echo = audio.gameObject.AddComponent<AudioEchoFilter>();
            echo.delay = (60 / clipObject.BPM) * 1000f;
            echo.decayRatio = clipObject.Echo.DecayRatio;
            echo.dryMix = clipObject.Echo.DryMix;
        }
        if (clipObject.Reverb != null)
        {
            AudioReverbFilter reverb = audio.gameObject.AddComponent<AudioReverbFilter>();
            reverb.reverbPreset = clipObject.Reverb.ReverbPreset;
        }

        audio.Play();
        return audio;
    }

    // SOUNDS
    public void PlaySound(SoundNames soundID)
    {
        SoundSO sound = Sounds.Find(s => s.SoundID == soundID);

        if (sound != null)
            StartCoroutine(PlayingSound(sound));
    }

    public IEnumerator PlayingSound(SoundSO sound)
    {
        AudioSource audio = Instantiate(Resources.Load("Prefabs/AudioPrefab") as GameObject, SoundsContainer.transform).GetComponent<AudioSource>();

        if (!SoundSources.ContainsKey(sound.SoundID))
            SoundSources.Add(sound.SoundID, new List<AudioSource>());
        SoundSources[sound.SoundID].Add(audio);

        audio.PlayOneShot(sound.Sound);

        yield return new WaitForSeconds(sound.Sound.length);

        SoundSources[sound.SoundID].Remove(audio);
        if (SoundSources[sound.SoundID].Count <= 0)
            SoundSources.Remove(sound.SoundID);

        Destroy(audio.gameObject);
    }

    // Sounds controls
    public void MouseClick()
    {
        PlaySound(SoundNames.Click);
    }
}

}
