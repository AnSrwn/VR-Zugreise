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
    public AudioSource radio;
    private Animator animator;
    private bool conductorChildConversationPlayed = false;
    private bool askedForTicket = false;
    private bool gaveTicket = false;
    private bool showedPaper = false;
    private bool showedHandle = false;
    private bool showedBird = false;
    private bool showedRadio = false;
    private List<string> shownToys = new List<string>();

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

    public void PlayConductorEndDialgoue()
    {
        if (sceneManager.ending == "good")
        {
            StartCoroutine(initiateConversationWithDelay(2.0f, "EndGoodConductor"));
        }
        else if (sceneManager.ending == "bad")
        {
            StartCoroutine(initiateConversationWithDelay(2.0f, "EndBadConductor"));
        }
        else
        {
            StartCoroutine(initiateConversationWithDelay(2.0f, "EndNeutralConductor"));
        }

    }

    private IEnumerator initiateConversationWithDelay(float delayTime, string conversation)
    {
        yield return new WaitForSeconds(delayTime);
        initiateConversation(conversation);
    }

    void OnTriggerEnter(Collider other)
    {
        // for testing reactions
        /*askedForTicket = true;
        sceneManager.honestAboutTicket = false;
        window.SetActive(true);*/

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

            case "Paper":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !showedPaper)
                {
                    showedPaper = true;
                    initiateConversation("ShowPaper");
                }
                break;

            case "Handle":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !showedHandle)
                {
                    showedHandle = true;
                    initiateConversation("ShowHandle");
                }
                break;

            case "Pigeon":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !showedBird)
                {
                    showedBird = true;
                    initiateConversation("ShowPigeon");
                }
                break;

            case "Raven":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !showedBird)
                {
                    showedBird = true;
                    initiateConversation("ShowRaven");
                }
                break;

            case "Radio":
                if (sceneManager.sceneNumber == 1 && askedForTicket && !showedRadio)
                {
                    showedRadio = true;
                    initiateConversation("ShowRadio");
                }
                break;

            case "Toy":
                switch (shownToys.Count)
                {
                    case 0:
                        shownToys.Add(other.gameObject.name);
                        initiateConversation("ShowToy1");
                        break;
                    case 1:
                        if (!shownToys.Contains(other.gameObject.name))
                        {
                            shownToys.Add(other.gameObject.name);
                            initiateConversation("ShowToy2");
                        }
                        break;
                }
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
            || dialogueManager.activeConversation == "GiveTicket"
            || dialogueManager.activeConversation == "ShowRadio")
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
                case "turnOffRadio":
                    radio.enabled = false;
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
