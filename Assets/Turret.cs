using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
 
    [SerializeField] private Transform laserSpawn;

    [SerializeField] private float firerate;
    [SerializeField] private float rotationSpeed;

    private Turret turretScript;
    private bool canFire = true;

    private Transform turretTransform,nearestTarget;

    private List<Transform> turretTargets;

    private void Awake()
	{
        turretScript = GetComponent<Turret>();
        turretTransform = GetComponent<Transform>();

        turretTargets = new List<Transform>();

        turretScript.enabled = false;
	}
	// Update is called once per frame
	void Update()
    {
        FindNearestTarget();
        RaycastHit hit;
        if (nearestTarget)
        {
            if (Physics.Linecast(turretTransform.position, nearestTarget.position, out hit))
            {
                if (hit.transform.tag == "Unit")
                {
                    RotateTurret();
                     
                    RaycastHit targetAligned;

                    if (Physics.Raycast(laserSpawn.position, laserSpawn.TransformDirection(Vector3.forward), out targetAligned))
                    {
                        if (targetAligned.transform.tag == "Unit")
                        {
                            if (canFire)
                            {
                                 StartCoroutine(FireLaser());
                            }
                        }
                    } 
                }
            }
        }
    }
    
    IEnumerator FireLaser()
    {
        canFire = false;

        GameObject laser = ProjectilePool.laserPool.GetPooledLaser();

        if (laser != null)
        {
            laser.transform.position = laserSpawn.position;
            laser.transform.rotation = laserSpawn.rotation;
            laser.SetActive(true);
        }

        yield return new WaitForSeconds(firerate);

        canFire = true;
    }
    private void FindNearestTarget()
    {
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < turretTargets.Count; i++)
        {
            float distance = (turretTargets[i].transform.position - turretTransform.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestTarget = turretTargets[i];
                nearestDistance = distance;
            }
        }
    }
    private void RotateTurret()
    {
        Vector3 dir = nearestTarget.position - turretTransform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretTransform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        turretTransform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.tag == "Unit")
        {
            turretTargets.Add(other.transform);
            turretScript.enabled = true;
        }
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.transform.tag == "Unit")
        {
            turretTargets.Remove(other.transform);
            if (turretTargets.Count == 0)
            {
                turretScript.enabled = false;
            }
        }
       
	}
}
