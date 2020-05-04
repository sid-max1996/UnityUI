using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
    Text mainTitle;

    private void Awake()
    {
        mainTitle = GameObject.Find("MainTitle").GetComponent<Text>();
        mainTitle.text = ScenesStore.mainSceneTitle;
        Button nextBtn = GameObject.Find("ButtonNext").GetComponent<Button>();
        nextBtn.onClick.AddListener(GoToOtherScene);
        Button settingsBtn = GameObject.Find("ButtonSettings").GetComponent<Button>();
        settingsBtn.onClick.AddListener(GoToSettingsScene);
    }

    public void GoToOtherScene ()
    {
        Debug.Log("MainScene GoToOtherScene");
        SceneManager.LoadScene("OtherScene", LoadSceneMode.Single);
    }

    public void GoToSettingsScene()
    {
        Debug.Log("MainScene GoToSettingsScene");
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Single);
    }
}
