using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour//Handles user inputs and camera placement
{
   
    [SerializeField] private float cameraHeight;//Height of the camera above the unit
    [SerializeField] private Material selected;
    [SerializeField] private Material unselected;

    private Transform currentUnitTransform; //Currently selected unit's transform

    private Camera unitCamera; //Reference to the camera
    

	// Update is called once per frame
	private void Awake()
	{
        unitCamera = GetComponent<Camera>();
	}
	void Update()
    {
        if (currentUnitTransform)//If unit selected, have camera track it
        {
            unitCamera.transform.position = new Vector3(currentUnitTransform.position.x, cameraHeight, currentUnitTransform.position.z);
        }

        if (Input.GetMouseButtonDown(0))//On left click
        {
            Click(); //Select Unit or Set destination
        }
        if (Input.GetMouseButtonDown(1))//On right click
        {
            Deselect(); //Deselect Unit
        }
    }
    private void Click()
    {
        Ray ray = unitCamera.ScreenPointToRay(Input.mousePosition);//Cast ray from camera to clicked location
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Unit")//If clicked a unit
            {
                if (currentUnitTransform)//If a unit already selected
                {
                    currentUnitTransform.GetComponent<MeshRenderer>().material = unselected; //Set previous selection material to unselected
                }
                    currentUnitTransform = hit.transform; //Select new unit
                    currentUnitTransform.GetComponent<MeshRenderer>().material = selected; //Set selected unit material to selected
            }
            else if (hit.transform.tag == "Ground") //If clicked ground
            {
                if (currentUnitTransform)//If unit selected
                {
                    currentUnitTransform.GetComponentInParent<UnitAI>().updateTarget(hit.point); //Set Unit's target location to the location of the click
                }
            }
        }
    }
    private void Deselect()//Deselect units
    {
        if (currentUnitTransform)//Check if a unit is selected
        {
            currentUnitTransform.GetComponent<MeshRenderer>().material = unselected; //Set unit material to deselected
            currentUnitTransform = null; 
        }
    }
}
