using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public SceneManager sceneManager;
    private AudioManager audioManager;
    public AudioSource audioSource;
    public GameObject child;
    public AudioSource childAudioSource;
    public GameObject window;
    public GameObject interactableObjects;
    private Animator animator;
    private bool conductorChildConversationPlayed = false;
    private bool askedForTicket = false;
    private bool gaveTicket = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        animator = GetComponent<Animator>();
        animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
        animator.SetBool(ConductorAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
        animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ConductorAnimationCondition.IS_YELLING, true);
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (sceneManager.sceneNumber == 0 && !conductorChildConversationPlayed)
                {
                    initiateConversation("ConductorChildConversation");
                    conductorChildConversationPlayed = true;
                }

                if (sceneManager.sceneNumber == 1 && !askedForTicket)
                {
                    initiateConversation("AskForTicket");
                    audioManager.PlaySet(AudioManager.MusicSet.Conductor);
                    askedForTicket = true;
                }
                break;

            case "Bird":
                break;

            case "Ticket":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !gaveTicket)
                {
                    initiateConversation("GiveTicket");
                    interactableObjects.SetActive(true);
                    audioManager.PlaySet(AudioManager.MusicSet.Conductor);
                    gaveTicket = true;
                    Destroy(other.gameObject);

                    // start third scene after 2 min.
                    StartCoroutine(sceneManager.StartThirdScene(120));
                }
                break;
        }
    }
    
    public void initiateConversation(string conversation)
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("Conductor", gameObject, audioSource));

        if (conversation == "ConductorChildConversation")
        {
            npcSpeakers.Add(new Speaker("Child", child, childAudioSource));
        }

        dialogueManager.initiateConversation(conversation, npcSpeakers);
    }

    public void playIdleAnimation()
    {
        animator.SetBool(ConductorAnimationCondition.IS_IDLE, true);
        animator.SetBool(ConductorAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
        animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
    }

    private void onMessageAction()
    {
        if ((dialogueManager.activeConversation == "AskForTicket"
            || dialogueManager.activeConversation == "GiveTicket")
           && dialogueManager.messageAction != null
           && dialogueManager.actionSpeaker == "Conductor")
        {
            switch (dialogueManager.messageAction)
            {
                case ConductorAnimationCondition.IS_IDLE:
                    print("Conductor " + ConductorAnimationCondition.IS_IDLE);
                    playIdleAnimation();
                    break;

                case ConductorAnimationCondition.IS_TALKING1:
                    print("Conductor " + ConductorAnimationCondition.IS_TALKING1);
                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, true);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
                    break;
                case ConductorAnimationCondition.IS_TALKING2:
                    print("Conductor " + ConductorAnimationCondition.IS_TALKING2);
                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, true);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
                    break;
                case ConductorAnimationCondition.IS_ANGRY:
                    print("Conductor " + ConductorAnimationCondition.IS_ANGRY);
                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, true);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
                    break;
                case ConductorAnimationCondition.IS_YELLING:
                    print("Conductor " + ConductorAnimationCondition.IS_YELLING);
                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, true);
                    break;
                case "sendWhiteRaven":
                    print("send white Raven");
                    sceneManager.honestAboutTicket = true;
                    window.SetActive(true);

                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, true);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
                    break;
                case "sendBlackRaven":
                    print("send black Raven");
                    sceneManager.honestAboutTicket = false;
                    window.SetActive(true);

                    animator.SetBool(ConductorAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING1, true);
                    animator.SetBool(ConductorAnimationCondition.IS_TALKING2, false);
                    animator.SetBool(ConductorAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ConductorAnimationCondition.IS_YELLING, false);
                    break;
                case "endTicketDialogue":
                    print("endTicketDialogue");
                    playIdleAnimation();
                    audioManager.PlaySet(AudioManager.MusicSet.Ocean);
                    break;
            }
        }
    }
}

public static class ConductorAnimationCondition
{
    public const string
        IS_IDLE = "isIdle",
        IS_TALKING1 = "isTalking1",
        IS_TALKING2 = "isTalking2",
        IS_ANGRY = "isAngry",
        IS_YELLING = "isYelling";
}
