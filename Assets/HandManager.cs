using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandManager : MonoBehaviour
{
    public GameObject leftWhite;
    public GameObject rightWhite;
    public GameObject leftStained;
    public GameObject rightStained;

    public void Start()
    {
        SetRenderModel(leftWhite, false);
        SetRenderModel(rightWhite, true);
    }

    public void SetRenderModel(GameObject model, bool right)
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                if (hand.handType == SteamVR_Input_Sources.RightHand && right)
                    hand.SetRenderModel(model);
                if (hand.handType == SteamVR_Input_Sources.LeftHand && !right)
                    hand.SetRenderModel(model);
            }
        }
    }
}
