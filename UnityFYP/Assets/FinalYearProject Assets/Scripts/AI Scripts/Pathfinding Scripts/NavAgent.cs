using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavAgent : MonoBehaviour
{
	public float speed = 1.0f;
	public float turnSpeed = 30.0f;
	[HideInInspector]
	public GameObject currentNode;
	[HideInInspector]
	public GameObject previousNode;
	[HideInInspector]
	public GameObject ObjectiveCurrentNode;
	[HideInInspector]
	public GameObject ObjectivePreviousNode;
	
	Stack<GameObject> path = null;
//	int count = 0;
	
	GameObject goal;
	
	
	GameObject objectiveNode;
	// Use this for initialization
	void Start ()
	{
		currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
		previousNode = gameObject.GetComponent<CurrentNode>().currentNode;
	}
	
	
	void FixedUpdate()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(path.Pop() == null)
		{
			path = pathfinder.Dijkstra(gameObject.GetComponent<CurrentNode>().currentNode,objectiveNode.GetComponent<CurrentNode>().currentNode);
		}
		if(currentNode == previousNode && path != null)
		{
			goal  = path.Pop();
		}
		
		if(ObjectiveCurrentNode != ObjectivePreviousNode)
		{
			path = pathfinder.Dijkstra(gameObject.GetComponent<CurrentNode>().currentNode,objectiveNode.GetComponent<CurrentNode>().currentNode);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		
		
		//GameObject.FindGameObjectsWithTag("Node"),
		//Debug.Log (player);
			if(path != null)
			{
				//goal = path.Pop();
				Vector3 goalPosition = goal.transform.position;
				Vector3 goalDirection = goalPosition - transform.position;
				goalDirection.y = 0.0f;
				Vector3 normalizedGoalDirection = goalDirection.normalized;
				transform.position += transform.forward * speed * Time.deltaTime;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(normalizedGoalDirection), turnSpeed*Time.deltaTime);
			}
	}
}
