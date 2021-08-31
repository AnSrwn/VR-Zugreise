using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyManager : MonoBehaviour
{
    public GameObject dirLight;
    public bool fadeIn;

    private PhysicallyBasedSky sky;
    private Light lightComp;

    public enum Type { Red, Purple }

    // Start is called before the first frame update
    void Start()
    {
        Volume vol = this.gameObject.GetComponent<Volume>();
        vol.profile.TryGet<PhysicallyBasedSky>(out sky);
        lightComp = dirLight.GetComponent<Light>();
        if (fadeIn) StartFadeIn(20);
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

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
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

    IEnumerator FadeIn(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            Double x = t / duration;
            lightComp.intensity = (float)Math.Pow((float)x, 4f);
            yield return null;
        }
    }

    IEnumerator FadeOut(float duration)
    {
        float t = -duration;
        while (t < 0)
        {
            t += Time.deltaTime;
            Double x = t / duration;
            lightComp.intensity = (float)Math.Pow((float)x, 4f);
            yield return null;
        }
    }
}
