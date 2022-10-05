using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midtermScoreManager : MonoBehaviour
{
    public static midtermScoreManager instance;

    int score; //Increments in 1000

    int lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        Debug.Log("You have: " + lives + "Lives left.");
    }

    public void ChangeScore(int scoreValue)
    {
        score = score + scoreValue; //Adds Score value on total score
        Debug.Log("The Score is: " + score);
    }

    public void TakeDamage()
    {
        score = score - 1000;
        lives--;
        Debug.Log("The Score is: " + score);
        Debug.Log("You have: " + lives + "Lives left.");
    }

    private void Update()
    {
        if (lives == 0)
        {
            Debug.Log("Game Over. Out of Lives.");
        }
    }
}
