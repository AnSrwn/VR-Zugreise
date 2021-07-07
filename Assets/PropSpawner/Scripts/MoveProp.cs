using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProp : MonoBehaviour
{
    public float speed = 5.0f;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
