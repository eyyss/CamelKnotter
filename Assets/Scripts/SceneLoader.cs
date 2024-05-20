using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public CanvasGroup group;
    public AudioSource loopMusicSource;
    public static SceneLoader Instance { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        GameEvents.OnGameLose.AddListener(GameLoseEvent);
    }
    private void OnDestroy()
    {
        GameEvents.OnGameLose.AddListener(GameLoseEvent);
    }
    private void GameLoseEvent()
    {
        StartCoroutine(t());
        IEnumerator t()
        {
            yield return new WaitForSecondsRealtime(1);
            LoadScene(1);
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void LoadScene(int buildIndex)
    {
        DOTween.To(() => group.alpha, x => group.alpha = x, 1, 1).OnComplete(delegate
        {
            SceneManager.LoadScene(buildIndex);
            DOTween.To(() => group.alpha, x => group.alpha = x, 0, 1).OnComplete(delegate
            {

            });
        });

    }
}
