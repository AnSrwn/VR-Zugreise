using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager manager = FindObjectOfType<AudioManager>();
        //manager.Play("Train");
        StartCoroutine(setTest(manager));
    }

    IEnumerator setTest(AudioManager manager)
    {
        Debug.Log("Now Playing Woods Set");
        manager.PlaySet(AudioManager.MusicSet.Woods);
        yield return new WaitForSeconds(5);

        Debug.Log("Now Playing Bathroom Set");
        manager.PlaySet(AudioManager.MusicSet.Bathroom);
        yield return new WaitForSeconds(5);

        Debug.Log("Now Playing Conductor Set");
        manager.PlaySet(AudioManager.MusicSet.Conductor);
        yield return new WaitForSeconds(15);

        Debug.Log("Now Playing Ocean Set");
        manager.PlaySet(AudioManager.MusicSet.Ocean);
        yield return new WaitForSeconds(5);

        Debug.Log("Now Playing Space Set");
        manager.PlaySet(AudioManager.MusicSet.Space);
    }
}
