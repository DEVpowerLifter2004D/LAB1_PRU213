using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);

            Debug.Log("Collected a coin! Score increased.");
        }

    else   if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
            Debug.Log("Hit a trap! Game Over!");

        }



        else if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            Debug.Log("Hit a trap! Game Over!");

        }

        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.GameWin();
            Debug.Log("Reached the goal! You win!");
        }


    }





}
