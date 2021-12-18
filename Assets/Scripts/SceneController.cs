using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneController : MonoBehaviour//Basic data handler
{
	[SerializeField] private TMP_Text nameField;

	void Start()
    {
        nameField.text = MenuManager.playerName; //Gets the static playerName from the previous scene and sets the UI text
    }
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))//Closes game on pressing escape
		{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
		}
	}
}
