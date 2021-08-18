using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TapWater : MonoBehaviour
{
    public GameObject rotatingSphere;
    public HandManager handManager;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatingSphere.transform.localEulerAngles.x > 22.5f)
        {
            meshRenderer.enabled = true;
        } else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(meshRenderer.enabled)
        {
            name = other.gameObject.name;
            Match matchRight = Regex.Match(name, "^finger.*r$");
            Match matchLeft = Regex.Match(name, "^finger.*l$");
            if (matchRight.Success)
            {
                handManager.SetRenderModel(handManager.rightRenderModel, true);
            }
            else if(matchLeft.Success)
            {
                handManager.SetRenderModel(handManager.leftRenderModel, false);
            }
        }
    }
}
