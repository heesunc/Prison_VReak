using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static bool g_isPuzzleSolved = false;
    public static PuzzleManager instance;
    public GameObject[] puzzleList;
    private GameObject selectedPuzzle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            selectedPuzzle = puzzleList[Random.Range(0, puzzleList.Length)];
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
    public void PuzzleSolveAfter()
    {
        g_isPuzzleSolved = true;
        StartCoroutine(PuzzleClose());
    }

    private IEnumerator PuzzleClose()
    {
        yield return new WaitForSeconds(2f);
        selectedPuzzle.SetActive(false);
    }

    private IEnumerator RotateExitDoor()
    {
        if (true)
        {
            int i=0;
            while (i < 72)
            {
                //Prison_door.transform.rotation = Quaternion.Euler(0, door_speed + 180, 0);
                //door_speed += 2;
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

}
