using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public AudioSource gameOverNoise;
    public Camera mainCamera; // Reference to the main camera
    private bool isWobbling = false;

    [ContextMenu("Increment Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        // Check if the player's score is a multiple of 10
        if (playerScore % 10 == 0)
        {
            // Start the screen wobble effect
            StartCoroutine(WobbleScreen());
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (!gameOverScreen.activeSelf)
        {
            gameOverNoise.Play();
        }
        gameOverScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator WobbleScreen()
    {
        if (!isWobbling)
        {
            isWobbling = true;

            float wobbleDuration = 0.2f;
            float wobbleIntensity = 0.1f;
            Vector3 originalPosition = mainCamera.transform.position;

            float elapsedTime = 0f;
            while (elapsedTime < wobbleDuration)
            {
                // Generate random offset for screen wobble
                Vector3 randomOffset = new Vector3(Random.Range(-wobbleIntensity, wobbleIntensity), Random.Range(-wobbleIntensity, wobbleIntensity), 0f);
                mainCamera.transform.position = originalPosition + randomOffset;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset camera position after wobble effect
            mainCamera.transform.position = originalPosition;

            isWobbling = false;
        }
    }
}
