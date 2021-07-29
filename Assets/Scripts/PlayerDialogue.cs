using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject npc;

    private float hitTime = 0.0f;
    private float selectTime = 3.0f;
    private bool isSelecting = false;
    private int choiceId = -1;

    private void Start()
    {
        StartConversation();
    }
    void Update()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 100.0f))
        {
            if (hit.collider.gameObject.name == "Choice")
            {
                ChoiceButton choiceButton = hit.collider.GetComponent<ChoiceButton>();

                if (!isSelecting)
                {
                    isSelecting = true;
                    hitTime = Time.time;
                    choiceId = choiceButton.number;
                }
                else if (isSelecting && choiceButton.number == choiceId)
                {
                    if (hitTime + selectTime <= Time.time)
                    {
                        choiceButton.Select();
                    }
                }
                else
                {
                    resetSelecting();
                }
            }
            else
            {
                if (isSelecting)
                {
                    resetSelecting();
                }
            }
        }
    }

    void resetSelecting()
    {
        isSelecting = false;
        hitTime = 0.0f;
        choiceId = -1;
    }

    void StartConversation()
    {
        NpcDude npcScript = (NpcDude) npc.GetComponent(typeof(NpcDude));
        npcScript.initiateConversation();
    }
}
