using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int hitPoints = 100;
    [SerializeField] GameObject Deathfx;
    [SerializeField] public int enemyDamage = 35;
    [SerializeField] Canvas p_Lose;
    [SerializeField] Canvas canController;
    public int currentHealth;
    [SerializeField] HealthBars healthBars;
    [SerializeField] GameObject HitFX;
    [SerializeField] private Camera ph_camera;

    PlayerController Hide;





    private void Start()
    {
        // target = FindObjectOfType<EnemyHealth>();
        Hide = FindObjectOfType<PlayerController>();
        p_Lose.enabled = false;
        currentHealth = hitPoints;
        healthBars.SetMaxHealth(hitPoints);
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        TakeDamage(enemyDamage);
        Instantiate(HitFX, transform.position, Quaternion.identity);


    }

    public void TakeDamage(float playerDamage)
    {
        Debug.Log("Player's health reduced 35 hitpoints");

        currentHealth -= enemyDamage;
        healthBars.SetHealth(currentHealth);

        if (currentHealth <= 0)

        {
            ProcessLost();
            Hide.isPlayerDead = true;
            GameObject vfx = Instantiate(Deathfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);


        }


    }

    public void ProcessLost()
    {


        Debug.Log("Enemy wins");
        BattleSystem BS = FindObjectOfType<BattleSystem>();
        BS.state = BattleState.LOST;
        Time.timeScale = 0;
        p_Lose.enabled = true;
        canController.enabled = false;
        ph_camera.enabled = true;

    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
