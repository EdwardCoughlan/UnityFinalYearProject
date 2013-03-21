using UnityEngine;
using System.Collections;

public class Connection: MonoBehaviour
{
	
	public float cost;
	public GameObject fromNode;
	public GameObject toNode;
	// Use this for initialization
	
	public void setConnection(GameObject fromNode, GameObject toNode)
	{
		this.fromNode = fromNode;
		this.toNode = toNode;
		setCost();
	}
	
	public void setCost()
	{
		this.cost = (toNode.transform.position - fromNode.transform.position).magnitude   +  toNode.GetComponent<Node>().cost;
	}
	
}
