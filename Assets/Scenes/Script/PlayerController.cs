using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float roatespeed = 80f;

    public float distance;
    public GameObject target;
    public GameObject canvas;
    public GameObject Text;
    public GameObject B1;
    public GameObject B2;
    //private bool _walking = false;
    //public CharacterController _controller;
    public float _gravity = 1.5f;
    public float _yVelocity = 0.0f;
    //public float _moveSpeed = 25.0f;
    public float _jumpSpeed = 40.0f;
    Vector3 empty = new Vector3(1,0,1);
    public float sense = 5;

    public bool noduck = true;
   public bool Popup = true;
    Vector3 camerapos;
    // Start is called before the first frame update
    void Start()
    {

    }

    public float panSpeed = 20f;

    void Update()
    {
        float rotX = Input.GetAxis("Mouse X") * sense;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0); //Gets the direction

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.Scale(transform.GetComponentInChildren<Camera>().transform.forward,empty) * Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.Scale(transform.GetComponentInChildren<Camera>().transform.right, empty) * Input.GetAxis("Horizontal") * speed * Time.deltaTime, Space.World);
        }

        if (Popup) { 
            transform.Rotate(0, rotX, 0);

            _yVelocity -= Input.GetAxis("Mouse Y") *sense;
            _yVelocity = Mathf.Clamp(_yVelocity, -60, 60);
            transform.GetComponentInChildren<Camera>().transform.localRotation = Quaternion.Euler(_yVelocity, 0, 0);
        }
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (Input.GetKey("p") && distance < 5)
        {
           
            canvas.SetActive(true);
            Text.SetActive(true);
            B1.SetActive(true);
            B2.SetActive(true);
            Popup = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(noduck)
            {
                noduck = false;
            
            }
            else
            {
                noduck = true;
              

            }

        }
     
      

        camerapos = transform.GetComponentInChildren<Camera>().transform.position;

        if (noduck && !Input.GetKey(KeyCode.LeftShift))
        {

            speed = 4;
            StartCoroutine(playAnimeUp());
         

        }
        else if (Input.GetKey(KeyCode.LeftShift) && noduck)
        {
            speed = 8;
        }
        else
        {
            speed = 1f;
            StartCoroutine(playAnimeDown());
        }
            

        IEnumerator playAnimeUp()
        {
            yield return transform.transform.GetComponentInChildren<Camera>().transform.position = Vector3.Lerp(camerapos, new Vector3(camerapos.x, 3f, camerapos.z), 0.05f);

        }

        IEnumerator playAnimeDown()
        {
            yield return transform.transform.GetComponentInChildren<Camera>().transform.position = Vector3.Lerp(camerapos, new Vector3(camerapos.x, 0.5f, camerapos.z), 0.05f);

        }

        //transform.position = pos;
    }
}
