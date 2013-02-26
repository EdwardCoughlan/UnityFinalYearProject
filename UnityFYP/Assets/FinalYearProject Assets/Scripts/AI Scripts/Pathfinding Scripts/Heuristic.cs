using UnityEngine;
using System.Collections;

public class Heuristic {
	
	private Node goalNode = new Node();
	
	public Heuristic(Node n)	
	{
		this.goalNode = goalNode;
	}
	
	public Node getGoal()
	{
		return this.goalNode;
	}
	
	public void setGoal(Node n)
	{
		this.goalNode = goalNode;
	}
	
	public float euclideanDistanceEstimate(GameObject n)
	{
		return (goalNode.transform.position - n.transform.position).magnitude;
	}
	
}
