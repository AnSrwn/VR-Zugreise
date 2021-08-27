using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrockenGlass : MonoBehaviour
{
    public SceneManager sceneManager;
    public GameObject birdPrefab;
    public Material whiteBird;
    public Material blackBird;
    public GameObject ticketPrefab;
    public GameObject startPosition;
    public GameObject finalPosition;
    public GameObject ticketPosition;
    public AudioSource windowAudioSource;
    private GameObject bird;
    private bool useGravity = false;
    private bool ticketInstantiated = false;
    float speed = 8.0f;

    private void Start() {
        bird = (GameObject) Instantiate(birdPrefab, startPosition.transform.position, Quaternion.identity);

        if (sceneManager.friendlyToAttendant)
        {
            bird.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = whiteBird;
        } else
        {
            bird.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = blackBird;
        }
    }

    void Update()
    {
        if (useGravity)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        
        if (bird.transform.position != finalPosition.transform.position) {
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, finalPosition.transform.position, Time.deltaTime * speed);
        } else {
            if (!ticketInstantiated)
            {
                windowAudioSource.Play();

                //bird.GetComponent<Rigidbody>().useGravity = true;
                bird.transform.rotation = finalPosition.transform.rotation;
                useGravity = true;

                Instantiate(ticketPrefab, ticketPosition.transform.position, Quaternion.identity);

                ticketInstantiated = true;
            }        
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bird")
        {
            useGravity = true;
        }
    }
}
