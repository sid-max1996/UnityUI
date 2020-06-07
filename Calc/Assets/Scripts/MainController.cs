using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    Text operatorLabel;
    Button eqButton;

    void Awake()
    {
        operatorLabel = 
            GameObject.Find("OperatorText").GetComponent<Text>();
        eqButton = 
            GameObject.Find("Button5").GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.Equals) ||
            Input.GetKeyDown(KeyCode.KeypadEquals))
            eqButton.OnPointerClick(null);

        if (Input.GetKeyDown(KeyCode.Plus) ||
            Input.GetKeyDown(KeyCode.KeypadPlus))
            operatorLabel.text = "+";

        if (Input.GetKeyDown(KeyCode.Minus) ||
            Input.GetKeyDown(KeyCode.KeypadMinus))
            operatorLabel.text = "-";

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            operatorLabel.text = "x";

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
            operatorLabel.text = "/";
    }
}
