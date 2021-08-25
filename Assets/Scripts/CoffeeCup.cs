using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public GameObject rightHand;

    void Update()
    {
        Vector3 rightHandPosition = rightHand.transform.position;
        rightHandPosition.y = rightHandPosition.y + 0.05f;
        transform.position = rightHandPosition;
    }
}
