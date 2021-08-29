using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeProp : MonoBehaviour
{
    [SerializeField]
    private bool randomizeXRotation = false;
    [SerializeField]
    private bool randomizeYRotation = false;
    [SerializeField]
    private bool randomizeZRotation = false;
    [SerializeField]
    private bool randomizeHeight = false;
    [SerializeField]
    private float minHeight = 0.0f;
    [SerializeField]
    private float maxHeight = 5.0f;
    [SerializeField]
    private bool randomizeScale = false;
    [SerializeField]
    private float minScale = 0.5f;
    [SerializeField]
    private float maxScale = 2.0f;
    // Randomize Scale and Rotation
    void OnEnable()
    {
        if(randomizeScale) {
            float randomScale = Random.Range(minScale, maxScale);
            this.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
        float randomXRotation = 0f;
        float randomYRotation = 0f;
        float randomZRotation = 0f;
        if(randomizeXRotation) {
            randomXRotation = Random.Range(0, 360);
        }
        if(randomizeYRotation) {
            randomYRotation = Random.Range(0, 360);
        }
        if(randomizeZRotation) {
            randomZRotation = Random.Range(0, 360);
        }
        this.transform.Rotate(randomXRotation, randomYRotation, randomZRotation);
        if(randomizeHeight) {
            float randomHeight = Random.Range(minHeight, maxHeight);
            this.transform.position += new Vector3(0, randomHeight, 0);
        }
    }
}
