using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject throwableSword;
    public GameObject playerObject, brokenCandle;
    public Image[] healthSprites;
    public Text scoreText, timeText;
    public GameObject[] pickUps;

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

    float hitTime
    {
        get;
        set;
    }

    public GameObject throwableObject
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

        if (Time.time > invincibleTime && invincibleTime > 0)
        {
            player.spriteRenderer.color = defaultColor;
            invincibleTime = 0;
        }

        if (Time.time > hitTime && hitTime > 0)
        {
            player.animator.SetBool("gothit", false);
            hitTime = 0;
        }
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
    }

    public void PickupThrowableSword()
    {
        throwableObject = throwableSword;
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
        hitTime = Time.time + 0.2f;

        player.animator.SetBool("gothit", true);

        player.spriteRenderer.color = new Color(1f, 1f, 1f, .5f);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {

        SceneManager.LoadScene(0);

    }
    public void SpawnRandomPickup(Transform _transform) 
    {
        //Calls a random pickup from the array - Anthony
        Instantiate(pickUps[Random.Range(0, pickUps.Length)],_transform.position, _transform.rotation); 
    }
    public void SpawnBrokenObject(Transform _transform, string tag)
    {
        GameObject _gameObject = null;

        switch(tag)
        {
            case "Candle":
                _gameObject = brokenCandle;
                break;
        }

        if (_gameObject == null) return;

        //Keeps looping until the int i is no longer larger than 3 - Anthony
        for (int i = 0; i < 3; i++)
        {
            _transform.TransformPoint(0, -100, 0);
            GameObject clone = Instantiate(_gameObject, _transform.position, Quaternion.identity);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            //Adds movement force in the right and upward direction - Anthony
            rb.AddForce(Vector3.right * Random.Range(-100, 50));
            rb.AddForce(Vector3.up * Random.Range(50, 150));
        }
    }
}


