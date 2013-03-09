using UnityEngine;
using System.Collections;

public class Heuristic {
	
	private GameObject goalNode;
	
	public Heuristic(GameObject n)	
	{
		this.goalNode = n;
	}
	
	public GameObject getGoal()
	{
		return this.goalNode;
	}
	
	public void setGoal(GameObject n)
	{
		this.goalNode = n;
	}
	
	public float euclideanDistanceEstimate(GameObject n)
	{
		return (goalNode.transform.position - n.transform.position).magnitude;
	}
	
}
