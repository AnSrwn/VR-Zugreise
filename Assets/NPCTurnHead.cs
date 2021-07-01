using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurnHead : MonoBehaviour
{
    private Animator myAnimator;
    public Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script started!");
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
        Debug.Log("Do something outside!");
        if (myCamera != null)
        {
            Debug.Log("Do something inside!");
            myAnimator.SetLookAtWeight(1, 0, 0.5f, 0.5f, 0.7f);
            myAnimator.SetLookAtPosition(myCamera.transform.position);
        }
    }
}
