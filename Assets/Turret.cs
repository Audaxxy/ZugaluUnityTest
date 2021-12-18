using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private Transform turretTarget,turretTransform;
    [SerializeField] private GameObject bullet;
    public Transform laserSpawn;
    public float firerate = 60;
    // Update is called once per frame
    void Update()
    {
        if (turretTarget != null)
        {
            RaycastHit hit;
            if (Physics.Linecast(turretTransform.position, turretTarget.position,out hit))
            {
                if (hit.transform.tag == "Unit")
                {
                    Vector3 dir = turretTarget.position - turretTransform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(dir);
                    Vector3 rotation = lookRotation.eulerAngles;
                    turretTransform.rotation = Quaternion.Euler(0, rotation.y, 0);

                    if (firerate == 300)
                    {
                        Instantiate(bullet,laserSpawn.position,turretTransform.rotation);
                        firerate = 0;
                    }
                    firerate++;
                }
                else
                {
                    Debug.Log("blocked");
                }
            }
        }
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.tag == "Unit")
        {
            turretTarget = other.transform;
        }
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.transform.tag == "Unit")
        {
            turretTarget = null;
        }
       
	}
}
