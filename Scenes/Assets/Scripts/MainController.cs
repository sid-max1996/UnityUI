using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
    void Awake()
    {
        Text mainTitle = GameObject.Find("MainTitle").GetComponent<Text>();
        mainTitle.text = ScenesStore.mainSceneTitle;
        Button nextBtn = GameObject.Find("ButtonNext").GetComponent<Button>();
        nextBtn.onClick.AddListener(delegate {
            SceneManager.LoadScene("OtherScene", LoadSceneMode.Single);
        });
        Button settBtn = GameObject.Find("ButtonSettings").GetComponent<Button>();
        settBtn.onClick.AddListener(delegate {
            SceneManager.LoadScene("SettingsScene", LoadSceneMode.Single);
        });
    }
}
