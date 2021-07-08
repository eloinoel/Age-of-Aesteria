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
        actions.Add("Meteor", MeteorRain);
        actions.Add("mitior", MeteorRain);
        actions.Add("meteors", MeteorRain);
        actions.Add("meteor rain", MeteorRain);
        actions.Add("meteor hagel", MeteorRain);
        actions.Add("Meteoritenhagel", MeteorRain);
        actions.Add("Meteorsturm", MeteorRain);
        actions.Add("Meteoritensturm", MeteorRain);
        actions.Add("Lichtschein", Buff);
        actions.Add("light", Buff);
        actions.Add("Segen", Buff);
        actions.Add("Heiliges Licht", Buff);
        actions.Add("Licht", Buff);
        actions.Add("baff", Buff);
        actions.Add("blessing", Buff);
        actions.Add("Licht Segen", Buff);
        actions.Add("Höllenfeuer", Hellfire);
        actions.Add("Höllenbrand", Hellfire);
        actions.Add("Brand", Hellfire);
        actions.Add("Feuer", Hellfire);
        actions.Add("feier", Hellfire);
        actions.Add("hellfeier", Hellfire);
        actions.Add("Flammenschwall", Hellfire);
        actions.Add("Explosion", Hellfire);

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

    private void Hellfire()
    {
        abilitySpawner.GetComponent<AbilitySpawner>().activateHellFire();
    }
}
