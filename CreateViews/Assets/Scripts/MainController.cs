using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    Dropdown dropdown;
    Toggle leftTg, rightTg, centerTg;
    Button createBtn;
    Button clearBtn;
    GameObject content;
    
    void Awake()
    {
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        leftTg = GameObject.Find("LeftToggle").GetComponent<Toggle>();
        rightTg = GameObject.Find("RightToggle").GetComponent<Toggle>();
        centerTg = GameObject.Find("CenterToggle").GetComponent<Toggle>();
        createBtn = GameObject.Find("CreateBtn").GetComponent<Button>();
        createBtn.onClick.AddListener(CreateBtnClick);
        clearBtn = GameObject.Find("ClearBtn").GetComponent<Button>();
        clearBtn.onClick.AddListener(ClearBtnClick);
        content = GameObject.Find("Content");
    }

    void ClearBtnClick ()
    {
        foreach (Transform child in content.transform)
            Destroy(child.gameObject);
    }

    void CreateBtnClick ()
    {
        var optionText = dropdown.options[dropdown.value].text;
        var gameObject = new GameObject();
        Image img = gameObject.AddComponent<Image>();

        if (optionText == "Red") img.color = Color.red;
        else if (optionText == "Green") img.color = Color.green;
        else if (optionText == "Blue") img.color = Color.blue;

        gameObject.transform.SetParent(content.transform);

        var rt = img.GetComponent<RectTransform>();
        var sX = transform.localScale.x;
        var sY = transform.localScale.y;
        int size = 100;
        rt.sizeDelta = new Vector2(sX * size, sY * size);
        int offsetX = 0;
        if (leftTg.isOn)
        {
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            offsetX = size / 2;
        } else if (rightTg.isOn) {
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            offsetX = -size / 2;
        } else
        {
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
        }
        rt.anchoredPosition = new Vector2(0, 0);
        Vector3 v = gameObject.transform.localPosition;
        var count = content.transform.childCount - 1;
        v.y -= (size / 2) + size * 1.2f * count;
        v.x += offsetX;
        gameObject.transform.localPosition = v;
    }
}
