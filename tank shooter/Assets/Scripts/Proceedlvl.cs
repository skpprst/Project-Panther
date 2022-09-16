using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proceedlvl : MonoBehaviour
{
    // Start is called before the first frame update
    public void ProceedToNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
