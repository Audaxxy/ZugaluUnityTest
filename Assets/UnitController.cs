using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
	[SerializeField] private Transform agentTarget;
	private NavMeshAgent unitAgent;

	private void Awake()
	{
		unitAgent = GetComponent<NavMeshAgent>();
	}
	private void Update()
	{
		unitAgent.destination = agentTarget.position;
	}
	
}
