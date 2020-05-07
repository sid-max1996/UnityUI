using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    bool isWild;
    Canvas canvas;
    Button btn1;
    Button btn2;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        btn1 = GameObject.Find("Button1").GetComponent<Button>();
        btn2 = GameObject.Find("Button2").GetComponent<Button>();
        isWild = true;
        btn1.onClick.AddListener(ChangeState);
        btn2.onClick.AddListener(WildClick);
    }

    private void OnRectTransformDimensionsChange()
    {
        Vector3 pos = btn2.transform.localPosition;
        Rect btnRect = btn2.GetComponent<RectTransform>().rect;
        pos.x = 0 - btnRect.width / 2;
        pos.y = 0 - btnRect.height / 2;
        btn2.transform.localPosition = pos;
    }

    public void ChangeState ()
    {
        Debug.Log("ChangeState");
        Text labelBtn1 = btn1.GetComponentInChildren<Text>();
        if (isWild)
        {
            isWild = false;
            labelBtn1.text = "Сброс";
            btn2.onClick.RemoveListener(WildClick);
            btn2.onClick.AddListener(QuitApplication);
        }
        else
        {
            isWild = true;
            labelBtn1.text = "Приручить";
            btn2.onClick.RemoveListener(QuitApplication);
            btn2.onClick.AddListener(WildClick);
        }
    }

    public void WildClick()
    {
        Debug.Log("WildClick");
        Vector3 pos = btn2.transform.localPosition;
        Rect btnRect = btn2.GetComponent<RectTransform>().rect;
        Rect canvasRect = canvas.GetComponent<RectTransform>().rect;
        pos.x = Random.Range(-1f, 1f) * (canvasRect.width / 2 - btnRect.width);
        pos.y = Random.Range(-1f, 1f) * (canvasRect.height / 2 - btnRect.height);
        Debug.Log("pos.x = " + pos.x);
        Debug.Log("pos.y = " + pos.y);
        btn2.transform.localPosition = pos;
    }

    public void QuitApplication()
    {
        Debug.Log("QuitApplication");
        Application.Quit();
    }
}
