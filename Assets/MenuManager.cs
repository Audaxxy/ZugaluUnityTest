using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    
    public static string playerName;

    [SerializeField]  private TMP_Text playerNameField;
    
    public void loadScene(string sceneName)
    {
        playerName = playerNameField.text;
        SceneManager.LoadScene(sceneName);
    }
    public void quitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
