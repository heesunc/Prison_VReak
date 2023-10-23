using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleManager : MonoBehaviour
{
    public bool g_isPuzzleSolved = false;
    public static PuzzleManager instance;
    public GameObject[] puzzleList;
    private GameObject selectedPuzzle;
    public GameObject exitDoor;
    public GameObject exitDoorOutline;
    public GameObject puzzleButton;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    int door_speed = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            selectedPuzzle = puzzleList[Random.Range(0, puzzleList.Length)];
        }
    }

    // 상호작용한 적 없는 객체에만 외곽선 효과 주기 위한 메소드(급하니까 일단 여기다)
    // 아나 일단 퍼즐 켜져있는 동안 상호작용 막는 거로만 급하게 구현;;;
    public void UninteractedOutline()
    {
        puzzleButton.GetComponent<XRSimpleInteractable>().enabled= false;
    }

    public void PuzzleJudge(bool isPuzzleSolved)
    {
        GameObject redLightCoverUI = GameObject.Find("Red Light Cover");
        GameObject greenLightCoverUI = GameObject.Find("Green Light Cover");
        AudioSource puzzleAudio = puzzleButton.GetComponent<AudioSource>();
        if (isPuzzleSolved)
        {
            redLightCoverUI.SetActive(true);
            greenLightCoverUI.SetActive(false);
            Debug.Log("퍼즐 해결");
            puzzleAudio.clip= correctSound;
            puzzleAudio.Play();
            PuzzleSolveAfter();
        }
        else
        {
            redLightCoverUI.SetActive(false);
            greenLightCoverUI.SetActive(true);
            Debug.Log("퍼즐 틀림");
            puzzleAudio.clip = incorrectSound;
            puzzleAudio.Play();
            StartCoroutine(PuzzleIncorrectAfter(redLightCoverUI));
        }
    }

    public void ActivatePuzzle()
    {
        if (selectedPuzzle == null)
        {
            selectedPuzzle = puzzleList[Random.Range(0, puzzleList.Length)];
        }
        if (!g_isPuzzleSolved)
        {
            selectedPuzzle.SetActive(true);
        }
    }

    private IEnumerator PuzzleIncorrectAfter(GameObject coverUI)
    {
        yield return new WaitForSeconds(2f);
        coverUI.SetActive(true);
    }

    public void PuzzleSolveAfter()
    {
        g_isPuzzleSolved = true;
        exitDoorOutline.SetActive(true);
        exitDoor.GetComponent<XRSimpleInteractable>().enabled = true;
        StartCoroutine(PuzzleClose());
    }

    public void ExitDoorOpen(GameObject doorObj)
    {
        if (g_isPuzzleSolved)
        {
            doorObj.GetComponent<AudioSource>().Play();
            StartCoroutine(RotateDoor(doorObj));
            doorObj.GetComponent<XRSimpleInteractable>().enabled = false;
            //g_isPuzzleSolved = false;
        }
    }

    private IEnumerator PuzzleClose()
    {
        yield return new WaitForSeconds(2f);
        selectedPuzzle.SetActive(false);
    }

    private IEnumerator RotateDoor(GameObject doorObj)
    {
        if (door_speed == 0)
        {
            while (door_speed < 72)
            {
                doorObj.transform.rotation = Quaternion.Euler(0, door_speed - 72, 0);
                door_speed += 2;
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

}
