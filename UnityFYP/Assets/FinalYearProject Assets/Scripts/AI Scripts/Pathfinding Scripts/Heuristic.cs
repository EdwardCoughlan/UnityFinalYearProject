using UnityEngine;
using System.Collections;

public class Heuristic {
	
	GameObject goalNode;
	
	public Heuristic(GameObject n)	
	{
		goalNode = n;
	}
	
	public GameObject getGoal()
	{
		return goalNode;
	}
	
	public void setGoal(GameObject n)
	{
		goalNode = n;
	}
	
	public float euclideanDistanceEstimate(GameObject n)
	{
		return (goalNode.transform.position - n.transform.position).magnitude;
	}
	
}
