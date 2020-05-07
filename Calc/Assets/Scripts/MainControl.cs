using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour {

    Text operatorLabel;
    Text answerLabel;
    InputField input1;
    InputField input2;

    public void Start()
    {
        operatorLabel = GameObject.Find("OperatorText").GetComponent<Text>();
        answerLabel = GameObject.Find("AnswerText").GetComponent<Text>();
        input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
        input1.onValidateInput += CharValidate;
        input2.onValidateInput += CharValidate;
        for (int i = 1; i <= 4; i++)
        {
            Button btn = GameObject.Find("Button" + i).GetComponent<Button>();
            btn.onClick.AddListener(() => onOperatorClick(btn));
        }
        Button btn5 = GameObject.Find("Button5").GetComponent<Button>();
        btn5.onClick.AddListener(onAnswerClick);
    }

    private char CharValidate(string input, int charIndex, char c)
    {
        string newVal = input.Insert(charIndex, c.ToString());
        Debug.Log("OpValidate newVal " + newVal);
        double res;
        if (!double.TryParse(newVal, out res))
        {
            c = '\0';
        }
        return c;
    }

    public void onOperatorClick(Button btn)
    {
        Text label = btn.GetComponentInChildren<Text>();
        Debug.Log("onOperatorClick " + label.text);
        operatorLabel.text = label.text;
    }

    public void onAnswerClick()
    {
        Debug.Log("onAnswerClick");
        try
        {
            double answer = 0;
            double num1 = double.Parse(input1.text);
            double num2 = double.Parse(input2.text);
            string op = operatorLabel.text;
            switch (op)
            {
                case "+":
                    answer = num1 + num2;
                    break;
                case "-":
                    answer = num1 - num2;
                    break;
                case "x":
                    answer = num1 * num2;
                    break;
                case "/":
                    answer = num1 / num2;
                    break;
                default:
                    break;
            }
            answerLabel.text = answer.ToString();
        }
        catch
        {
            Debug.Log("Parse Error");
            answerLabel.text = "Error";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) ||
            Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadEquals))
            onAnswerClick();

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            operatorLabel.text = "+";

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            operatorLabel.text = "-";

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            operatorLabel.text = "x";

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
            operatorLabel.text = "/";

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (input1.isFocused)
                input2.ActivateInputField();
            else
                input1.ActivateInputField();
        }
    }
}
