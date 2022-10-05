using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    int score = 0;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void ChangeScore(int coinValue) {
        score += coinValue;
        Debug.Log(score);
        return;
     }
}
