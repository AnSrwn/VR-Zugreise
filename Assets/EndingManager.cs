using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EndingManager : MonoBehaviour
{
    public SpaceManager spaceManager;
    public SkyManager skyManager;
    public bool fadeOut = false;

    public void GoodEnding()
    {
        fadeOut = true;
    }

    public void NeutralEnding()
    {
        spaceManager.ClearFantasy();
        fadeOut = true;
    }

    public void BadEnding()
    {
        spaceManager.ClearAll();
    }

    public void FadeOut()
    {
        skyManager.StartFadeOut(20);
    }
}
