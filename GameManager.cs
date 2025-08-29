using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public Text scoreText;            // assign in Inspector
    public GameObject gameOverPanel;  // assign a hidden panel with a Restart button

    int score;
    bool isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        UpdateScoreUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ---- Temporary effect helpers ----

    public IEnumerator TempMouseSpeed(MouseController mouse, float addAmount, float duration)
    {
        if (mouse == null) yield break;
        mouse.moveSpeed += addAmount;
        yield return new WaitForSeconds(duration);
        mouse.moveSpeed -= addAmount;
    }

    public IEnumerator TempCatSpeed(CatAI cat, float addAmount, float duration)
    {
        if (cat == null) yield break;
        cat.AddSpeedModifier(addAmount);
        yield return new WaitForSeconds(duration);
        cat.AddSpeedModifier(-addAmount);
    }
}
