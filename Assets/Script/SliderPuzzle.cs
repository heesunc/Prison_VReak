using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderData // �� �����̴����� ���� �ٸ��� ������ �� �������ֱ� ���� ���� class �������
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
        // Ŭ���� ���°� �̹� true�̸� �� �̻� ������Ʈ�� �ʿ䰡 �����Ƿ� return
        if (isClear)
        {
            return;
        }

        // ��� �����̴��� true �������� Ȯ��
        bool allSlidersTrue = true;

        foreach (SliderData sliderData in slidersData)
        {
            float sliderValue = sliderData.slider.value;

            // ������ MinValue�� MaxValue ������ �����ߴ��� Ȯ��
            bool isInRange = (sliderValue >= sliderData.minValue) && (sliderValue <= sliderData.maxValue);

            // ���� �����̴��� ���°� false��� ��ü �����̴� ���µ� false�� �����ϰ� ���� ����
            if (!isInRange)
            {
                allSlidersTrue = false;
                break;
            }
        }

        // ��� �����̴��� true ������ �� Ŭ���� ó��
        if (allSlidersTrue)
        {
            // Ŭ���� ���·� �����ϰ� Ŭ���� ó���� ���� �ڵ� �ۼ�
            isClear = true;
            Debug.Log("Clear!");
        }
    }
}