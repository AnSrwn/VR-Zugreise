using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BreakDoor : MonoBehaviour
{
    public GameObject puddle;
    public GameObject staticHandle;
    public GameObject throwableHandle;
    public GameObject paper;

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        puddle.SetActive(true);
        paper.SetActive(true);
        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<CircularDrive>().minAngle = 0;
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isActive)
        {
            staticHandle.SetActive(false);
            throwableHandle.SetActive(true);
            isActive = false;
        }
    }
}
