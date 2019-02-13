using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public GameObject healthBarPrefab;
    GameObject healthBar;
    public int maxHealth;
    int currentHealth;
    Transform realPositon;

    public float invincibleTime
    {
        get;
        set;
    }

    float barVisible;

    //-0,005 x position for every -1 on the x scale
    //0, 0, 0 = 100%

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = Instantiate(healthBarPrefab, new Vector3(transform.position.x, transform.position.y + 0.55f), transform.rotation);
        healthBar.transform.parent = gameObject.transform;
        healthBar.SetActive(false);
    }

    private void Update()
    {
        //Calculates the width
        int width = (currentHealth * 30) / maxHealth;
        Transform currentHealthBar = healthBar.transform.GetChild(2);
        //Scales the green health bar based on  current health
        currentHealthBar.localScale = new Vector3(width, 3.5f);
        //Places the green bar on the correct position based on width
        currentHealthBar.transform.localPosition = new Vector3(0 + ((30 - width) * -0.005f), 0, 0);

        if (Time.time > invincibleTime && invincibleTime > 0)
        {
            invincibleTime = 0;
        }

        if (Time.time > barVisible && barVisible > 0)
        {
            barVisible = 0;
            healthBar.SetActive(false);
        }
    }

    public void DecreaseHealth(int damage, GameObject _gameObject)
    {
        currentHealth -= damage > currentHealth ? currentHealth : damage;
        invincibleTime = Time.time + 0.5f;
        barVisible = Time.time + 5f;
        healthBar.SetActive(true);
        if (currentHealth <= 0)
        {
            Destroy(_gameObject);
        }
    }

}
