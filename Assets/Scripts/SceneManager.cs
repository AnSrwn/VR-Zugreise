using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneManager : MonoBehaviour
{
    private int _sceneNumber = 0;
    public int sceneNumber = 0;
    public event System.EventHandler SceneNumberChanged;
    public CharacterManager characterManager;
    public DialogueManager dialogueManager;
    public EnvironmentManager environmentManager;
    public AudioManager audioManager;
    public SkyManager skyManager;
    public EndingManager endingManager;

    public GameObject interactableObjects;

    // Decisions
    public bool friendlyToAttendant = true;
    public bool honestAboutTicket = true;
    public bool toyMissing = false;
    public bool spaceFriendly = true;
    public bool rocketFriendly = true;
    public bool cowFriendly = true;
    public bool aliensFriendly = true;
    public bool astronautFriendly = true;

    public string ending = "good";

    private void Start()
    {
        startFirstScene();
        //startSecondScene();
        //StartCoroutine(StartThirdScene(1));
    }

    private void Update()
    {
        if (sceneNumber != _sceneNumber)
        {
            _sceneNumber = sceneNumber;
            OnSceneChanged();
        }
    }

    private void OnSceneChanged()
    {
        switch (sceneNumber)
        {
            case 0:
                startFirstScene();
                break;

            case 1:
                startSecondScene();
                break;

            case 2:
                startThirdScene();
                break;
            case 3:
                startFourthScene();
                break;
        }
    }

    private void startFirstScene()
    {
        characterManager.startFirstScene();
        dialogueManager.sceneNumber = 0;
        audioManager.PlaySet(AudioManager.MusicSet.Woods);

        sceneNumber = 0;
    }

    private void startSecondScene()
    {
        characterManager.startSecondScene();
        dialogueManager.sceneNumber = 1;
        environmentManager.LoadOcean();

        sceneNumber = 1;
    }

    private void startThirdScene()
    {
        characterManager.startThirdScene();
        dialogueManager.sceneNumber = 2;
        audioManager.PlaySet(AudioManager.MusicSet.Space);
        skyManager.StartFadeIn(20);
        StartCoroutine(environmentManager.LoadSpace(2.0f));

        interactableObjects.SetActive(false);

        sceneNumber = 2;
    }

    private void startFourthScene()
    {
        ending = CalculateEnding();
        characterManager.startFourthScene();
        dialogueManager.sceneNumber = 3;

        if (ending == "bad")
        {
            audioManager.PlaySet(AudioManager.MusicSet.Nothing);
        } else {
            audioManager.PlaySet(AudioManager.MusicSet.Space);

        }

        sceneNumber = 3;
    }

    public IEnumerator StartThirdScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        environmentManager.RiseOcean();
        while (environmentManager.waterRising)
        {
            yield return null;
        }
        sceneNumber = 2;
    }

    private string CalculateEnding()
    {
        bool[] decisions = new bool[8] {
            friendlyToAttendant,
            honestAboutTicket,
            !toyMissing,
            spaceFriendly,
            rocketFriendly,
            cowFriendly,
            aliensFriendly,
            astronautFriendly
        };

        int goodDecisions = decisions.Count(c => c);
        int badDecisions = decisions.Count(c => !c);

        if (badDecisions <= 2)
        {
            endingManager.GoodEnding();
            return "good";
        } else if (goodDecisions <= 2)
        {
            endingManager.BadEnding();
            return "bad";
        } else
        {
            endingManager.NeutralEnding();
            return "neutral";
        }
    }
}
