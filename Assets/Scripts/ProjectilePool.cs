using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that creates and handles a pool of projectile to prevent constant laser instantiation
public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool laserPool; //Static reference to the pool to be accessed from other scripts
    public List<GameObject> pooledLasers; //The pool
    public GameObject laser; //Gameobject the pool is populated with
    public int poolSize;//Number of lasers in the pool

	private void Awake()
	{
		laserPool = this;
	}
	
	private void Start()//Populates the pool with disabled lasers
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

	public GameObject GetPooledLaser()//Gets the first laser in the pool not currently active
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
