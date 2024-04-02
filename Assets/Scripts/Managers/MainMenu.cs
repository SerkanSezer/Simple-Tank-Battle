using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mask;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void LoadGameScene()
    {
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    public void OpenSettingMenu()
    {
        settingsMenu.SetActive(true);
        mask.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        mask.SetActive(false);
        audioManager.SaveMusicSoundVolume();
    }
    public void QuitGame() {
        Application.Quit();
    }

}
