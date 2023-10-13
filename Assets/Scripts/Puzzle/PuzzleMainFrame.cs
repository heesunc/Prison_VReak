using UnityEngine;

public class PuzzleMainFrame : MonoBehaviour
{
    public GameObject redLightCoverUI;
    public GameObject greenLightCoverUI;

    public void SetLightCoverUI(bool isPuzzleSolved)
    {
        if (isPuzzleSolved)
        {
            redLightCoverUI.SetActive(true);
            greenLightCoverUI.SetActive(false);
            Debug.Log("퍼즐 해결");
            // TODO 해결 사운드 실행
            PuzzleManager.instance.PuzzleSolveAfter();
        }
        else
        {
            redLightCoverUI.SetActive(false);
            greenLightCoverUI.SetActive(true);
            Debug.Log("퍼즐 틀림");
            // TODO 오답 사운드 실행

        }
    }
}
