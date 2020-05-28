using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    Text operatorLabel;
    Text answerLabel;
    InputField input1;
    InputField input2;

    void Awake()
    {
        operatorLabel = GameObject.Find("OperatorText").GetComponent<Text>();
        answerLabel = GameObject.Find("AnswerText").GetComponent<Text>();
        input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Text label = GetComponentInChildren<Text>();
        Debug.Log("operator click " + label.text);
        if (label.text == "=")
        {
            Debug.Log("onAnswerClick");
            try
            {
                double answer = 0;
                double num1 = double.Parse(input1.text);
                double num2 = double.Parse(input2.text);
                string op = operatorLabel.text;
                if (op == "+") answer = num1 + num2;
                else if (op == "-") answer = num1 - num2;
                else if (op == "x") answer = num1 * num2;
                else if (op == "/") answer = num1 / num2;
                answerLabel.text = answer.ToString();
            }
            catch
            {
                Debug.Log("Parse Error");
                answerLabel.text = "Error";
            }
        } else operatorLabel.text = label.text;
    }
}
