using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static bool isPaused;
    public static System.Action<OwnerOfCell> OnEndLevel;

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

    private void LoadNextLevel(OwnerOfCell owner)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index < 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print($"Win - {owner}");
    }
}