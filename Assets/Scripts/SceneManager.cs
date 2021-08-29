﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int sceneNumber = 0;
    public CharacterManager characterManager;
    public DialogueManager dialogueManager;
    public EnvironmentManager environmentManager;

    // Decisions
    public bool friendlyToAttendant = true;
    public bool honestAboutTicket = true;

    void Update()
    {
        switch (sceneNumber)
        {
            case 0:
                characterManager.sceneNumber = 0;
                dialogueManager.sceneNumber = 0;
                break;

            case 1:
                characterManager.sceneNumber = 1;
                dialogueManager.sceneNumber = 1;
                environmentManager.LoadOcean();
                break;
        }
    }
}
