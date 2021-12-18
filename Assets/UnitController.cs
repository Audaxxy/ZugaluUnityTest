using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
   
    [SerializeField] private float cameraHeight;
    [SerializeField] private Material selected,unselected;

    private Camera unitCamera;
    private Transform currentUnitTransform;

	// Update is called once per frame
	private void Awake()
	{
        unitCamera = GetComponent<Camera>();
	}
	void Update()
    {
        if (currentUnitTransform != null)
        {
            unitCamera.transform.position = new Vector3(currentUnitTransform.position.x, cameraHeight, currentUnitTransform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
        }
    }
    private void Click()
    {
        Ray ray = unitCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Unit")
            {
                if (currentUnitTransform)
                {
                    currentUnitTransform.GetComponent<MeshRenderer>().material = unselected;
                }
                    currentUnitTransform = hit.transform;
                    currentUnitTransform.GetComponent<MeshRenderer>().material = selected;
            }
            else if (hit.transform.tag == "Ground") 
            {
                if (currentUnitTransform)
                {
                    currentUnitTransform.GetComponentInParent<UnitAI>().updateTarget(hit.point);
                }
            }
        }
    }
    private void Deselect()
    {
        if (currentUnitTransform)
        {
            currentUnitTransform.GetComponent<MeshRenderer>().material = unselected;
            currentUnitTransform = null;
        }
    }
}
