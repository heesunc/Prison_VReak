using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderData // 각 슬라이더마다 값을 다르게 설정한 걸 저장해주기 위해 따로 class 만들어줌
{
    public Slider slider;
    public float minValue;
    public float maxValue;
}

public class SliderPuzzle : MonoBehaviour
{
    public SliderData[] slidersData;
    private bool isClear = false;

    private void Update()
    {
        // 클리어 상태가 이미 true이면 더 이상 업데이트할 필요가 없으므로 return
        if (isClear)
        {
            return;
        }

        // 모든 슬라이더가 true 상태인지 확인
        bool allSlidersTrue = true;

        foreach (SliderData sliderData in slidersData)
        {
            float sliderValue = sliderData.slider.value;

            // 설정한 MinValue와 MaxValue 범위에 도달했는지 확인
            bool isInRange = (sliderValue >= sliderData.minValue) && (sliderValue <= sliderData.maxValue);

            // 현재 슬라이더의 상태가 false라면 전체 슬라이더 상태도 false로 설정하고 루프 종료
            if (!isInRange)
            {
                allSlidersTrue = false;
                break;
            }
        }

        // 모든 슬라이더가 true 상태일 때 클리어 처리
        if (allSlidersTrue)
        {
            // 클리어 상태로 변경하고 클리어 처리를 위한 코드 작성
            isClear = true;
            Debug.Log("Clear!");
        }
    }
}