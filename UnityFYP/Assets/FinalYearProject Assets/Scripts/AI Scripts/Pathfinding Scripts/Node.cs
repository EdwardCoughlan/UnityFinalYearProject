using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	public List<GameObject> Neighbors;
	public List<Connection> connections = new List<Connection>();
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Vector3.one);
		gameObject.GetComponent<Node>().generateConnections();
		foreach(GameObject n in Neighbors)
		{
			Gizmos.DrawWireSphere(gameObject.transform.position, 0.25f);
			Gizmos.DrawLine(gameObject.transform.position, n.transform.position);
		}
	}
	

	public void addConnection(Connection c)
	{
		connections.Add(c);
	}
	
	public List<Connection> getConnections()
	{
		return connections;
	}
	
	public void generateConnections()
	{
		if(Neighbors.Count == connections.Count)
		{
			connections  = connections;
		}
		else if( connections.Count > Neighbors.Count)
		{
			connections = new List<Connection>();
		}
		else
		{
			Connection c;
			foreach(GameObject n in Neighbors)
			{
				c= new Connection(gameObject, n);
				connections.Add(c);
			}
		}
	}
}
