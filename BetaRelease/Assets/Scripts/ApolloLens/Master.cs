using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour {

    public Transform LiveStreamPrefab;
    private GameObject liveStreamObject;

    public GameObject MainMenuButton;
    public InputField ServerAddressInputField;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillIt()
    {
        Application.Quit();
    }

    public void LaunchVideoFeed()
    {
        liveStreamObject = Instantiate(LiveStreamPrefab, new Vector3(-1f, 0f, 2f), Quaternion.identity).gameObject;

    }

    public void KillVideoFeed()
    {
        Destroy(liveStreamObject);
    }
}
