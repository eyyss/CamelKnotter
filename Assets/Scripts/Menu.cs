using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button startButton;
    public Text scoreText;
    private void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
        startButton.onClick.AddListener(delegate
        {
            LoadGameplayScene();
        });
    }
    public void LoadGameplayScene()
    {
        SceneLoader.Instance.LoadScene(1);
    }
}
