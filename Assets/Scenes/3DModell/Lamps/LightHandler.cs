using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    [Range(0, 0.05f)] public float emissiveIntensity = 0f;
    public Color emissiveColor = new Color(65, 65, 65);
    public bool flicker = false;
    int randomValue;
    int time = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        randomValue = Random.Range(0, transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (flicker)
        {
            
            StartCoroutine(lightFlicker(randomValue, Random.Range(0, 10), Random.Range(0, 3)));
            time += 1;
            //Debug.Log(time);
        }


        if (!flicker && emissiveIntensity != 0 || !flicker && emissiveIntensity != 0.05)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
            }
        }
       

    }


    IEnumerator lightFlicker(int value, int flickerspeed, int waitTime)
    {
        if(time < flickerspeed + 1) { 

           
            emissiveIntensity = 0;
            transform.GetChild(value).GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
        }

        if (time > flickerspeed)
        {
            emissiveIntensity = 0.05f;
            transform.GetChild(value).GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
            yield return new WaitForSeconds(waitTime);
            time = 0;
            Debug.Log(emissiveIntensity);
        }

    }
}
