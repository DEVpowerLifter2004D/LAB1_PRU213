using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{



    private  int score = 0;
    [SerializeField] private GameObject gameWinUi;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    private bool isGameOver = false;
    private bool isGameWin = false;
    [SerializeField] private Transform playerSpawnPoint;

    void Start()
    {
        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);

        // Tự động lấy vị trí player ban đầu nếu chưa gán
        if (playerSpawnPoint == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Tạo một GameObject làm spawn point tại vị trí player ban đầu
                GameObject spawnPoint = new GameObject("PlayerSpawnPoint");
                spawnPoint.transform.position = player.transform.position;
                playerSpawnPoint = spawnPoint.transform;
            }
        }
    }

    // Update is called once per fra me
    void Update()
    {
    }
      
    public void AddScore(int points)
    {
      if(!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
    } 


    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    } 


    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over!");
        score = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }


    public void GameWin()
    {
        if (isGameWin) return;
        isGameWin = true;
        Debug.Log("You Win!");
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }

    public void RestartGame()
    {
        // Reset game state
        isGameOver = false;
        isGameWin = false;
        score = 0;
        UpdateScore();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPoint.position;

        // reset velocity
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // reset trạng thái chết
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ResetPlayer();
        }

        // Ẩn UI và resume game
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
        Time.timeScale = 1;
    }





    public void GotoMenu ()
    {
        Debug.Log("Go to Menu");
        SceneManager.LoadScene("Menu"); 
       
        Time.timeScale = 1;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }


    public bool IsGameWin()
    {
        return isGameWin;
    }
}
