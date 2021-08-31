using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public int sceneNumber = 0;
    public List<QuantumTek.QuantumDialogue.QD_Dialogue> dialogues = new List<QuantumTek.QuantumDialogue.QD_Dialogue>();
    public string activeConversation = "";
    public int activeActionNodeId = -1;
    public Dictionary<int, string> activeActionChoices = new Dictionary<int, string>();
    public static event Action actionChoicesAvailable;
    public string messageAction;
    public string actionSpeaker;
    public static event Action messageActionAvailable;
    public QuantumTek.QuantumDialogue.QD_DialogueHandler qdHandler;

    public GameObject floatingCanvas;
    public TextMeshProUGUI messageText;
    public List<Button> choices;

    private int currentSceneNumber = 0;
    private List<Speaker> npcSpeakers;

    private bool ended = false;

    private void Start()
    {
        floatingCanvas.SetActive(false);
        qdHandler.dialogue = dialogues[sceneNumber];
    }

    public void initiateConversation(string conversation, List<Speaker> npcSpeakers)
    {
        actionSpeaker = null;
        qdHandler.dialogue = dialogues[sceneNumber];

        this.activeConversation = conversation;
        this.npcSpeakers = npcSpeakers;

        floatingCanvas.SetActive(true);

        qdHandler.SetConversation(conversation);
        SetText();
    }

    private void endConversation()
    {
        activeConversation = "";
        activeActionNodeId = -1;
        // messageAction = null;
        // actionSpeaker = null;
        npcSpeakers = null;
        ended = false;

        floatingCanvas.SetActive(false);
    }

    private void Update()
    {
        if (currentSceneNumber != sceneNumber)
        {
            currentSceneNumber = sceneNumber;
            endConversation();
            return;
        }

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
        floatingCanvas.SetActive(true);
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
        actionSpeaker = null;
        ClearChoices();

        if (ended)
        {
            endConversation();
            return;
        }

        floatingCanvas.SetActive(true);

        // Generate choices if a choice, otherwise display the message
        if (qdHandler.currentMessageInfo.Type == QuantumTek.QuantumDialogue.QD_NodeType.Message)
        {
            QuantumTek.QuantumDialogue.QD_Message message = qdHandler.GetMessage();

            string npcName = message.SpeakerName;

            Speaker npc = npcSpeakers.Find(speaker => speaker.name == npcName);

            floatingCanvas.transform.position = npc.gameObject.transform.position;
            floatingCanvas.transform.SetParent(npc.gameObject.transform);

            string text = message.MessageText;

            if (text.Substring(0, 1) == "[") {
                actionSpeaker = npcName;
                
                int fromIndex = text.IndexOf("[") + 1;
                int toIndex = text.IndexOf("]");

                messageAction = null;
                messageAction = text.Substring(fromIndex, toIndex - fromIndex);
                messageActionAvailable?.Invoke();

                text = text.Replace("[" + messageAction + "]", "");
            }

            string endMessageAction = null;

            if (text.Substring(text.Length - 1, 1) == "]") {
                actionSpeaker = npcName;
                int fromIndex = text.IndexOf("[") + 1;
                int toIndex = text.IndexOf("]");

                endMessageAction = text.Substring(fromIndex, toIndex - fromIndex);
                text = text.Replace("[" + endMessageAction + "]", "");
            }

            messageText.text = text;
            messageText.gameObject.SetActive(true);

            if (message.Clip)
            {
                float clipLength = message.Clip.length;
                npc.audioSource.clip = message.Clip;
                npc.audioSource.Play();
                StartCoroutine(InvokeNext(clipLength, endMessageAction));
            }
        }
        else if (qdHandler.currentMessageInfo.Type == QuantumTek.QuantumDialogue.QD_NodeType.Choice)
        {
            GenerateChoices();
        }
    }

    private IEnumerator InvokeNext(float delayTime, string endMessageAction)
    {
        yield return new WaitForSeconds(delayTime);

        if (endMessageAction != null)
        {
            this.messageAction = endMessageAction;
            messageActionAvailable?.Invoke();
        }

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
