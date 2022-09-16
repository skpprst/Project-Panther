using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelector : MonoBehaviour
{

    public Button[] lvlButtons;


    // Start is called before the first frame update
    void Start()
    {

        int levelAt = PlayerPrefs.GetInt("levelAt", 3);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 3 > levelAt)
                lvlButtons[i].interactable = false;


        }
    }

    // Update is called once per frame
    public void Level1()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Level3()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync(6);
    }
    public void Boss()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void Level6()
    {
        SceneManager.LoadSceneAsync(8);
    }
    public void Level7()
    {
        SceneManager.LoadSceneAsync(9);
    }
    public void Boss2()
    {
        SceneManager.LoadSceneAsync(10);
    }

    public void Home()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
