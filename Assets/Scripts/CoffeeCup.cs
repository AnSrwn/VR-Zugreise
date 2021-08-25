using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public GameObject handManager;
    public GameObject rightHand;
    public GameObject train;
    public GameObject CoffeeCubePrefab;
    public GameObject dropPosition1;
    public GameObject dropPosition2;
    private Transform dropPositionStart;

    private int currentDropPosition = 0;
    private bool isDroppingCoffee = false;
    private bool isShakeTrain = false;
    private List<GameObject> coffeeCubes = new List<GameObject>();

    float shakeSpeed = 0.7f;
    float shakeAmount = 2.0f;
    float dropSpeed = 3.0f;

    void Update()
    {
        // Manages the drop of the coffee cup
        if (currentDropPosition != 2 && isDroppingCoffee)
        {
            if (currentDropPosition == 0)
            {
                transform.position = Vector3.MoveTowards(dropPositionStart.transform.position, dropPosition1.transform.position, Time.deltaTime * dropSpeed);
                transform.rotation = Quaternion.Lerp(dropPositionStart.rotation, dropPosition1.transform.rotation, Time.deltaTime * dropSpeed);

                if (transform.position == dropPosition1.transform.position)
                {
                    currentDropPosition = 1;
                }
            } else if (currentDropPosition == 1)
            {
                for (int i = 0; i < 50; i++)
                {
                    GameObject cube = Instantiate(CoffeeCubePrefab, transform.position, Quaternion.identity);
                    coffeeCubes.Add(cube);
                }

                transform.position = Vector3.MoveTowards(dropPositionStart.transform.position, dropPosition2.transform.position, Time.deltaTime * dropSpeed);
                transform.rotation = Quaternion.Lerp(dropPositionStart.rotation, dropPosition2.transform.rotation, Time.deltaTime * dropSpeed);

                if (transform.position == dropPosition2.transform.position)
                {
                    currentDropPosition = 2;

                    //change hand texture
                    HandManager manager = handManager.GetComponent<HandManager>();
                    manager.SetRenderModel(manager.rightStained, true);
                    manager.SetRenderModel(manager.leftStained, false);

                    Destroy(transform.GetChild(2).gameObject);                    

                    transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    transform.GetChild(1).gameObject.GetComponent<Rigidbody>().isKinematic = false;

                    transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                    transform.GetChild(1).gameObject.GetComponent<Rigidbody>().useGravity = true;

                    coffeeCubes.ForEach(cube => {
                        Destroy(cube);
                        // cube.GetComponent<Rigidbody>().isKinematic = true;
                        // cube.GetComponent<Rigidbody>().useGravity = false;
                    });
                }
            }
            
        // Manages coffee cup position while carried
        } else if (currentDropPosition != 2 && !isDroppingCoffee) {
            Vector3 rightHandPosition = rightHand.transform.position;
            rightHandPosition.y = rightHandPosition.y + 0.05f;
            transform.position = rightHandPosition;
        }

        if (isShakeTrain) {
            Vector3 trainPosition = train.transform.position;
            trainPosition.x = Mathf.Sin(Time.deltaTime * shakeSpeed) * shakeAmount;
            train.transform.position = trainPosition;
        }
    }

    public void DropCoffee()
    {
        isDroppingCoffee = true;
        dropPositionStart = transform;
        isShakeTrain = true;
        StartCoroutine(StopShakeTrain(5.0f));
    }

    private IEnumerator StopShakeTrain(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isShakeTrain = false;
    }
}
