using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawner;

    public bool toggle() {
        GameObject instance = GameObject.Find(spawner.name);
        if(instance != null) {
            Destroy(instance);
            return false;
        } else {
            instance = Instantiate(spawner);
            instance.name = spawner.name;
            instance.SetActive(true);
            instance.tag = spawner.name;
            return true;
        }
    }

    public void toggle(GameObject button) {
        bool spawnerExists = toggle();
        if(spawnerExists) {
            button.GetComponentInChildren<Text>().text = "Remove Spheres";
        } else {
            button.GetComponentInChildren<Text>().text = "Add Spheres";
        }
    }
}
