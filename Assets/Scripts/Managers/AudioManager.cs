using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private GameManager gameManager;

    public float soundVolume;

    private AudioSource audioSource;

    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] private AudioClip[] winSound;
    [SerializeField] private AudioClip[] loseSound;

    private void Awake()
    {
        if (musicSlider)
            musicSlider.onValueChanged.AddListener(ChangeAudioSourceVolume);

        if (soundSlider)
            soundSlider.onValueChanged.AddListener(ChangeSoundFXVolume);

        SaveManager.Init();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += GameManager_OnGameFinished;

        audioSource = GetComponent<AudioSource>();
        LoadMusicAndSoundVolume();
        audioSource.Play();
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        audioSource.Stop();
        if (obj){
            PlayWinSound();
        }
        else{
            PlayLoseSound();
        }
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound[Random.Range(0, winSound.Length)], soundVolume);
    }
    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound[Random.Range(0, loseSound.Length)], soundVolume);
    }
    private void ChangeAudioSourceVolume(float volume)
    {
        audioSource.volume = volume;
    }
    private void ChangeSoundFXVolume(float volume)
    {
        soundVolume = volume;
    }

    public void SaveMusicSoundVolume()
    {
        string loadString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);
        saveProps.musicVolume = audioSource.volume;
        saveProps.soundVolume = soundVolume;
        string newLoad = JsonUtility.ToJson(saveProps);
        SaveManager.Save(newLoad);
    }
    private void LoadMusicAndSoundVolume()
    {
        string loadString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);

        audioSource.volume = saveProps.musicVolume;
        if(musicSlider != null)
        musicSlider.value = saveProps.musicVolume;

        soundVolume = saveProps.soundVolume;
        if(soundSlider != null)
        soundSlider.value = saveProps.soundVolume;
    }

}
