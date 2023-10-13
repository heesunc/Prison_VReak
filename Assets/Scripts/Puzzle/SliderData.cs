using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderData
{
    public Slider slider;
    public float minValue;
    public float maxValue;
    public Color sliderColor;

    public SliderData(Slider slider, float minValue, float maxValue, Color sliderColor)
    {
        this.slider = slider;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.sliderColor = sliderColor;
    }
}