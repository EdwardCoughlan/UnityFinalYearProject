using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavAgent : MonoBehaviour
{
	public float speed = 1.0f;
	public float turnSpeed = 30.0f;
	
	Stack<GameObject> path = null;
	int count = 0;
	
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
			//Traverse to next node in path to player node
		if(path ==  null || count == 120)
		{
			Heuristic hr = new Heuristic(player.GetComponent<CurrentNode>().currentNode);
			path = pathfinder.AStar(
				gameObject.GetComponent<CurrentNode>().currentNode,
				player.GetComponent<CurrentNode>().currentNode, hr);
			count = 0;
			
		}
		GameObject goal = path.Pop();
		Vector3 goalPosition = goal.transform.position;
		Vector3 goalDirection = goalPosition - transform.position;
		goalDirection.y = 0.0f;
		Vector3 normalizedGoalDirection = goalDirection.normalized;
		transform.position += transform.forward * speed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(normalizedGoalDirection), turnSpeed*Time.deltaTime);
		count++;
	}
}
