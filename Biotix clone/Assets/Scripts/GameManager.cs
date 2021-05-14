using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float delayForNextLevel = 2f;
    [HideInInspector] public static bool isPaused;
    public static System.Action OnEndLevel;

    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        OnEndLevel += LoadNextLevel;
    }

    static public void SetPause(bool choise)
    {
        if (choise)
        {
            Time.timeScale = 0;
            isPaused = choise;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = choise;
        }
    }

    private void LoadNextLevel()
        => StartCoroutine(NextLevelRoutine());

    private IEnumerator NextLevelRoutine()
    {
        yield return new WaitForSeconds(delayForNextLevel);

        if (CellManager.AIBotCells.Count == 0)
            print($"win - {OwnerOfCell.Player1}");
        else if (CellManager.Player1Cells.Count == 0)
            print($"win - {OwnerOfCell.AIBot}");

        while (isPaused)
            yield return new WaitForSeconds(0.5f);

        int index = SceneManager.GetActiveScene().buildIndex;
        if (index < 2)
            SceneManager.LoadScene(index + 1);
    }
}