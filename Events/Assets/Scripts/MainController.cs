using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    bool isWild = true;
    Canvas canvas;
    Button btn1;
    Button btn2;
    RectTransform btn2Tr;
    RectTransform canvasTr;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        btn1 = GameObject.Find("Button1").GetComponent<Button>();
        btn2 = GameObject.Find("Button2").GetComponent<Button>();
        btn2Tr = btn2.GetComponent<RectTransform>();
        canvasTr = canvas.GetComponent<RectTransform>();
        btn1.onClick.AddListener(ChangeState);
        btn2.onClick.AddListener(WildClick);
    }

    public void ChangeState ()
    {
        if (isWild)
        {
            btn2.onClick.RemoveListener(WildClick);
            btn2.onClick.AddListener(QuitApplication);
        }
        else
        {
            btn2.onClick.RemoveListener(QuitApplication);
            btn2.onClick.AddListener(WildClick);
        }
        Text labelBtn1 = btn1.GetComponentInChildren<Text>();
        labelBtn1.text = isWild ? "Сброс" : "Приручить";
        isWild = !isWild;
    }

    void WildClick()
    {
        float xCoeff = canvasTr.rect.width / 2 - btn2Tr.rect.width;
        float x = Random.Range(-1f, 1f) * xCoeff;
        float yCoeff = canvasTr.rect.height / 2 - btn2Tr.rect.height;
        float y = Random.Range(-1f, 1f) * yCoeff;
        btn2.transform.localPosition = new Vector3(x, y, 0);
    }

    void QuitApplication()
    {
        Application.Quit();
    }

    void OnRectTransformDimensionsChange()
    {
        if (btn2Tr == null) return;
        float x = 0 - btn2Tr.rect.width / 2;
        float y = 0 - btn2Tr.rect.height / 2;
        btn2.transform.localPosition = new Vector3(x, y, 0);
    }
}
