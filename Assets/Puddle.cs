using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public GameObject toiletPaper;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == toiletPaper)
        {
            float oldXY = this.gameObject.transform.localScale.x;
            float oldZ = this.gameObject.transform.localScale.z;
            float newXY = oldXY - (Time.deltaTime / 50);
            if(newXY > 0)
            {
                this.gameObject.transform.localScale = new Vector3(newXY, newXY, oldZ);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
