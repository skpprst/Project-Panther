using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankSelection : MonoBehaviour
{
    [SerializeField] private AudioClip _compressClip, _uncomprossCLip;
    [SerializeField] private AudioSource _source;
    public GameObject[] tanks;
    public int selectedTanks = 0;

    public void NextTank()
    {
        _source.PlayOneShot(_compressClip);
        tanks[selectedTanks].SetActive(false);
        selectedTanks = (selectedTanks + 1) % tanks.Length;
        tanks[selectedTanks].SetActive(true);
    }

    public void PreviousTank()
    {
        _source.PlayOneShot(_uncomprossCLip);
        tanks[selectedTanks].SetActive(false);
        selectedTanks--;
        if (selectedTanks < 0)
        {
            selectedTanks += tanks.Length;
        }
        tanks[selectedTanks].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedTanks", selectedTanks);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
