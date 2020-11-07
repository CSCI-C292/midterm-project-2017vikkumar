using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Dialogue _currentDialogue;
    [SerializeField] RuntimeData _runTimeData;
    int _currentSlideIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.DialogFinished += OnDialogFinished;
        GameEvents.DialogInitiated += OnDialogInitiated;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentSlideIndex < _currentDialogue.DialogSlides.Length - 1)
            {
                _currentSlideIndex++;
                ShowSlide();
            }
            else
            {
                GetComponent<Canvas>().enabled = false;
                GameEvents.InvokeDialogFinished();
            }
        }
    }
    void OnTriggerEnter()
    {
        GameEvents.InvokeDialogInitiated(_currentDialogue);
    }
    void OnDialogInitiated(object sender, DialogEventArgs args)
    {
        _currentDialogue = args.dialogPayload;
        _currentSlideIndex = 0;
        GetComponent<Canvas>().enabled = true;
        LoadAvatar();
        ShowSlide();
        _runTimeData.CurrentGameplayState = GameplayState.InDialog;
    }
    void OnDialogFinished(object sender, EventArgs args)
    {
        GetComponent<Canvas>().enabled = false;
        _runTimeData.CurrentGameplayState = GameplayState.FreeWalk;
    }
    void LoadAvatar()
    {
        GameObject avatarObj = transform.Find("Avatar").gameObject;
        avatarObj.GetComponent<RawImage>().texture = _currentDialogue.NPCAvatar;
    }

    void ShowSlide()
    {
        GameObject textObj = transform.Find("DialogText").gameObject;
        TextMeshProUGUI textComponent = textObj.GetComponent<TextMeshProUGUI>();
        textComponent.text = _currentDialogue.DialogSlides[_currentSlideIndex];
    }
}
