using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    // Tail Prefab
    public GameObject tailPrefab;

    // Score
    public Text scoreText;
    int score = 0;

    // Movement Direction
    Vector2 direction = Vector2.up;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    // Should the snake be bigger?
    bool shouldBeBigger = false;

    // Can the snake change a move direction?
    bool canChangeDirection = true;

    // Speed of snake
    [Range(0.1f,0.8f)]
    public float timeToMove = 0.3f;

    // Counter of time
    private float time = 0.0f;

    // Counter of start length of the Tail
    int counterStartTail = 0;

    // Update is called once per frame
    void Update()
    {
        // After a certain time, it calls the Move function, used instead of InvokeRepeating, 
        // because of the possibility of setting the speed in the editor window.
        time += Time.deltaTime;
        if (time >= timeToMove)
        {
            Move();
            time = 0.0f;
        }

        // When Player touch left or right side of the window, the snake change his direction
        if (canChangeDirection)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                float middle = Screen.width / 2;

                if (touch.position.x < middle && touch.phase == TouchPhase.Ended)
                    MoveLeft();
                else if (touch.position.x > middle && touch.phase == TouchPhase.Ended)
                    MoveRight();
            }
        }
    }

    // If snake hit some object
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.tag == "NormalFood" || coll.tag == "SpecialFood")
        {
            // Get longer in next Move call
            shouldBeBigger = true;

            // Get a points
            if (coll.tag == "NormalFood")
            {
                score++;
                UpdateScoreText();
            }
            else if (coll.tag == "SpecialFood")
            {
                score += 10;
                UpdateScoreText();
            }

            // Save score to show it in Game Over scene
            //int temp = PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("Score", score);

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // If it isn't a food. We will run end game process.
        else
        {
            GameOver();
        }
    }

    // Load Game Over scene
    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    // Update the Score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Move Snake with tail
    void Move()
    {
        // Start with adding to Snake 4 parts of tail
        if(counterStartTail < 4)
        {
            shouldBeBigger = true;
            counterStartTail++;
        }

        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(direction);

        AddTail(ref shouldBeBigger, ref v);
        canChangeDirection = true;
    }

    // If the Player touch right side of the window
    void MoveRight()
    {
        if (direction == Vector2.up)
            direction = Vector2.right;
        else if (direction == Vector2.left)
            direction = Vector2.up;
        else if (direction == Vector2.down)
            direction = Vector2.left;
        else
            direction = Vector2.down;
    }

    // If the Player touch right side of the window
    void MoveLeft()
    {
        if (direction == Vector2.up)
            direction = Vector2.left;
        else if (direction == Vector2.left)
            direction = Vector2.down;
        else if (direction == Vector2.down)
            direction = Vector2.right;
        else
            direction = Vector2.up;
    }

    // Add part of tail
    void AddTail(ref bool shouldBeBigger, ref Vector2 v)
    {
        // Did Snake ate something?
        if (shouldBeBigger)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in tail List
            tail.Insert(0, g.transform);

            // Reset the flag
            shouldBeBigger = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of List, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
}
