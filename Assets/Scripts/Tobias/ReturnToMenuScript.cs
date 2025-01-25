using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMenuScript : MonoBehaviour
{
    private Button returnToMenuButton;

    private void Awake()
    {
        returnToMenuButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        returnToMenuButton.onClick.AddListener(ReturnToMenuScreen);
    }

    private void OnDisable()
    {
        returnToMenuButton.onClick.RemoveListener(ReturnToMenuScreen);
    }

    public void ReturnToMenuScreen() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}
