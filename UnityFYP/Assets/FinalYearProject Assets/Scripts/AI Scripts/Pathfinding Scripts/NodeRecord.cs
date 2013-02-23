using UnityEngine;
using System.Collections;

public class NodeRecord
{
	public GameObject node;
	public Connection connection = null;
	public float CostSoFar =0.0f;
	
	public NodeRecord(GameObject node, Connection connection,float costSoFar)
	{
		this.node = node;
		this.connection = connection;
		this.CostSoFar = costSoFar;
	}
	
	public NodeRecord(GameObject node, float costSoFar)
	{
		this.node = node;
		this.CostSoFar = costSoFar;
	}
	
	public NodeRecord(GameObject node)
	{
		this.node = node;
	}
	
	public NodeRecord()
	{
	}
}
