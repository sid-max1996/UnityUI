using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    InputField input;
    void Awake()
    {
        input = GetComponent<InputField>();
        input.onValidateInput += CharValidate;
    }

    char CharValidate(string input, int charIndex, char c)
    {
        string newVal = input.Insert(charIndex, c.ToString());
        double res;
        bool isValid = double.TryParse(newVal, out res);
        return isValid ? c : '\0';
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !input.isFocused)
        {
            input.ActivateInputField();
        }
    }
}
