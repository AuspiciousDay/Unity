using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static readonly string FormatScore = "SCORE: {0}";
    public static readonly string FormatCoinCount = "COIN: {0}";
    public TMP_Text scoreText;
    public GameObject gameOverUI;
    public TMP_Text coinText;
    public bool IsGameOver {get; private set;}
    private int score = 0;
    private int coinCount = 0;
    private void Start()
    {
        SetScore(score);
        GetCoin(coinCount);
        gameOverUI.SetActive(false);        
    }
    private void Update()
    {
        if(IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = string.Format(FormatScore, score);
    }
    private void GetCoin(int coinCount)
    {
        this.coinCount = coinCount;
        coinText.text = string.Format(FormatCoinCount, coinCount);
    }
    public void AddScore(int add)
    {
        if (IsGameOver)
        {
            return;
        }

        SetScore(score + add);
    }
    public void AddCoin()
    {
        if (IsGameOver)
        {
            return;
        }
        GetCoin(coinCount + 1);
    }
    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
    }
}
