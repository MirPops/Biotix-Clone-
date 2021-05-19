using UnityEngine;
using UnityEngine.SceneManagement;

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

    // ��� ��� ������ 3 ������ �� ����� ���������� ������ �� ����������
    public static void LoadNextLVL()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            print("You win)))");
    }
}