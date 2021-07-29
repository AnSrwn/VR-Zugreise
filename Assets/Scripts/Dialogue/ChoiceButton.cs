using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public int number;
    public DialogueManager dialogueManager;

    public void Select() => dialogueManager.Choose(number);
}
