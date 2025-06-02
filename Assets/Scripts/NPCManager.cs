using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine.Events;

public class NPCManager : MonoBehaviour
{
    Animator anim;

    [SerializeField] KeyCode talkKey = KeyCode.E;

    [SerializeField] KeyCode nextPhraseKey = KeyCode.Return;

    [SerializeField] Dialog phrases;

    bool talking = false;

    bool typingPhrase = false;

    int talkingIndex = 0;

    int talkParameter = Animator.StringToHash("IsTalking");

    [SerializeField] float talkingLettersSpeed = 0.1f;

    [SerializeField] float talkingWordSpeed = 0.5f;

    [SerializeField] bool typeWords = false;

    [SerializeField] bool blockPlayer = false;

    [SerializeField] GameObject phraseArea;

    [SerializeField] TMP_Text phraseTextArea;

    YieldInstruction _waiter;

    private static string _talkMethod = nameof(Talk);

    [SerializeField] private UnityEvent onDialogFinished;
    
    public Dialog Phrases {set => phrases = value; }
    public UnityEvent OnDialogFinished {set => onDialogFinished = value; }


    private void Start()
    {
        _waiter = new WaitForSeconds(talkingWordSpeed);
        anim = GetComponent<Animator>();
    }


    private void OnTriggerStay(Collider other)
    {
        Vector3 directionToPlayer = other.gameObject.transform.position - transform.position;
        directionToPlayer.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1);

        if (Input.GetKey(talkKey) && !talking)
        {
            //talk
            talking = true;

            phraseArea.SetActive(true);

            anim.SetBool(talkParameter, talking);

            if (blockPlayer)
            {
                PlayerManager controller = other.gameObject.GetComponent<PlayerManager>();
                if (controller)
                {
                    controller.enabled = false;
                }
            }

            StartCoroutine(Talk(phrases.Dialogs[talkingIndex]));
        }
        else if (Input.GetKey(nextPhraseKey) && talking && !typingPhrase)
        {
            if (talkingIndex + 1 < phrases.Dialogs.Length)
            {
                talkingIndex++;
                StartCoroutine(Talk(phrases.Dialogs[talkingIndex]));
            }
            else
            {
                //end of speaking
                phraseArea.SetActive(false);
                if (blockPlayer)
                {
                    PlayerManager controller = other.gameObject.GetComponent<PlayerManager>();
                    if (controller)
                    {
                        controller.enabled = true;
                    }
                }
                StopTalking();
                onDialogFinished?.Invoke();
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopTalking();
    }

    private void StopTalking()
    {
        //stop talking animation
        talking = false;
        talkingIndex = 0;
        typingPhrase = false;
        StopCoroutine(_talkMethod);
        anim.SetBool(talkParameter, talking);
        phraseArea.SetActive(false);
    }

    IEnumerator Talk(string phrase)
    {
        typingPhrase = true;

        phraseTextArea.text = "";

        if (typeWords)
        {
            string[] words = phrase.Split(" ");

            foreach (string word in words)
            {
                phraseTextArea.text += $" {word}";
                yield return _waiter;
            }
        }
        else
        {
            int length = phrase.Length;

            for (int i = 0; i < length; i++)
            {
                phraseTextArea.text += phrase[i];
                yield return new WaitForSeconds(talkingLettersSpeed);
            }
        }

        if (talkingIndex < phrases.Dialogs.Length - 1)
        {
            phraseTextArea.text += "...";
        }

        typingPhrase = false;
    }
}