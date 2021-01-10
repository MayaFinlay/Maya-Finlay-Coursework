using System;
using UnityEngine;

// Maya Finlay
// 10/01/2021 
// Bat Script for Brick Breaker

public class Bat : MonoBehaviour
{
    // Variables to Detect Key Presses
    public KeyCode moveRightKey = KeyCode.RightArrow;
    public KeyCode moveLeftKey = KeyCode.LeftArrow;
    bool canMoveRight = true;
    bool canMoveLeft = true;    

    // Variables for Bat Speed / Direction
    public float speed = 0.2f;
    float direction = 0.0f;

    // Variables the class components to be used 
    public Rigidbody2D rbBat;
    public Manager manager = null;
    public Ball ball;   

    // Initiallise Rigid Body Audio components
    public void Start()
    {                
        rbBat = GetComponent<Rigidbody2D>();
    }

    // Fixed Update function to update conditions such as bat speed / position, 
    // brick array, and game over conditions
    public void FixedUpdate()
    {   
        // Calculate bat's position and speed     
        Vector3 position = transform.localPosition;
        position.x += speed * direction;
        transform.localPosition = position;
    }

    // Update function to detect key presses
    void Update()
    {               
        // get input from keyboard and set to command
        bool isRightPressed = Input.GetKey(moveRightKey);
        bool isLeftPressed = Input.GetKey(moveLeftKey);

        // set direction for when right key is pressed and there is no collision
        if (isRightPressed && canMoveRight)
        {
            direction = 1.0f;
        }
        // set direction for when left key is pressed and there is no collision
        else if (isLeftPressed && canMoveLeft)
        {
            direction = -1.0f;
        }
        // set direction for when no key is pressed or there is a collision
        else
        {
            direction = 0.0f;
        }        
    }
    
    // Disallow the bat from moving past the walls once colliding with them
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            case "Right Wall":
                canMoveRight = false;
                break;

            case "Left Wall":
                canMoveLeft = false;
                break;
        }
    }

    // Allow the bat to move when exiting a collision with the walls
    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.name)
        {
            case "Right Wall":
                canMoveRight = true;
                break;

            case "Left Wall":
                canMoveLeft = true;
                break;
        }
    }
    
}