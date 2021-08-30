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

    // Decisions
    public bool friendlyToAttendant = true;
    public bool honestAboutTicket = true;

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
                break;
        }
    }

    private void startFirstScene()
    {
        characterManager.startFirstScene();
        dialogueManager.sceneNumber = 0;
        sceneNumber = 0;
    }

    private void startSecondScene()
    {
        characterManager.startSecondScene();
        dialogueManager.sceneNumber = 1;
        environmentManager.LoadOcean();
        sceneNumber = 1;
    }
}
