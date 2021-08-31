using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Image fill;
    public float currentValue = 0;
    public float maxValue = 100;

    public int number;
    public DialogueManager dialogueManager;

    void Start() {
        fill.fillAmount = Normalize();
    }

    private void OnEnable() {
        currentValue = 0;
        maxValue = 100;
        fill.fillAmount = Normalize();
    }

    public void setCurrentValue(float value)
    {
        currentValue = value;

        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }

        fill.fillAmount = Normalize();
    }

    private float Normalize()
    {
        return currentValue / maxValue;
    }

    public void Select() => dialogueManager.Choose(number);
}
