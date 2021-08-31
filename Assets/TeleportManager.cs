using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject completeArea;
    public GameObject backArea;

    public enum Event { Start, End }

    public void Dialogue(int sceneNumber, Event ev) {
        if(ev == Event.Start)
        {
            switch (sceneNumber)
            {
                case 0:
                    completeArea.SetActive(false);
                    backArea.SetActive(false);
                    break;
                case 1:
                    completeArea.SetActive(false);
                    backArea.SetActive(true);
                    break;
                case 2:
                    Debug.Log("Teleport manager not implemented for last scene");
                    break;
                default:
                    Debug.Log("Scene number " + sceneNumber + " is not supported");
                    break;
            }
        }
        else
        {
            completeArea.SetActive(true);
            backArea.SetActive(false);
        }
    }
}
