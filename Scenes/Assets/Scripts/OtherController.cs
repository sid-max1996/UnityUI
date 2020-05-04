using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherController : MonoBehaviour {

    private static int count = 0;

    private void Awake()
    {
        Debug.Log("OtherScene Awake");
        count += 1;
        Text textField = GameObject.Find("TextCount").GetComponent<Text>();
        textField.text = "Количество переходов на данную сцену равно " + count;
        Text otherTitle = GameObject.Find("OtherTitle").GetComponent<Text>();
        otherTitle.text = ScenesStore.otherSceneTitle;
        Button backBtn = GameObject.Find("ButtonBack").GetComponent<Button>();
        backBtn.onClick.AddListener(GoToMainScene);
    }

    public void GoToMainScene()
    {
        Debug.Log("OtherScene GoToMainScene");
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

}
