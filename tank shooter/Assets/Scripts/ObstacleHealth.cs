using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 70;
    [SerializeField] GameObject Deathfx;
    int playerDamage = 35;

    AudioSource auds;

    int currentHitPoints = 0;
    // Start is called before the first frame update
    void OnEnable()

    {
        currentHitPoints = maxHitPoints;
    }



    void Start()
    {
        auds = GetComponent<AudioSource>();
    }



    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
          GameObject vfx = Instantiate(Deathfx, transform.position, Quaternion.identity);
        
    }

    private void ProcessHit()
    {
        Debug.Log("Obstacle Hit!");
        currentHitPoints-=playerDamage;
        if (currentHitPoints <= 0)
        {
            Debug.Log("Obstacle Destroyed!");
              gameObject.SetActive(false);

        }
    }
    }

