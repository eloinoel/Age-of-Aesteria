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

    void Start() {
        actions.Add("meteor", MeteorRain);
        actions.Add("attack", Attack);
        actions.Add("charge", Charge);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); //keys = strings
        Debug.Log("Started KeywordRecognizer with words " + actions.Keys.ToString());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void MeteorRain()
    {
        Debug.Log("MeteorRain Function");
    }

    private void Attack()
    {
        Debug.Log("Attack Buff Function");
    }

    private void Charge()
    {
        Debug.Log("Charge Attack Function");
    }
}
