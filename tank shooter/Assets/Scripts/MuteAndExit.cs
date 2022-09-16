using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteAndExit : MonoBehaviour
{
    private bool isMuted;

    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;

    }

    // Update is called once per frame
    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }

    public void AppQuit()
    {
        Application.Quit();
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
