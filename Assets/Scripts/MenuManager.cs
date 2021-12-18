using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour //Basic UI controls
{
    
    public static string playerName; //Static playerName to carry through scene change

    [SerializeField]  private TMP_Text playerNameField;
	
	public void loadScene(string sceneName) //Loads scene by given name
    {
        playerName = playerNameField.text; //Takes user's input name
        SceneManager.LoadScene(sceneName); //Loads next scene
    }
    public void quitGame() //Closes application both in editor and build
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
