using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportManager : MonoBehaviour
{
    public TeleportArea completeArea;
    public TeleportArea backArea;
    public TeleportArea childArea;

    public enum Event { Start, End }

    public void Dialogue(int sceneNumber, Event ev) {
        Debug.Log("Scene " + sceneNumber + ", dialogue " + ev);
        if(ev == Event.Start)
        {
            switch (sceneNumber)
            {
                case 0:
                    completeArea.gameObject.GetComponent<MeshCollider>().enabled = false;
                    backArea.GetComponent<MeshCollider>().enabled = false;
                    childArea.GetComponent<MeshCollider>().enabled = false;
                    break;
                case 1:
                    completeArea.GetComponent<MeshCollider>().enabled = false;
                    backArea.GetComponent<MeshCollider>().enabled = true;
                    childArea.GetComponent<MeshCollider>().enabled = false;
                    break;
                case 2:
                    completeArea.GetComponent<MeshCollider>().enabled = false;
                    backArea.GetComponent<MeshCollider>().enabled = false;
                    childArea.GetComponent<MeshCollider>().enabled = true;
                    break;
                case 3:
                    completeArea.GetComponent<MeshCollider>().enabled = false;
                    backArea.GetComponent<MeshCollider>().enabled = false;
                    childArea.GetComponent<MeshCollider>().enabled = true;
                    break;
                default:
                    Debug.Log("Scene number " + sceneNumber + " is not supported");
                    break;
            }
        }
        else
        {
            completeArea.GetComponent<MeshCollider>().enabled = true;
            backArea.GetComponent<MeshCollider>().enabled = false;
            childArea.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
