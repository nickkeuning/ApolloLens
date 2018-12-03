using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour {

    public InputField ServerAddressInputField;

	// Use this for initialization
	void Start () {
        ServerAddressInputField.text = GetIPAddress();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private string GetIPAddress()
    {
        return PlayerPrefs.GetString("StreamingIP", "10.0.0.192");
    }

    public void OnAddressChanged()
    {
        string newAddress = ServerAddressInputField.text;
        PlayerPrefs.SetString("StreamingIP", newAddress);
    }
}
