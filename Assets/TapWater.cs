using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TapWater : MonoBehaviour
{
    public SceneManager sceneManager;
    public GameObject rotatingSphere;
    public HandManager handManager;

    private MeshRenderer meshRenderer;
    private bool rightClean = false;
    private bool leftClean = false;
    private bool nextSceneTriggered = false;

    enum Hand { Left, Right, None }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatingSphere.transform.localEulerAngles.x > 22.5f)
        {
            meshRenderer.enabled = true;
        } else
        {
            meshRenderer.enabled = false;
        }

        if (rightClean && leftClean && !nextSceneTriggered)
        {
            sceneManager.sceneNumber = 1;
            nextSceneTriggered = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(meshRenderer.enabled)
        {
            Hand hand = GetHand(other.gameObject);
            if (hand == Hand.Right)
            {
                handManager.SetRenderModel(handManager.rightWhite, true);
                rightClean = true;
            }
            else if(hand == Hand.Left)
            {
                handManager.SetRenderModel(handManager.leftWhite, false);
                leftClean = true;
            }
        }
    }

    private Hand GetHand(GameObject child)
    {
        Transform t = child.transform;
        while (t.parent != null)
        {
            if (t.parent.name.Contains("HandColliderLeft"))
            {
                return Hand.Left;
            }
            else if (t.parent.name.Contains("HandColliderRight"))
            {
                return Hand.Right;
            }
            t = t.parent.transform;
        }
        return Hand.None;
    }
}
