using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MainMenuVoiceCommands : MonoBehaviour, ISpeechHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        RespondToVoiceCommand(eventData.RecognizedText);
    }

    public void RespondToVoiceCommand(string command)
    {
        switch (command)
        {
            case "Video":
                break;
            case "Scans":
                break;
            case "Vitals":
                break;
            case "Render":
                break;
        }
    }
}
