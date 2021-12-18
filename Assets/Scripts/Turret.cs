using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour//Handles turret configuarables, target detection, rotation, and firing
{
 
    [SerializeField] private Transform laserSpawn;//Point lasers are fired from

    [SerializeField] private float firerate; //Turret fire rate (time between shots)
    [SerializeField] private float rotationSpeed; //Rate of which turret will rotate towards it's nearest target
   

    private Turret turretScript; //Reference to this script to enable/disable when no targets are present to save on performance
    private bool canFire = true; //Determine whether turret is able to fire, fire rate dependant

    private Transform turretTransform; //Turret's transform, used for rotation and distance detection
    private Transform nearestTarget; //Turret's current nearest target
    
    private List<Transform> turretTargets; //List that contains all targets wihtin the turret's range

    private void Awake()
	{
        turretScript = GetComponent<Turret>();
        turretTransform = GetComponent<Transform>();

        turretTargets = new List<Transform>();

        turretScript.enabled = false;
	}
	
	void Update()
    {
        FindNearestTarget();//Calculate nearest target
        RaycastHit hit;
        if (nearestTarget)
        {
            if (Physics.Linecast(turretTransform.position, nearestTarget.position, out hit))//Cast a line between the turret and the nearest target to check for line of sight
            {
                if (hit.transform.tag == "Unit")//If line of sight = true
                {
                    RotateTurret();//Rotate turret towards ttarget
                     
                    RaycastHit targetAligned;

                    if (Physics.Raycast(laserSpawn.position, laserSpawn.TransformDirection(Vector3.forward), out targetAligned))//Cast a ray forward from the turret barrel
                    {
                        if (targetAligned.transform.tag == "Unit")//If barrel has line of sight with target
                        {
                            if (canFire)//Fire if possible
                            {
                                 StartCoroutine(FireLaser());//Fire laser
                            }
                        }
                    } 
                }
            }
        }
    }
    
    IEnumerator FireLaser() //Handles firerate, pulls laser from the pool and sets position and rotation to be aligned with the barrel
    {
        canFire = false; //Prevents firing another shot until reset

        GameObject laser = ProjectilePool.laserPool.GetPooledLaser(); //Gets the first available laser fro mthe pool

        if (laser)
        {
            laser.transform.position = laserSpawn.position;
            laser.transform.rotation = laserSpawn.rotation;
            laser.SetActive(true);
        }

        yield return new WaitForSeconds(firerate); //Waits until firerate elapses

        canFire = true;//Another shot can now be fired
    }

    private void FindNearestTarget()//Calculates the nearest target to the turret
    {
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < turretTargets.Count; i++) //For every target in range
        {
            float distance = (turretTargets[i].transform.position - turretTransform.transform.position).sqrMagnitude; //distance from target to turret
            if (distance < nearestDistance) //If closer than last
            {
                nearestTarget = turretTargets[i]; //Set new nearest target
                nearestDistance = distance; //Set new shortest distance
            }
        }
    }

    private void RotateTurret()//Rotates the turret toward nearest target
    {
        Vector3 dir = nearestTarget.position - turretTransform.position; //direction from turret to nearest target
        Quaternion lookRotation = Quaternion.LookRotation(dir); 
        Vector3 rotation = Quaternion.Lerp(turretTransform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles; //Lerps turret rotation towards lookRotation based on the rotationspeed
        turretTransform.rotation = Quaternion.Euler(0, rotation.y, 0); //Sets turret y axis rotation
    }
	private void OnTriggerEnter(Collider other)//When a unit enters range
	{
        if (other.transform.tag == "Unit")
        {
            turretTargets.Add(other.transform);//Add unit to target list
            turretScript.enabled = true;//Enable script so Update() runs
        }
	}
	private void OnTriggerExit(Collider other)//When a unit exits range
	{
        if (other.transform.tag == "Unit")
        {
            turretTargets.Remove(other.transform);//Remove unit from target list
            if (turretTargets.Count == 0)//If no targets
            {
                turretScript.enabled = false;//Disable script so Update() doesn't run
            }
        }
       
	}
}
