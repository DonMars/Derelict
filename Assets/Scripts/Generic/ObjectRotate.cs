using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectRotate : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    void Update()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
