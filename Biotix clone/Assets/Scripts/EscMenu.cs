using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;

    public void OpenEscMenu()
    {
        escMenu.SetActive(true);
        GameManager.SetPause(true);
    }

    public void Countinue()
    { 
        escMenu.SetActive(false);
        GameManager.SetPause(false);
    }

    public void Replay()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void Exit()
        => Application.Quit();
}
