using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button openCreditsButton;
    [SerializeField] private Button closeCreditsButton;
    [SerializeField] private GameObject creditsPanel;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        openCreditsButton.onClick.AddListener(OpenCreditsPanel);
        closeCreditsButton.onClick.AddListener(CloseCreditsPanel);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        openCreditsButton.onClick.RemoveListener(OpenCreditsPanel);
        closeCreditsButton.onClick.RemoveListener(CloseCreditsPanel);
    }

    public void OpenCreditsPanel() => creditsPanel?.SetActive(true);

    public void CloseCreditsPanel() => creditsPanel?.SetActive(false);

    public void StartGame() => SceneManager.LoadScene("GameScene");
}
