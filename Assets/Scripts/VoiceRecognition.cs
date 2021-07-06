using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public GameObject abilitySpawner;



    void Start() {
        actions.Add("meteor", MeteorRain);
        actions.Add("meteors", MeteorRain);
        actions.Add("meteor rain", MeteorRain);
        actions.Add("meteor storm", MeteorRain);
        actions.Add("Holy light", Buff);
        actions.Add("light", Buff);
        actions.Add("lights blessing", Buff);
        actions.Add("shining light", Buff);
        actions.Add("blessing", Buff);
        actions.Add("suns blessing", Buff);
        actions.Add("charge", Charge);
        actions.Add("kill them all", Charge);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); //keys = strings
        Debug.Log("Started KeywordRecognizer" /*with words " + string.Join("\n", actions.Keys.ToArray())*/);
        //printMicrophones();

        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }


    private void printMicrophones()
    {
        foreach(var item in Microphone.devices)
        {
            Debug.Log(item.ToString());
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void MeteorRain()
    {
        abilitySpawner.GetComponent<AbilitySpawner>().activateMeteorShower();
    }

    private void Buff()
    {
        abilitySpawner.GetComponent<AbilitySpawner>().activateBuff();
    }

    private void Charge()
    {
        Debug.Log("Charge Attack Function");
    }
}
