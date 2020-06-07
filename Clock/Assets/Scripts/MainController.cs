using System;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    Text clockText;
    Toggle checkbox;
    Button toogleBtn;
    Button resetBtn;
    bool isRunTimer = false;
    DateTime startTime;
    DateTime? pauseTime = null;
    TimeSpan pauseSpan;


    void Awake()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture =
            new System.Globalization.CultureInfo("ru-Ru");
        clockText = GameObject.Find("TimeText").GetComponent<Text>();
        checkbox = GameObject.Find("Toggle").GetComponent<Toggle>();
        checkbox.onValueChanged.AddListener(onCheckBoxToogle);
        toogleBtn = GameObject.Find("TimerButton").GetComponent<Button>();
        toogleBtn.onClick.AddListener(ToogleTimer);
        resetBtn = GameObject.Find("ResetButton").GetComponent<Button>();
        resetBtn.onClick.AddListener(ResetTimer);
    }

    void ToogleTimer()
    {
        isRunTimer = !isRunTimer;
        if (isRunTimer && pauseTime.HasValue)
        {
            pauseSpan += DateTime.Now - pauseTime.Value;
            pauseTime = null;
        }
        else if (!isRunTimer)
            pauseTime = DateTime.Now;
    }

    void ResetTimer()
    {
        startTime = DateTime.Now;
        pauseSpan = TimeSpan.Zero;
        pauseTime = null;
        clockText.text = "00:00";
    }

    void onCheckBoxToogle(bool val)
    {
        if (val)
        {
            toogleBtn.interactable = true;
            resetBtn.interactable = true;
            ResetTimer();
        }
        else
        {
            toogleBtn.interactable = false;
            resetBtn.interactable = false;
            isRunTimer = false;
            pauseTime = null;
        }
    }

    void Update()
    {
        if (!checkbox.isOn)
            clockText.text = System.DateTime.Now.ToLongTimeString();
        else if (isRunTimer)
        {
            TimeSpan s = DateTime.Now - startTime - pauseSpan;
            clockText.text = string.Format("{0}:{1}", s.Minutes * 60 +
                s.Seconds, "0" + s.Milliseconds.ToString()[0]);
        }
    }
}
