using System.Collections;
using UnityEngine;


public class Laser : MonoBehaviour //Script that handles the laser's movement and return to pool.
{
	[SerializeField] private float speed;//Target velocity of laser
	[SerializeField] private float lifetime;//Time before laser returns to pool
	[SerializeField] private Rigidbody laserRB;//The laser's rigidbody
	
	private void OnEnable()//When activated from pool
	{
		StartCoroutine(DisableAfterSeconds(lifetime, gameObject));//returns laser to pool after x time
		laserRB.velocity = transform.forward * speed;//sets laser velocity
	}
	IEnumerator DisableAfterSeconds(float seconds, GameObject obj)//returns laser to pool after x time
	{
		yield return new WaitForSeconds(seconds);
		obj.SetActive(false);
	}
	private void OnCollisionEnter(Collision collision)//returns laser to pool on collision
	{
			gameObject.SetActive(false);
	}
}
