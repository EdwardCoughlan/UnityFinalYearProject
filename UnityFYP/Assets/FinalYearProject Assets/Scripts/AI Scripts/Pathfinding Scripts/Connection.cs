using UnityEngine;
using System.Collections;

public class Connection
{
	
	public float cost;
	public GameObject fromNode;
	public GameObject toNode;
	// Use this for initialization
	
	public Connection(GameObject fromNode, GameObject toNode)
	{
		this.fromNode = fromNode;
		this.toNode = toNode;
		setCost();
	}
	
	public Connection ()
	{
	}
	
	public void setCost()
	{
		this.cost = (toNode.transform.position - fromNode.transform.position).magnitude;
	}
	
}
