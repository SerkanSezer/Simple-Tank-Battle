using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject gameFinishedPanel;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;
    [SerializeField] TextMeshProUGUI xpText;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        gameManager.OnGameFinished += GameManager_OnGameFinished;
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        ShowGameFinishedPanel(obj);
    }

    public void ShowGameFinishedPanel(bool obj)
    {
        SetXpPoint();
        if (obj) { 
            winText.gameObject.SetActive(true);
        }
        else { 
            loseText.gameObject.SetActive(true);
        }
        gameFinishedPanel.SetActive(true);
    }
    public void SetXpPoint()
    {
        xpText.text = gameManager.GetPlayerXP().ToString();
    }
    public void LoadGameScene()
    {
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }
    public void LoadMainMenu()
    {
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
