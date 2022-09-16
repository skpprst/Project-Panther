using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncomprossCLip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameObject Page1;
    [SerializeField] private GameObject Page2;

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

    public void IWasClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        Page1.SetActive(true);
    }

    public void ClosePage1()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

    public void ClosePage2()
    {
        Page2.SetActive(false);
    }
}
