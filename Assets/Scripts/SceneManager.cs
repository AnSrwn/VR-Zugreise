using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject interactableObjects;

    // Decisions
    public bool friendlyToAttendant = true;
    public bool honestAboutTicket = true;
    public bool toyMissing = false;

    private void Start()
    {
        startFirstScene();
        // startSecondScene();
        // StartCoroutine(StartThirdScene(1));
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
        environmentManager.LoadSpace();

        interactableObjects.SetActive(false);

        sceneNumber = 2;
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
}
