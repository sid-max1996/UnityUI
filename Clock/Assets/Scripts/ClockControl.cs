using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockControl : MonoBehaviour {

    Text clockText;
    Toggle checkbox;
    bool isStopClock = false;

    private void Start()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-Ru");
        clockText = GameObject.Find("TimeText").GetComponent<Text>();
        checkbox = GameObject.Find("Toggle").GetComponent<Toggle>();
    }

    void Update () {
		if (!checkbox.isOn)
        {
            clockText.text = System.DateTime.Now.ToLongTimeString();
            isStopClock = false;
        } else if (!isStopClock)
        {
            clockText.text = "00:00";
            isStopClock = true;
        }

        if (TimerStoradge.isTimer && TimerStoradge.isStart)
        {
            TimeSpan s = DateTime.Now - TimerStoradge.startTime - TimerStoradge.pauseSpan;
            clockText.text = string.Format("{0}:{1}", s.Minutes * 60 + s.Seconds, "0" + s.Milliseconds.ToString()[0]);
        }
    }
}
