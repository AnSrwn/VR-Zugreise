using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHandler : MonoBehaviour
{
    public GameObject positionToMoveTo;
    public GameObject startPos;
    public GameObject[] CurrentObject;
    public float runtimerotation;
    float rotationvalue = 0;
    public bool rotate;
    public bool respawn = false;
    int currentInstances = 0;
    public Transform parent;
    public float duration;
    public GameObject rotationParent;

    void Start()
    {
        StartCoroutine(LerpPosition(CurrentObject[0], startPos.transform.position, positionToMoveTo.transform.position, duration, rotate, runtimerotation));
    }

    IEnumerator LerpPosition(GameObject CurrentObject, Vector3 startPos, Vector3 targetPosition, float duration, bool rotate, float runtimerotation)
    {
        float time = 0;


        if (currentInstances < 1)
        {

            CurrentObject = Instantiate(CurrentObject, startPos, CurrentObject.transform.rotation, parent);
            currentInstances++;
        }
        CurrentObject.transform.position = startPos;
        Vector3 startPosition = CurrentObject.transform.position;

        while (time < duration)
        {
            if (rotate)
            {
                rotationvalue += runtimerotation;
                CurrentObject.transform.localRotation = Quaternion.Euler(new Vector3(rotationvalue, rotationvalue, rotationvalue));
            }
            CurrentObject.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        if (respawn)
        {
            CurrentObject.transform.parent = rotationParent.transform;
            CurrentObject.transform.localScale = new Vector3(-100, 100, 100);
            CurrentObject.transform.localRotation = Quaternion.Euler(180, 90, 0);
        }

    }
}
