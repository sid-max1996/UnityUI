using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    Text colorText;
    Image colorRectangle;
    Slider sliderAlpha;
    Slider sliderRed;
    Slider sliderGreen;
    Slider sliderBlue;
    Slider sliderGray;

    void Awake()
    {
        colorText = 
            GameObject.Find("ColorText").GetComponent<Text>();
        colorRectangle = 
            GameObject.Find("ColorRectangle").GetComponent<Image>();
        sliderAlpha = 
            GameObject.Find("SliderAlpha").GetComponent<Slider>();
        sliderAlpha.onValueChanged.AddListener(onSliderValueChangeRGBA);
        sliderRed = 
            GameObject.Find("SliderRed").GetComponent<Slider>();
        sliderRed.onValueChanged.AddListener(onSliderValueChangeRGBA);
        sliderGreen =
            GameObject.Find("SliderGreen").GetComponent<Slider>();
        sliderGreen.onValueChanged.AddListener(onSliderValueChangeRGBA);
        sliderBlue = 
            GameObject.Find("SliderBlue").GetComponent<Slider>();
        sliderBlue.onValueChanged.AddListener(onSliderValueChangeRGBA);
        sliderGray = 
            GameObject.Find("SliderGray").GetComponent<Slider>();
        sliderGray.onValueChanged.AddListener(onSliderValueChangeGray);
    }

    void onSliderValueChangeRGBA(float value)
    {
        float r = sliderRed.value;
        float g = sliderGreen.value;
        float b = sliderBlue.value;
        float a = sliderAlpha.value;
        var newColor = new Color(r, g, b, a);
        colorRectangle.color = newColor;
        colorText.text = ColorUtility.ToHtmlStringRGBA(newColor);
    }

    void onSliderValueChangeGray(float value)
    {
        sliderRed.value = value;
        sliderGreen.value = value;
        sliderBlue.value = value;
        var newColor = 
            new Color(value, value, value, sliderAlpha.value);
        colorRectangle.color = newColor;
        colorText.text = ColorUtility.ToHtmlStringRGBA(newColor);
    }
}
