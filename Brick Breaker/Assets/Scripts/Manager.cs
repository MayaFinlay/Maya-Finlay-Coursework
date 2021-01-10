using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// Maya Finlay
// 10/01/2021 
// Manager Script for Brick Breaker

public class Manager : MonoBehaviour
{
    // Variables for Ball 
    public Ball ballTemplate;
    public int totalBalls = 1;

    // Variables for Lives  
    public Text livesLabel;
    private int totalLives = 3;
    public int currentLives = 0;

    // Variables for Score 
    public Text scoreLabel = null;
    private int score = 0;

    // Variables for Game Over      
    public Text gameOverLabel;
    public bool gameOver = false;

    //Variables for Audio and Brick Array
    AudioSource goAudio;
    public GameObject[] brickArray;


    // Initiallise amount of balls to spawn, Label / Audio components, Lives Counter, 
    //and Brick array
    void Start()
    {
        for (int count = 0; count < totalBalls; count++)
        {
            SpawnBall();
        }

        goAudio = GetComponent<AudioSource>();
        gameOverLabel.gameObject.SetActive(false);
        brickArray = GameObject.FindGameObjectsWithTag("Bricks");
        currentLives = totalLives;
    }


    public void FixedUpdate()
    {
        // Update brick array every frame as bricks will be removed from it 
        // as the game progresses
        brickArray = GameObject.FindGameObjectsWithTag("Bricks");

        //Check for game over conditions when all lives are lost or 
        // all bricks are broken
        if (brickArray.Length >= 1 && currentLives >= 1)
        {
            gameOver = false;
        }
        else if (brickArray.Length >= 1 && currentLives == 0)
        {
            gameOver = true;
            GameOver();
        }
        else if (brickArray.Length == 0 && currentLives >= 1)
        {
            gameOver = true;
            GameOver();
        }
    }

    // This method spawns a new ball with a random size and initial direction.
    public void SpawnBall()
    {
        // Clone the template game object
        Ball ballClone = Instantiate(ballTemplate);

        // Activate the new ball clone
        ballClone.gameObject.SetActive(true);
    }

    public void DespawnBall(Ball ballToDespawn)
    {
        // Destroy the ball game object
        Destroy(ballToDespawn.gameObject);
        ballTemplate.ballActive = false;
    }

    // Method to increase points when called upon
    //and call game over if all bricks are broken
    public void IncreaseScore(int points)
    {
        score += points;
        scoreLabel.text = score.ToString();
    }

    // Method to decrease lives when called upon
    // and call game over if lives fall below 1
    public void DecreaseLives()
    {
        if (currentLives >= 1)
        {
            currentLives--;
        }
        livesLabel.text = "Lives: " + currentLives.ToString();
    }

    // Method to hide game objects, show game over text 
    // and play audio when game over conditions are met
    public void GameOver()
    {
        goAudio.Play();
        this.GetComponent<SpriteRenderer>().enabled = false;
        gameOverLabel.gameObject.SetActive(true);
        foreach (GameObject brick in brickArray)
        {
            brick.GetComponent<SpriteRenderer>().enabled = false;

        }

        // temporarily reset gameover conditions to avoid multiple game overs
        currentLives = totalLives;
        brickArray = GameObject.FindGameObjectsWithTag("Untagged"); ;
    }
}
