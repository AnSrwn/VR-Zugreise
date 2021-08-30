using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyArea : MonoBehaviour
{
    public SceneManager sceneManager;

    private int numberOfToys = 0;
    private readonly int maxNumber = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toy")
        {
            numberOfToys += 1;
            sceneManager.toyMissing = ToyMissing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Toy") {
            numberOfToys -= 1;
            sceneManager.toyMissing = ToyMissing();
        }
    }

    public bool ToyMissing()
    {
        bool result = numberOfToys < maxNumber;
        Debug.Log("Toy missing: " + result);
        return result;
    }
}
