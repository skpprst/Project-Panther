using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    public Joystick joystick;
    public float rotateHorizontal;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        rotateHorizontal = joystick.Horizontal * 1f;
        // if (rotateHorizontal >= 5f && rotateHorizontal <= 1f)
        // {
        transform.Rotate(0, rotateHorizontal, 0);
        // }
    }
}
