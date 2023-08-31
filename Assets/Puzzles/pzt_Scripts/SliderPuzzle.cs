using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderData
{
    public Slider slider;
    public float minValue;
    public float maxValue;
}

public class SliderPuzzle : MonoBehaviour
{
    public SliderData[] slidersData;
    private bool isClear = false;

    public GameObject redUI;
    public GameObject black1UI;

    public GameObject SquareButton;

    private void Update()
    {
        if (isClear)
        {
            return;
        }

        bool allSlidersTrue = true;

        foreach (SliderData sliderData in slidersData)
        {
            float sliderValue = sliderData.slider.value;

            bool isInRange = (sliderValue >= sliderData.minValue) && (sliderValue <= sliderData.maxValue);

            if (!isInRange)
            {
                allSlidersTrue = false;
                break;
            }
        }

        if (allSlidersTrue)
        {
            isClear = true;
        }
    }

    public void OnSquareButtonClick()
    {
        if (isClear)
        {
            if (redUI != null)
                redUI.SetActive(false);

            if (black1UI != null)
                black1UI.SetActive(false);
        }
    }
}