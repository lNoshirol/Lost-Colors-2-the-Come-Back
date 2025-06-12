using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsManager : MonoBehaviour
{
    [Tooltip("The audio source used to play SFXs.")] public AudioSource _sfxSource;
    [Tooltip("The audio source used to play ambiant sounds.")] public AudioSource _ambiantSource;
    [Tooltip("The audio source used to play music.")] public AudioSource _musicSource;

    [SerializeField] private AudioClip _theOneAndOnlyMusic;
    [SerializeField] private AudioClip _theOneAmbiantSound;
    [SerializeField] private bool _hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment;

    // Singleton
    #region Singleton
    private static SoundsManager _instance;

    public static SoundsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SoundsManager");
                _instance = go.AddComponent<SoundsManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        if (!_hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment) return;
        PlayMusic(_theOneAndOnlyMusic, false);
        PlayAmbiant(_theOneAmbiantSound, false);
    }

    public void PlaySound(AudioClip audioClip, bool isPitchRandom)
    {
        _sfxSource.pitch = (!isPitchRandom) ? 1 : 1 + Random.Range(-0.2f, 0.2f);
        _sfxSource.PlayOneShot(audioClip);
    }

    public async void PlayMusic(AudioClip music, bool loop)
    {
        _musicSource.clip = music;
        _musicSource.loop = loop;
        _musicSource.Play();
        int TimeMusicHasFinished = Mathf.FloorToInt(music.length * 1000);
        await Task.Delay(TimeMusicHasFinished);
        var myRandomIndex = Random.Range(5, 15);
        await Task.Delay(myRandomIndex * 1000);
        Debug.Log("JAIFINI");
    }    
    
    public async void PlayAmbiant(AudioClip music, bool loop)
    {
        _ambiantSource.clip = music;
        _ambiantSource.loop = loop;
        _ambiantSource.Play();
        int TimeMusicHasFinished = Mathf.FloorToInt(music.length * 1000);
        await Task.Delay(TimeMusicHasFinished);
        var myRandomIndex = Random.Range(5, 15);
        await Task.Delay(myRandomIndex * 1000);
        Debug.Log("JAIFINI");
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void StopSound()
    {
        _musicSource.Stop();
        _ambiantSource.Stop();
        
    }

    public void SlowDownAllSound()
    {
        _musicSource.pitch = 0.5f;
        _ambiantSource.pitch = 0.5f;
    }

    public void BackToNormal()
    {
        _musicSource.pitch = 1;
        _ambiantSource.pitch = 1;
    }
}