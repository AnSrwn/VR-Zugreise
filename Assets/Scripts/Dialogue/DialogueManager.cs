using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public string activeConversation = "";
    public int activeActionNodeId = -1;
    public Dictionary<int, string> activeActionChoices = new Dictionary<int, string>();
    public static event Action actionChoicesAvailable;
    public QuantumTek.QuantumDialogue.QD_DialogueHandler qdHandler;

    public GameObject floatingCanvas;
    public TextMeshProUGUI messageText;
    public List<Button> choices;

    private List<Speaker> npcSpeakers;

    private bool ended = false;

    private void Start()
    {
        floatingCanvas.SetActive(false);
    }

    public void initiateConversation(string conversation, List<Speaker> npcSpeakers)
    {
        this.activeConversation = conversation;
        this.npcSpeakers = npcSpeakers;

        floatingCanvas.SetActive(true);

        qdHandler.SetConversation(conversation);
        SetText();
    }

    private void endConversation()
    {
        activeConversation = "";
        npcSpeakers = null;

        floatingCanvas.SetActive(false);
    }

    private void Update()
    {
        if (ended)
        {
            endConversation();
            return;
        }

        // Next messge on return
        if (qdHandler.currentMessageInfo.Type == QuantumTek.QuantumDialogue.QD_NodeType.Message && Input.GetKeyUp(KeyCode.Return))
        {
            Next();
        }

        floatingCanvas.transform.rotation = Quaternion.LookRotation(floatingCanvas.transform.position - Camera.main.transform.position);
    }

    private void ClearChoices()
    {
        foreach (Button choice in choices)
        {
            choice.GetComponentInChildren<TextMeshProUGUI>().text = "";
            choice.gameObject.SetActive(false);
        }
    }

    private void GenerateChoices()
    {
        // Exit if not a choice
        if (qdHandler.currentMessageInfo.Type != QuantumTek.QuantumDialogue.QD_NodeType.Choice)
            return;
        // Clear the old choices
        ClearChoices();
        // Generate new choices
        QuantumTek.QuantumDialogue.QD_Choice choice = qdHandler.GetChoice();
        int added = 0;

        if (choice.isAction)
        {
            while (added < choice.Choices.Count)
            {
                activeActionChoices.Add(added, choice.Choices[added]);
                added++;
            }

            floatingCanvas.SetActive(false);

            activeActionNodeId = choice.ID;

            actionChoicesAvailable?.Invoke();
        }
        else
        {
            while (added < choice.Choices.Count && added < choices.Count)
            {
                Button newChoice = choices[added];
                newChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice.Choices[added];
                ChoiceButton button = newChoice.GetComponent<ChoiceButton>();
                button.number = added;
                newChoice.gameObject.SetActive(true);
                added++;
            }
        }
    }

    private void SetText()
    {
        messageText.gameObject.SetActive(false);
        messageText.text = "";
        ClearChoices();

        if (ended)
        {
            endConversation();
            return;
        }

        // Generate choices if a choice, otherwise display the message
        if (qdHandler.currentMessageInfo.Type == QuantumTek.QuantumDialogue.QD_NodeType.Message)
        {
            QuantumTek.QuantumDialogue.QD_Message message = qdHandler.GetMessage();

            string npcName = message.SpeakerName;

            Speaker npc = npcSpeakers.Find(speaker => speaker.name == npcName);

            floatingCanvas.transform.position = npc.gameObject.transform.position;
            floatingCanvas.transform.parent = npc.gameObject.transform;

            messageText.text = message.MessageText;
            messageText.gameObject.SetActive(true);

            if (message.Clip)
            {
                float clipLength = message.Clip.length;
                npc.audioSource.clip = message.Clip;
                npc.audioSource.Play();
                StartCoroutine(InvokeNext(false, clipLength));
            }
        }
        else if (qdHandler.currentMessageInfo.Type == QuantumTek.QuantumDialogue.QD_NodeType.Choice)
        {
            GenerateChoices();
        }
    }

    private IEnumerator InvokeNext(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Next();
    }

    public void Next(int choice = -1)
    {
        if (ended)
        {
            endConversation();
            return;
        }

        // Go to the next message
        qdHandler.NextMessage(choice);
        // Set the new text
        SetText();
        // End if there is no next message
        if (qdHandler.currentMessageInfo.ID < 0)
            ended = true;
    }

    public void Choose(int choice)
    {
        if (ended)
        {
            endConversation();
            return;
        }

        Next(choice);

        floatingCanvas.SetActive(true);
    }
}
