using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {
    void Awake()
    {
        var input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input1.text = ScenesStore.mainSceneTitle;
        var input2 = GameObject.Find("InputField2").GetComponent<InputField>();
        input2.text = ScenesStore.otherSceneTitle;
        Button saveBtn = GameObject.Find("SaveBtn").GetComponent<Button>();
        saveBtn.onClick.AddListener(delegate {
            ScenesStore.mainSceneTitle = input1.text;
            ScenesStore.otherSceneTitle = input2.text;
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        });
        Button cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(delegate {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        });  
    }
}
