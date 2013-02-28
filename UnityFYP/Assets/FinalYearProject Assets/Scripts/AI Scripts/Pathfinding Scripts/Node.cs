using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	public float nodeRadius = 1f;
	public LayerMask nodeLayerMask;
	public LayerMask collisionLayerMask;
	public List<GameObject> Neighbors;
	public List<Connection> connections = new List<Connection>();
	public bool renderNodes = true;
	public bool canGetNeighbours = true;
	public float cost = 2.0f;
	
	private bool processed = false;
	
	void OnDrawGizmos()
	{
		if(renderNodes)
		{
			Gizmos.DrawWireSphere(transform.position, 0.1f);
			foreach(GameObject n in Neighbors)
			{
				//Gizmos.DrawWireSphere(gameObject.transform.position, 0.001f);
				Gizmos.DrawLine(gameObject.transform.position, n.transform.position);
			}
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
	
	void generateConnections()
	{
		connections.Clear();
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
	
	[ContextMenu ("Get neighbouring nodes")]
	void getNeighbouringNodes()
	{
		if(canGetNeighbours)
		{
			Neighbors.Clear();
			Collider[] cols = Physics.OverlapSphere(transform.position, nodeRadius, nodeLayerMask);
			foreach(Collider col in cols)
			{
				if(col.gameObject != gameObject)
				{
					RaycastHit hit;
					Physics.Raycast(transform.position, (col.transform.position - transform.position), out hit, nodeRadius);
					if(hit.transform != null)
					{
						if(hit.transform.gameObject.GetComponent<Node>() == col.gameObject.GetComponent<Node>())
						{
							Neighbors.Add(col.gameObject);
						}
					}
				}
			}
			generateConnections();
			
		}
	}
	
	
	public void setProcessed(bool input)
	{
		this.processed = input;
	}
	
	public bool getProcessed()
	{
		return this.processed;
	}
}
