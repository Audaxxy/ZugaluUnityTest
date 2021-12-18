using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Camera unitCamera;
    [SerializeField] private Transform currentUnitTransform;
    [SerializeField] private float cameraHeight;
    [SerializeField] private Transform unitTarget;
    [SerializeField] private Material selected,unselected;

    // Update is called once per frame
    void Update()
    {
        unitCamera.transform.position = new Vector3(currentUnitTransform.position.x, cameraHeight, currentUnitTransform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }
    public void Click()
    {
        Ray ray = unitCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Unit")
            {
                if (currentUnitTransform.GetComponent<MeshRenderer>() != null)
                {
                    currentUnitTransform.GetComponent<MeshRenderer>().material = unselected;
                }

                    currentUnitTransform = hit.transform;
                    currentUnitTransform.GetComponent<MeshRenderer>().material = selected;
                
            }
            else if (hit.transform.tag == "Ground") 
            {
                currentUnitTransform.GetComponentInParent<UnitAI>().updateTarget(hit.point);
            }
        }

    }
}
