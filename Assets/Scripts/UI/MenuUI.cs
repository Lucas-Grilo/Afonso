using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OptionGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        SceneManager.LoadScene("Gameplay");
    }
    private void OptionGame()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        optionsPanel.SetActive(true);
    }

    private void ExitGame()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}