﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyManager : MonoBehaviour
{
    public GameObject dirLight;
    private PhysicallyBasedSky sky;

    public enum Type { Red, Purple }

    // Start is called before the first frame update
    void Start()
    {
        Volume vol = this.gameObject.GetComponent<Volume>();
        vol.profile.TryGet<PhysicallyBasedSky>(out sky);
    }

    public void SetSky(Type type, float duration)
    {
        switch (type)
        {
            case Type.Red:
                StartCoroutine(RotateLight(duration, 0));
                StartCoroutine(ChangeColor(duration, new Color(0.5f, 0, 0)));
                break;
            case Type.Purple:
                StartCoroutine(RotateLight(duration, 50));
                StartCoroutine(ChangeColor(duration, new Color(0.75f, 0.5f, 1)));
                break;
            default:
                Debug.Log("Sky type " + type + " is not implemented!");
                break;
        }
    }

    IEnumerator RotateLight(float duration, float endRotation)
    {
        float startRotation = dirLight.transform.eulerAngles.x;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float xRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            dirLight.transform.eulerAngles = new Vector3(xRotation, dirLight.transform.eulerAngles.y, dirLight.transform.eulerAngles.z);
            yield return null;
        }
    }

    IEnumerator ChangeColor(float duration, Color endColor)
    {
        Color startColor = sky.horizonTint.value;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            sky.horizonTint.Override(Color.Lerp(startColor, endColor, t / duration));
            yield return null;
        }
    }
}