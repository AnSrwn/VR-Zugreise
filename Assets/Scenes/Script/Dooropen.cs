using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dooropen : MonoBehaviour
{

    public float distance;
    public float rate;
    public GameObject target;
    public bool lerp = false;
    float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //void SetTransformX(float n)
    //{
    //  Vector3.Lerp(transform.position, new Vector3(n, transform.position.y, transform.position.z),2);
    //}
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        rate = 1.0f / distance * 5f;
        if (Input.GetKeyDown("p") && distance < 10)
        {
            if (lerp)
            {
                lerp = false;
            }
            else
            {
                lerp = true;
            }

        }
        if (distance > 10 && transform.rotation.y != 0)
        {
            lerp = false;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), 0.01f);
            //StartCoroutine(WaitForSec(2));
        }

        if (!lerp)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), 0.01f);
        }

        if (lerp)
        {
          
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 120, 0), 0.01f);
            
            //StartCoroutine(WaitForSec(2));

        }

    }

    IEnumerator WaitForSec(float n)
    {

        yield return new WaitForSeconds(n);
        lerp = false;
    }
}
