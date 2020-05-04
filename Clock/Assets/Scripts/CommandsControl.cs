using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandsControl : MonoBehaviour {

    Text clockText;
    Toggle checkbox;
    Button toogleBtn;
    Button resetBtn;
    
    private void Start()
    {
        clockText = GameObject.Find("TimeText").GetComponent<Text>();
        checkbox = GameObject.Find("Toggle").GetComponent<Toggle>();
        checkbox.onValueChanged.AddListener(delegate { onCheckBoxToogle(); });
        toogleBtn = GameObject.Find("TimerButton").GetComponent<Button>();
        toogleBtn.onClick.AddListener(onToogleBtnClick);
        resetBtn = GameObject.Find("ResetButton").GetComponent<Button>();
        resetBtn.onClick.AddListener(onResetBtnClick);
    }

    public void onCheckBoxToogle()
    {
        if (checkbox.isOn)
        {
            toogleBtn.interactable = true;
            resetBtn.interactable = true;
            TimerStoradge.isTimer = true;
        }
        else
        {
            TimerStoradge.isTimer = false;
            TimerStoradge.isStart = false;
            TimerStoradge.isAfterPause = false;
            TimerStoradge.isFirstTime = true;
            toogleBtn.interactable = false;
            resetBtn.interactable = false;
        }
    }

    public void onToogleBtnClick()
    {
        Debug.Log("onToogleBtnClick");
        TimerStoradge.isTimer = false;
        TimerStoradge.isStart = !TimerStoradge.isStart;
        if (TimerStoradge.isStart)
        {
            if (TimerStoradge.isFirstTime)
            {
                TimerStoradge.pauseSpan = TimeSpan.Zero;
                TimerStoradge.startTime = DateTime.Now;
                TimerStoradge.isFirstTime = false;
                Debug.Log("isFirstTime");
            }
            if (TimerStoradge.isAfterPause)
            {
                TimerStoradge.pauseSpan += DateTime.Now - TimerStoradge.pauseTime;
                TimerStoradge.isAfterPause = false;
                Debug.Log("isAfterPause");
            }
            Debug.Log("Start");
        }
        else
        {
            TimerStoradge.pauseTime = DateTime.Now;
            TimerStoradge.isAfterPause = true;
            Debug.Log("Pause");
        }
        TimerStoradge.isTimer = true;
    }

    public void onResetBtnClick()
    {
        Debug.Log("onResetBtnClick");
        TimerStoradge.pauseSpan = TimeSpan.Zero;
        TimerStoradge.startTime = DateTime.Now;
        TimerStoradge.isAfterPause = false;
        clockText.text = "00:00";
    }
}
