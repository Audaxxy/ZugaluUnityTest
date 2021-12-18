using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool laserPool;
    public List<GameObject> pooledLasers;
    public GameObject laser;
    public int poolSize;

	private void Awake()
	{
		laserPool = this;
	}
	private void Start()
	{
		pooledLasers = new List<GameObject> ();
		GameObject newLaser;
		for (int i = 0; i < poolSize; i++)
		{
			newLaser = Instantiate(laser,gameObject.transform);
			newLaser.SetActive(false);
			pooledLasers.Add(newLaser);
		}
	}

	public GameObject GetPooledLaser()
	{
		for (int i = 0; i < poolSize; i++)
		{
			if (!pooledLasers[i].activeInHierarchy)
			{
				return pooledLasers[i];
			}
		}
		return null;
	}
}
