using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    #if !UNITY_WEBGL
using UnityEngine.Windows.Speech;
    #endif
using System;
using System.Linq;

public class VoiceRecognition : MonoBehaviour
{
    public GameObject abilitySpawner;

#if !UNITY_WEBGL
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

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
        actions.Add("leit", Buff);
        actions.Add("blessing", Buff);
        actions.Add("Segen", Buff);
        actions.Add("Heiliges Licht", Buff);
        actions.Add("Licht", Buff);
        actions.Add("baff", Buff);
        actions.Add("Licht Segen", Buff);
        actions.Add("H�llenfeuer", Hellfire);
        actions.Add("H�llenbrand", Hellfire);
        actions.Add("Brand", Hellfire);
        actions.Add("vorher", Hellfire);
        actions.Add("Feuer", Hellfire);
        actions.Add("fire", Hellfire);
        actions.Add("hellfire", Hellfire);
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
#endif

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
