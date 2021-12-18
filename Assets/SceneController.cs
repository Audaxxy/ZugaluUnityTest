using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
  
    public TMP_Text nameField;
    void Start()
    {
        nameField.text = MenuManager.playerName;
    }

}
