using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer Settings")]
    public float roundTime = 120f; // total time in seconds
    public TextMeshProUGUI timerText; // timer on screen

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        // Count down timer
        roundTime -= Time.deltaTime;

        if (roundTime < 0f)
            roundTime = 0f;

        UpdateTimerUI();

        // Restarts the scene automatically when timer reaches 0
        if (roundTime <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(roundTime / 60f);
            int seconds = Mathf.FloorToInt(roundTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}