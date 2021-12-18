using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera unitCamera;
    [SerializeField] private Transform unitTransform;
    [SerializeField] private float cameraHeight;
    [SerializeField] private Transform unitTarget;
    // Update is called once per frame
    void Update()
    {
        unitCamera.transform.position = new Vector3(unitTransform.position.x, cameraHeight, unitTransform.position.z);

        if (Input.GetMouseButton(0))
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
            unitTarget.transform.position = hit.point;
        }

    }
}
