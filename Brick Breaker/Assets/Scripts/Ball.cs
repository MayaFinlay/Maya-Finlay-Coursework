using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Maya Finlay
// 10/01/2021 
// Ball Script for Brick Breaker

public class Ball : MonoBehaviour
{    
    // Variables the class components to be used 
    public Manager manager = null;
    public Bat bat;
    public Rigidbody2D rbBall;
    public Transform batPosition;

    // Variables for Ball Speed / Direction
    public float size = 1.0f;
    public float speed = 0.2f;
    public float directionX = 0.5f;
    public float directionY = -1.0f;

    // Variable for checking if ball is active
    public bool ballActive;

    // Variables for audio
    AudioSource breakAudio;

    // Initiallise Audio and Rigidbody components
    void Start()
    {     
        rbBall = GetComponent<Rigidbody2D>();
        breakAudio = GetComponent<AudioSource>();
    }
    
    // Respawn ball back at the paddle and randomise starting x direction
     void Update()
    {
        if (!ballActive)
        {
            transform.localPosition = batPosition.position;            
        }

        // set ball to move up on spacebar push
        if (Input.GetButtonDown("Jump") && !ballActive)
        {
            rbBall.AddForce(Vector2.up * (speed / 2));
            ballActive = true;
        }
    }

    // Fixed Update function to update bat speed / position
    void FixedUpdate()
    {
        // Calculate bat's position and speed
        Vector3 position = transform.localPosition;
        position.x += speed * directionX;
        position.y += speed * directionY;
        transform.localPosition = position;
    }

    // This method is run by Unity whenever the ball hits something. The 'other' parameter
    // contains details about the collision, including the other game object that was hit.
    void OnCollisionEnter2D(Collision2D other)
    {        
        // Invert the vertical and horizontal direction of the ball
        // on collision with bricks, increase score, and delete ball
        if (other.transform.CompareTag("Bricks"))
        {
            directionX = -directionX;
            directionY = -directionY;
            Destroy(other.gameObject);
            breakAudio.Play();
            manager.IncreaseScore(10);
        }
            
        else
        { // Compare other game object names to each of the cases
          // within the switch. If the other game object name matches one of the cases then
          // it will run all the statements, until the break statement
            switch (other.gameObject.name)
            {
                // Invert the vertical direction of the ball on collision with bat or top wall
                case "Bat":
                case "Top Wall":
                    directionY = -directionY;
                    break;

                // Invert the horizontal direction of the ball on collision with side walls
                case "Right Wall":
                case "Left Wall":
                    directionX = -directionX;
                    break;
                
                // Lose a life and despawn the ball on collision with bottom wall
                // if all lives are lost do not respawn ball but trigger game over
                // otherwise respawn ball
                case "Bottom Wall":                    
                    manager.DecreaseLives();
                    manager.DespawnBall(this);
                    if (manager.currentLives > 0)
                    {
                        manager.SpawnBall();
                    }

                    break;

                // If the ball collides with something unlisted log information
                default:
                    Debug.Log("Ball hit" + other.gameObject);
                    break;
            }
        }        
    }    
}
