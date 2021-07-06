using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    public float rotate;
    public int current;
    public float distance;
    public float distance2;
    public bool allowFollow = true;
    public PlayerController pc;
    // Use this for initialization    
    void Start() { }
    // Update is called once per frame    
    void Update()
    {
        //Debug.Log("allow" + allowFollow);

        distance = Vector3.Distance(transform.position, target[0].position);
        distance2 = Vector3.Distance(transform.position, target[1].position);
        if (transform.position != target[current].position && distance >= 3 && distance <= 14 && allowFollow)
        {
            current = 0;
            transform.GetComponent<Animator>().SetBool("stand", false);
            transform.GetComponent<Animator>().enabled = true;
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);


        }

        else if (transform.position != target[current].position && distance >= 14 && allowFollow && distance2 >= 3)
        {
            current = 1;
            transform.GetComponent<Animator>().SetBool("stand", false);
            transform.GetComponent<Animator>().enabled = true;
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);


        }
        else 
        {

            transform.GetComponent<Animator>().SetBool("stand", true);

        }
        Quaternion toRotation = Quaternion.LookRotation(target[current].position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotate * Time.deltaTime);

    }


    public void stoprunning()
    {
        allowFollow = false;
        current = 1;
    }
    public void runrunning()
    {
        allowFollow = true;
        current = 0;
    }
}