using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour {

    public Transform LiveStreamPrefab;
    private GameObject liveStreamObject;
    private Transform liveStreamTransform;
    private ControlScript controlScript;

    public GameObject MainMenuButton;
    public InteractiveToggle MainMenuButtonScript;
    public InputField ServerAddressInputField;

    public string ServerAddress;

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
        ServerAddress = PlayerPrefs.GetString("StreamingIP", "35.3.33.251");
        if (ServerAddressInputField.text != "")
        {
            ServerAddress = ServerAddressInputField.text;
        }
        MainMenuButtonScript = MainMenuButton.GetComponent<InteractiveToggle>();
        MainMenuButtonScript.IsEnabled = false;

        liveStreamTransform = Instantiate(LiveStreamPrefab, new Vector3(-1f, 0.166f, 3f), Quaternion.identity);
        controlScript = liveStreamTransform.Find("Control").gameObject.GetComponent<ControlScript>();
        liveStreamObject = liveStreamTransform.gameObject;
        
        controlScript.StartCall(this);
    }


    public void KillVideoFeed()
    {
        Destroy(liveStreamObject);
    }
}
