using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Return to Main menu");
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }
}
