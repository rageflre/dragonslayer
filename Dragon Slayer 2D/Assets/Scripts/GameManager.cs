using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player;
    public Image[] healthSprites;
    public Text scoreText, timeText;

    public int currentHealth
    {
        get;
        set;
    }

    public float invincibleTime
    {
        get;
        set;
    }

    public int currentScore
    {
        get;
        set;
    }

    public float currentTime
    {
        get;
        set;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        currentHealth = 3;
        currentScore = 0;
        currentTime = 0;
    }

    private void Update()
    {
        scoreText.text = "Score: " + currentScore;
        timeText.text = "Time: " + Mathf.Round(currentTime);
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
    }

    public void IncreaseScore(int increase)
    {
        currentScore += increase;
    }

    public void IncreaseHealth()
    {
        healthSprites[currentHealth].enabled = true;
        currentHealth++;
    }

    public void DecreaseHealth()
    {
        currentHealth--;
        healthSprites[currentHealth].enabled = false;
        invincibleTime = Time.time + 1;
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

}
