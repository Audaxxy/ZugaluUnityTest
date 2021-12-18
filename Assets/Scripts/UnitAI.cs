using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitAI : MonoBehaviour//Basic pathfinding AI script
{
	private Vector3 agentTarget;//Agent's target location
	private NavMeshAgent unitAgent;//The agent
	

	private void Awake()
	{
		agentTarget = transform.position;
		unitAgent = GetComponent<NavMeshAgent>();
	}
	public void updateTarget(Vector3 targetPosition)//Sets the Agent's target destination. Called by the UnitController script
	{
		agentTarget = targetPosition;
		unitAgent.destination = agentTarget;
	}
}
