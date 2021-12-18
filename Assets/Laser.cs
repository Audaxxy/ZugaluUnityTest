using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody bullet;
	private void Awake()
	{
		Destroy(gameObject, 1f);
	}
	void Update()
    {
        bullet.velocity = transform.forward * speed;
    }
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.transform.tag == "Unit")
        Destroy(gameObject);
	}
}
