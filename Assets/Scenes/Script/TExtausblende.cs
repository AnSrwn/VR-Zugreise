using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TExtausblende : MonoBehaviour
{
    public PlayerController pc;
    public bool Show = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void triggerino()
    {

            Show = false;
            pc.Popup = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(Show == false){

            transform.gameObject.SetActive(false);
            Show = true;
            pc.Popup = true;
        }
    }
}
