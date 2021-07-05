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
            objectPool.ReturnGameObject(this.gameObject);
    }
}
