using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPickup : MonoBehaviour
{
    [SerializeField] ParticleSystem MissileUpgrade;
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVector;
    EnemyHealth enemyHealth;
    
   Vector3 startingPosition;
   
     float movementFactor;
     int addDamage = 150;
    // public float Radius = 1;


    private void Start()
    {
        startingPosition = transform.position;
         gameObject.SetActive(false);
        enemyHealth = FindObjectOfType<EnemyHealth>();
         Invoke("SpawnSuperBullet", 40f);
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period; // continually growing over time

         
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to 0 to 1 so its cleaner

        

        Vector3 offset = movementVector * movementFactor;    
        transform.position = startingPosition + offset;
    }



    // private void SpawnObjectAtRadom()
    // {
    //     Vector3 randomPos = Random.insideUnitCircle * Radius;

    //     Instantiate(MissileUpgrade, randomPos, Quaternion.identity);
    //     CancelInvoke();
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;

    //     Gizmos.DrawWireSphere(this.transform.position, Radius);
    // }

    private void SpawnSuperBullet()
    {
        gameObject.SetActive(true);
        
    }

    // private void OnCollisionEnter(Collision other)
    // {
         
    // }

     private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.tag == "Player")
              {
                Destroy(gameObject);

         }

        // MissileUpgrade.transform.localScale = new Vector3(5, 5, 5);
        enemyHealth.playerDamage += addDamage;
     }
}
