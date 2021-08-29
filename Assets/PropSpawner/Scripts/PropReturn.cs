using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropReturn : MonoBehaviour
{
    private PropPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<PropPool>();
    }

    private void OnDisable()
    {
        if(objectPool!= null)
            this.gameObject.transform.position = new Vector3();
            objectPool.ReturnGameObject(this.gameObject);
    }
}
