using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherController : MonoBehaviour {

    private static int count = 0;

    private void Awake()
    {
        Text textField = 
            GameObject.Find("TextCount").GetComponent<Text>();
        textField.text = 
            "Количество переходов на данную сцену равно " + ++count;
        Text otherTitle = 
            GameObject.Find("OtherTitle").GetComponent<Text>();
        otherTitle.text = ScenesStore.otherSceneTitle;
        Button backBtn =
            GameObject.Find("ButtonBack").GetComponent<Button>();
        LoadSceneMode singleMode = LoadSceneMode.Single;
        backBtn.onClick.AddListener(delegate {
            SceneManager.LoadScene("MainScene", singleMode);
        });
    }
}
