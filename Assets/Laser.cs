using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float speed, lifetime;
    [SerializeField] private Rigidbody bullet;
	
	private void OnEnable()
	{
		StartCoroutine(DisableAfterSeconds(lifetime, gameObject));
		bullet.velocity = transform.forward * speed;
	}
	IEnumerator DisableAfterSeconds(float seconds, GameObject obj)
	{
		yield return new WaitForSeconds(seconds);
		obj.SetActive(false);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Unit")
		{
			gameObject.SetActive(false);
		}
	}
}
