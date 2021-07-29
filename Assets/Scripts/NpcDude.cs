using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDude : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public AudioSource audioSource;
    public GameObject attendant;
    public AudioSource attendantAudioSource;

    public void initiateConversation()
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("Dude", gameObject, gameObject.GetComponent<AudioSource>()));
        npcSpeakers.Add(new Speaker("Attendant", attendant, attendant.GetComponent<AudioSource>()));

        dialogueManager.initiateConversation("IntroConversation", npcSpeakers);
    }
}
