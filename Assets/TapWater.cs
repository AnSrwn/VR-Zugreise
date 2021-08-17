using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapWater : MonoBehaviour
{
    public GameObject rotatingSphere;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotatingSphere.transform.localEulerAngles.x);
        if (rotatingSphere.transform.localEulerAngles.x > 22.5f)
        {
            meshRenderer.enabled = true;
        } else
        {
            meshRenderer.enabled = false;
        }
    }
}
