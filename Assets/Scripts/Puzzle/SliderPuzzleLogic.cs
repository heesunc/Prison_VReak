using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]

public class SliderPuzzleLogic : MonoBehaviour
{
    public SliderData[] slidersData;

    public PuzzleMainFrame puzzleMainFrame;

    public Slider Slider1;
    public Slider Slider2;
    public Slider Slider3;
    public Slider Slider4;
    public Slider Slider5;

    private bool isClear = false;

    private void Start()
    {
        SliderData[] set1 = new SliderData[]
        {
            new SliderData(Slider1, 0.4f, 0.6f, Color.yellow),
            new SliderData(Slider2, 0.4f, 0.6f, Color.black),
            new SliderData(Slider3, 0.1f, 0.3f, Color.red),
            new SliderData(Slider4, 0.1f, 0.3f, Color.black),
            new SliderData(Slider5, 0.8f, 1.0f, Color.black)
        };

        SliderData[] set2 = new SliderData[]
        {
            new SliderData(Slider1, 0.7f, 0.9f, Color.black),
            new SliderData(Slider2, 0.4f, 0.6f, Color.black),
            new SliderData(Slider3, 0.2f, 0.5f, Color.red),
            new SliderData(Slider4, 0.4f, 0.6f, Color.black),
            new SliderData(Slider5, 0.7f, 0.9f, Color.black)
        };

        SliderData[] set3 = new SliderData[]
        {
            new SliderData(Slider1, 0.4f, 0.6f, Color.black),
            new SliderData(Slider2, 0.2f, 0.4f, Color.yellow),
            new SliderData(Slider3, 0.2f, 0.4f, Color.yellow),
            new SliderData(Slider4, 0.4f, 0.6f, Color.red),
            new SliderData(Slider5, 0.2f, 0.4f, Color.black)
        };
        SliderData[] set4 = new SliderData[]
        {
            new SliderData(Slider1, 0.4f, 0.6f, Color.black),
            new SliderData(Slider2, 0.9f, 1.0f, Color.red),
            new SliderData(Slider3, 0.9f, 1.0f, Color.black),
            new SliderData(Slider4, 0.0f, 0.1f, Color.yellow),
            new SliderData(Slider5, 0.6f, 0.8f, Color.red)
        };
        SliderData[] set5 = new SliderData[]
       {
            new SliderData(Slider1, 0.0f, 0.1f, Color.black),
            new SliderData(Slider2, 0.4f, 0.6f, Color.black),
            new SliderData(Slider3, 0.2f, 0.4f, Color.black),
            new SliderData(Slider4, 0.4f, 0.6f, Color.black),
            new SliderData(Slider5, 0.0f, 0.1f, Color.black)
       };
        SliderData[] set6 = new SliderData[]
       {
            new SliderData(Slider1, 0.4f, 0.6f, Color.black),
            new SliderData(Slider2, 0.4f, 0.6f, Color.black),
            new SliderData(Slider3, 0.2f, 0.4f, Color.black),
            new SliderData(Slider4, 0.4f, 0.6f, Color.yellow),
            new SliderData(Slider5, 0.7f, 0.9f, Color.red)
       };
        SliderData[] set7 = new SliderData[]
       {
            new SliderData(Slider1, 0.4f, 0.6f, Color.red),
            new SliderData(Slider2, 0.4f, 0.6f, Color.yellow),
            new SliderData(Slider3, 0.2f, 0.4f, Color.black),
            new SliderData(Slider4, 0.4f, 0.6f, Color.black),
            new SliderData(Slider5, 0.7f, 0.9f, Color.black)
       };
        SliderData[] set8 = new SliderData[]
       {
            new SliderData(Slider1, 0.4f, 0.6f, Color.yellow),
            new SliderData(Slider2, 0.4f, 0.6f, Color.red),
            new SliderData(Slider3, 0.8f, 1.0f, Color.black),
            new SliderData(Slider4, 0.4f, 0.6f, Color.black),
            new SliderData(Slider5, 0.2f, 0.4f, Color.black)
       };
        SliderData[] set9 = new SliderData[]
       {
            new SliderData(Slider1, 0.4f, 0.6f, Color.black),
            new SliderData(Slider2, 0.0f, 0.1f, Color.black),
            new SliderData(Slider3, 0.0f, 0.1f, Color.black),
            new SliderData(Slider4, 0.8f, 1.0f, Color.black),
            new SliderData(Slider5, 0.7f, 0.9f, Color.yellow)
       };

        slidersData = UnityEngine.Random.Range(0, 9) == 0 ? set1 :
              UnityEngine.Random.Range(0, 9) == 1 ? set2 :
              UnityEngine.Random.Range(0, 9) == 2 ? set3 :
              UnityEngine.Random.Range(0, 9) == 3 ? set4 :
              UnityEngine.Random.Range(0, 9) == 3 ? set5 :
              UnityEngine.Random.Range(0, 9) == 3 ? set6 :
              UnityEngine.Random.Range(0, 9) == 3 ? set7 :
              UnityEngine.Random.Range(0, 9) == 3 ? set8 :
              set9;

        foreach (SliderData sliderData in slidersData)
        {
            if (sliderData.slider != null)
            {
                sliderData.slider.image.color = sliderData.sliderColor;
            }
        }
    }

    private void Update()
    {
        if (isClear)
        {
            return;
        }

        isClear = true;
        foreach (SliderData sliderData in slidersData)
        {
            if (sliderData.slider != null)
            {
                float sliderValue = sliderData.slider.value;
                bool isInRange = (sliderValue >= sliderData.minValue) && (sliderValue <= sliderData.maxValue);
                if (!isInRange)
                {
                    isClear = false;
                    break;
                }
            }
        }
    }
    public void OnClickSquareButton()
    {
        //CheckPuzzleSolved();                                               // 퍼즐 상태 체크
        puzzleMainFrame.SetLightCoverUI(isClear);
    }
}