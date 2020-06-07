using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [System.Serializable]
    class Task
    {
        public string text;
        public bool isDone;
    }

    class Tasks
    {
        public List<Task> list = new List<Task>();
    }

    InputField input;
    Button addBtn, deleteBtn, upBtn, downBtn;
    GameObject content, template;
    int activeInd = -1;

    void Awake ()
    {
        input = GameObject.Find("Input").GetComponent<InputField>();
        addBtn = GameObject.Find("AddBtn").GetComponent<Button>();
        addBtn.onClick.AddListener(AddBtnClick);
        deleteBtn = GameObject.Find("DeleteBtn").GetComponent<Button>();
        deleteBtn.onClick.AddListener(DeleteActiveElement);
        upBtn = GameObject.Find("UpBtn").GetComponent<Button>();
        upBtn.onClick.AddListener(delegate { MoveActiveElement(-1); });
        downBtn = GameObject.Find("DownBtn").GetComponent<Button>();
        downBtn.onClick.AddListener(delegate { MoveActiveElement(1); });
        content = GameObject.Find("Content");
        template = GameObject.Find("Template");
        template.SetActive(false);

        string json = PlayerPrefs.GetString("tasks");
        if (PlayerPrefs.HasKey("tasks")) {
            var tasks = JsonUtility.FromJson<Tasks>(json);
            foreach (var task in tasks.list)
                AddTask(task.text, task.isDone);
        }
    }

    void AddTask (string text, bool isDone = false)
    {
        var clone = Instantiate(template, content.transform);
        clone.GetComponentInChildren<Text>().text = text;
        var toogle = clone.GetComponentInChildren<Toggle>();
        toogle.isOn = isDone;
        toogle.onValueChanged.AddListener(delegate { SaveTasks(); });
        clone.name = "Task" + content.transform.childCount;
        clone.SetActive(true);

        var rt = content.GetComponent<RectTransform>();
        var rt1 = clone.GetComponent<RectTransform>();
        var newHeight = rt.rect.height + rt1.rect.height + 10;
        rt.sizeDelta = new Vector2(0, newHeight);

        var btn = clone.GetComponent<Button>();
        btn.onClick.AddListener(delegate
        {
            var ind = clone.transform.GetSiblingIndex();
            activeInd = ind != activeInd ? ind : -1;
            foreach (Transform child in content.transform)
            {
                var curInd = child.GetSiblingIndex();
                var img = child.gameObject.GetComponent<Image>();
                img.color = curInd == activeInd ? Color.gray : Color.white;
            }
        });
    }

    void SaveTasks()
    {
        var tasks = new Tasks();
        foreach (Transform child in content.transform)
        {
            var obj = child.gameObject;
            var toggle = obj.GetComponentInChildren<Toggle>();
            var label = obj.GetComponentInChildren<Text>();
            if (obj.name != "Template")
                tasks.list.Add(new Task { text = label.text, isDone = toggle.isOn });
        }
        string json = JsonUtility.ToJson(tasks);
        PlayerPrefs.SetString("tasks", json);
        PlayerPrefs.Save();
    }

    void AddBtnClick()
    {
        AddTask(input.text);
        input.text = "";
        SaveTasks();
    }

    void DeleteActiveElement()
    {
        if (activeInd != -1)
        {
            var child = content.transform.GetChild(activeInd);
            Destroy(child.gameObject);
            activeInd = -1;
            SaveTasks();
        }
    }

    void MoveActiveElement(int val)
    {
        if (activeInd != -1)
        {
            var tr = content.transform;
            var child = tr.GetChild(activeInd);
            var newInd = activeInd + val;
            if (newInd >= 1 && newInd < tr.childCount)
            {
                child.SetSiblingIndex(newInd);
                activeInd = newInd;
            }
            SaveTasks();
        }
    }
}
