using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartButton : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
    }

   public void StartGame() => SceneManager.LoadScene("GameScene");
}
