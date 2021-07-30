using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject npc;

    private float hitTime = 0.0f;
    private float selectTime = 3.0f;
    private bool isSelecting = false;
    private ChoiceButton currentChoice;

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
                    currentChoice = choiceButton;
                    hitTime = Time.time;
                    currentChoice.maxValue = selectTime;
                }
                else if (isSelecting && choiceButton.number == currentChoice.number)
                {
                    currentChoice.setCurrentValue(Time.time - hitTime);

                    if (hitTime + selectTime <= Time.time)
                    {
                        currentChoice.Select();
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
        currentChoice.setCurrentValue(0);
        isSelecting = false;
        hitTime = 0.0f;
        currentChoice = null;
    }

    void StartConversation()
    {
        NpcDude npcScript = (NpcDude) npc.GetComponent(typeof(NpcDude));
        npcScript.initiateConversation();
    }
}
