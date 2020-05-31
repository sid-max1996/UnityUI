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
        rt.sizeDelta = new Vector2(100, 100);
        var lEdge = RectTransform.Edge.Left;
        var rEdge = RectTransform.Edge.Right;
        var tEdge = RectTransform.Edge.Top;
        if (leftTg.isOn)
            rt.SetInsetAndSizeFromParentEdge(lEdge, 0, rt.rect.width);
        else if (rightTg.isOn)
            rt.SetInsetAndSizeFromParentEdge(rEdge, 0, rt.rect.width);
        rt.SetInsetAndSizeFromParentEdge(tEdge, 0, rt.rect.height);
        Vector3 v = gameObject.transform.localPosition;
        v.x = centerTg.isOn ? 0 : v.x;
        var count = content.transform.childCount - 1;
        v.y -= rt.rect.height * 1.3f * count;
        gameObject.transform.localPosition = v;
    }
}
