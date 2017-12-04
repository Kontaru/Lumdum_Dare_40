using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour {

    public string Play_Audio;
    public string[] Stop_Others;

	void Start()
    {
        AudioManager.instance.Play(Play_Audio);
        foreach (string audio in Stop_Others)
        {
            AudioManager.instance.Stop(audio);
        }
    }
}
