using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TSClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncomprossCLip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject Page1;
    [SerializeField] private GameObject Page2;


    public GameObject[] tanks;
    public int selectedTanks = 0;

    private void Awake()
    {
        // GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().PlayMusic();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncomprossCLip);
    }


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
        Page2.SetActive(false);
        PlayerPrefs.SetInt("selectedTanks", selectedTanks);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void OpenPage1()
    {
        Page1.SetActive(true);
    }

    public void ClosePage1()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

   
}
