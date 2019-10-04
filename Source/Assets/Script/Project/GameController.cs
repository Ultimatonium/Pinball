using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private Plunger plunger;
    [SerializeField]
    private StartEnder startEnder;
    [SerializeField]
    private GameObject[] startActive;
    [SerializeField]
    private GameObject[] startInactive;
    [Header("UI")]
    [SerializeField]
    private Text scoreField;
    [SerializeField]
    private Text highScoreField;
    [SerializeField]
    private GameObject gameOver;

    [HideInInspector]
    public static int score = 0;
    private static GameController instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        Application.logMessageReceived += HandleError;
    }

    private void Update()
    {
        ResetField();
        SetScore();
    }

    private void HandleError(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Log) return;
        string text = condition + "¦" + stackTrace;
        switch (type)
        {
            case LogType.Error:
                Debug.LogError(text);
                break;
            case LogType.Assert:
                Debug.LogAssertion(text);
                break;
            case LogType.Warning:
                Debug.LogWarning(text);
                break;
            case LogType.Log:
                Debug.Log(text);
                break;
            case LogType.Exception:
                Debug.LogError(text);
                break;
            default:
                Debug.LogError("unhandeld type: " + type);
                break;
        }
        Debug.Log("forced game over");
        GameOver();
    }

    public static GameController GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }
        else
        {
            Debug.LogError("GameController not ready");
            return null;
        }
    }

    private void SetScore()
    {
        scoreField.text = "Score: " + score;
    }

    private void SetHighScore()
    {
        highScoreField.text = "Highscore: " + score;
        score = 0;
    }

    public void GameOver()
    {
        DestroyAllBalls();
        gameOver.SetActive(true);
    }

    private void DestroyAllBalls()
    {
        foreach (var item in FindObjectsOfType<Ball>())
        {
            Destroy(item.gameObject);
        }
    }

    private void ResetField()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyAllBalls();
            foreach (var item in startActive)
            {
                item.SetActive(true);
            }
            foreach (var item in startInactive)
            {
                item.SetActive(false);
            }
            SetHighScore();
            startEnder.triggered = false;
            GameObject newBall = Instantiate(ballPrefab);
            plunger.ball = newBall.GetComponent<Ball>();
            gameOver.SetActive(false);
        }
    }
}
