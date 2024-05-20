using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0000;
    public Text scoreText;
    public Texture2D cursorTexture;
    public Vector2 cursorOffset;
    public Button toMenuButton;
    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
    }
    private void Start()
    {
        toMenuButton.onClick.AddListener(delegate
        {
            SceneLoader.Instance.LoadScene(0);
        });

        GameEvents.OnCamelKnotEvent.AddListener(CamelKnotEvent);
        GameEvents.OnGameLose.AddListener(GameLose);
    }
    private void OnDestroy()
    {
        GameEvents.OnCamelKnotEvent.RemoveListener(CamelKnotEvent);
        GameEvents.OnGameLose.RemoveListener(GameLose);
    }
    private void CamelKnotEvent()
    {
        score += 1;
        scoreText.text = "000"+score.ToString();
    }
    private void GameLose()
    {
        if (PlayerPrefs.GetInt("Score") < score)
        {
            PlayerPrefs.SetInt("Score", score);
        }
    }
}
