using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject playerObject;
    public Image[] healthSprites;
    public Text scoreText, timeText;

    PlayerControl player;
    Color defaultColor;

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

        player = playerObject.GetComponent<PlayerControl>();

        currentHealth = 3;
        currentScore = 0;
        currentTime = 0;
    }

    private void Start()
    {
        defaultColor = player.spriteRenderer.color;
    }

    private void Update()
    {
        scoreText.text = "Score: " + currentScore;
        timeText.text = "Time: " + Mathf.Round(currentTime);
        
        if (Time.time > invincibleTime)
            player.spriteRenderer.color = defaultColor;
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

        player.spriteRenderer.color = new Color(1f, 1f, 1f, .5f);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

}
