using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] HealthBars healthBars;
    [SerializeField] Canvas canController;
    [SerializeField] Canvas p_Win;
    [SerializeField] public int hitPoints = 200;
    [SerializeField] GameObject Deathfx;
    [SerializeField] public int playerDamage = 50;
    [SerializeField] GameObject HitFX;
    PlayerController Hide;
    public int currentHealth;
    public int unlockNextLevel;




    AudioSource auds;

    EnemyAI eai;


    // Start is called before the first frame update


    private void Awake()
    {
        p_Win.enabled = false;
        currentHealth = hitPoints;
        healthBars.SetMaxHealth(hitPoints);
        unlockNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }
    void Start()
    {
        auds = GetComponent<AudioSource>();

        Hide = FindObjectOfType<PlayerController>();
        eai = FindObjectOfType<EnemyAI>();

    }



    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    public void ProcessHit()
    {
        Debug.Log("Enemy's health reduced 35 hitpoints");
        TakeDamage(playerDamage);
        healthBars.SetHealth(currentHealth);
        Instantiate(HitFX, transform.position, Quaternion.identity);


    }

    public void TakeDamage(float damage)
    {

        healthBars.SetHealth(currentHealth);
        currentHealth -= playerDamage;


        if (currentHealth <= 0)
        {
            ProcessWon();
            eai.isDead = true;
            gameObject.SetActive(false);
            GameObject vfx = Instantiate(Deathfx, transform.position, Quaternion.identity);


        }
    }

    public void ProcessWon()
    {
        BattleSystem BS = FindObjectOfType<BattleSystem>();
        BS.state = BattleState.WON;
        Invoke("DelayStop", 2f);
        Debug.Log("PLAYER 1 WINS");
        p_Win.enabled = true;
        canController.enabled = false;
        Hide.CheckState();
        if (unlockNextLevel > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", unlockNextLevel);
        }

    }
    public void DelayStop()
    {
        Time.timeScale = 0;
    }



}

