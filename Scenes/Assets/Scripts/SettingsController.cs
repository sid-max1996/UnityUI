using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour {

    InputField input1, input2;

    private void Awake()
    {
        input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input1.text = ScenesStore.mainSceneTitle;
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
        input2.text = ScenesStore.otherSceneTitle;
        Button saveBtn = GameObject.Find("SaveBtn").GetComponent<Button>();
        saveBtn.onClick.AddListener(onSaveBtnClick);
        Button cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();
        cancelBtn.onClick.AddListener(onCancelBtnClick);  
    }

    public void GoToMainScene()
    {
        Debug.Log("SettingsScene GoToMainScene");
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void onSaveBtnClick()
    {
        Debug.Log("onSaveBtnClick");
        ScenesStore.mainSceneTitle = input1.text;
        ScenesStore.otherSceneTitle = input2.text;
        GoToMainScene();
    }

    public void onCancelBtnClick()
    {
        Debug.Log("onCancelBtnClick");
        GoToMainScene();
    }
}
