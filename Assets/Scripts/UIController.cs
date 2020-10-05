using BearLoopGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : Singleton<UIController>
{
    public TextMeshProUGUI timer;
    private TimeSpan timeSpan;
    public UINumpad numpad;
    public GameObject tutorial;

    protected override void InitSingleton()
    {
        base.InitSingleton();
    }

    public void UpdateTimer(float time)
    {
        timeSpan = TimeSpan.FromSeconds(time);
        timer.text = timeSpan.ToString(@"mm\:ss");
    }

    public void NumpadToggle(bool value)
    {
        numpad.SetEnabled(value);
    }

    public void TutorialToggle(bool value)
    {
        tutorial.SetActive(value);
    }
}
