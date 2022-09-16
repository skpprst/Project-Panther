using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTanks : MonoBehaviour
{
    public GameObject[] tankPrefabs;
    [SerializeField] GameObject Tank1;
    [SerializeField] GameObject Tank2;
    [SerializeField] GameObject Tank3;
    [SerializeField] GameObject Tank4;

    
    
    private void Start() {
        {
            int selectedTanks = PlayerPrefs.GetInt("selectedTanks");
            if(selectedTanks == 0)
            {
                Tank1.SetActive(true);
                
            }
            else if(selectedTanks == 1)
            {
                Tank2.SetActive(true);
            }
            else if(selectedTanks == 2)
            {
                Tank3.SetActive(true);
            }
            else if(selectedTanks == 3)
            {
                Tank4.SetActive(true);
            }
        }
    }
}
