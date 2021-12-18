using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneController : MonoBehaviour
{
	[SerializeField] private TMP_Text nameField;

	void Start()
    {
        nameField.text = MenuManager.playerName;
    }
}
