using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitAI : MonoBehaviour
{
	[SerializeField] private Camera unitCamera;
	[SerializeField] private Vector3 agentTarget;
	private NavMeshAgent unitAgent;
	

	private void Awake()
	{
		agentTarget = this.transform.position;
		unitAgent = GetComponent<NavMeshAgent>();
	}
	private void Update()
	{
		unitAgent.destination = agentTarget;
	}
	public void updateTarget(Vector3 targetPosition)
	{
		agentTarget = targetPosition;
	}
}