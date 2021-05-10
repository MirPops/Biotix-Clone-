using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static bool isPaused;

    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
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
}