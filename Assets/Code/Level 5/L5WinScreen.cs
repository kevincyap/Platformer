using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L5WinScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void handleQuit() {
        Application.Quit();
    }
}
