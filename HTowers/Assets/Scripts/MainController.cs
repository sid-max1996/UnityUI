using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

static public class State {
    public static bool isGameEnd;
    public static int level;
    public static int stepCount;
    public static int panelHeight = 30;
}

public class MainController : MonoBehaviour
{
    public GameObject col1, col2, col3;
    public Text scoreLabel;
    public GameObject modal;
    public Text modalText;

    void Awake()
    {
        col1 = GameObject.Find("Col1");
        col2 = GameObject.Find("Col2");
        col3 = GameObject.Find("Col3");
        modal = GameObject.Find("Modal");
        modalText = GameObject.Find("ModalText").GetComponent<Text>();
        var modalBtn = 
            GameObject.Find("ModalBtn").GetComponent<Button>();
        modalBtn.onClick.AddListener(delegate { 
            modal.SetActive(false);
        });
        modal.SetActive(false);
        int dropdownValue = PlayerPrefs.GetInt("dropdownValue");
        var dropdown = 
            GameObject.Find("Dropdown").GetComponent<Dropdown>();
        dropdown.value = dropdownValue;
        dropdown.onValueChanged.AddListener(delegate
        {
            int value = dropdown.value;
            PlayerPrefs.SetInt("dropdownValue", value);
            PlayerPrefs.Save();
        });
        var rBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
        rBtn.onClick.AddListener(delegate
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        });
        scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        InitGame();
        var dBtn = GameObject.Find("DemoBtn").GetComponent<Button>();
        dBtn.onClick.AddListener(runDemo);
    }

    async Task InitGame ()
    {
        State.isGameEnd = false;
        var dropdown = 
            GameObject.Find("Dropdown").GetComponent<Dropdown>();
        string optionText = dropdown.options[dropdown.value].text;
        State.level = int.Parse(optionText);
        State.stepCount = (int)Math.Round(Math.Pow(2, State.level) - 1);
        scoreLabel.text = "Steps: " + State.stepCount;
        Color[] colors = { Color.red, Color.cyan, Color.yellow,
            Color.green, Color.blue, Color.magenta };
        foreach (var col in new GameObject[] { col1, col2, col3 })
        {
            foreach (Transform child in col.transform)
                Destroy(child.gameObject);
        }
        await Task.Delay(700);
        for (int i = 0; i < State.level; i++)
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<CanvasGroup>();
            var controller = gameObject.AddComponent<PanelController>();
            Image panel = gameObject.AddComponent<Image>();
            panel.name = "Panel";
            panel.color = colors[i % 6];
            panel.transform.SetParent(col1.transform);
            var rt = panel.GetComponent<RectTransform>();
            var colRt = col1.GetComponent<RectTransform>();
            var sX = transform.localScale.x;
            var sY = transform.localScale.y;
            var newWidth = sX * colRt.rect.width * (1f - 0.1f * i);
            int h = State.panelHeight;
            rt.sizeDelta = new Vector2(newWidth, sY * h);
            controller.FindPosition();
        }
        return;
    }

    async Task Step
        (int n, GameObject src, GameObject dst, GameObject tmp)
    {
        if (src == null || dst == null || tmp == null)
            return;
        if (n == 0) return;
        try
        {
            await Step(n - 1, src, tmp, dst);
            var parent = src.transform;
            var ind = parent.childCount - 1;
            var child = parent.GetChild(ind).gameObject;
            child.transform.SetParent(dst.transform);
            var controller = child.GetComponent<PanelController>();
            controller.FindPosition();
            scoreLabel.text = "Steps: " + --State.stepCount;
            await Task.Delay(1500 / (State.level - 1));
            await Step(n - 1, tmp, dst, src);
        } catch (Exception err) {}
        return;
    }


    async void runDemo ()
    {
        await InitGame();
        State.isGameEnd = true;
        await Task.Delay(500);
        await Step(State.level, col1, col2, col3);
    }

    void OnRectTransformDimensionsChange()
    {
        if (col1 == null || col2 == null || col3 == null) return;
        foreach (var col in new GameObject[] { col1, col2, col3 })
        {
            foreach (Transform child in col.transform)
            {
                var o = child.gameObject;
                var c = o.GetComponent<PanelController>();
                c.FindPosition();
            }
        }
    }
}
