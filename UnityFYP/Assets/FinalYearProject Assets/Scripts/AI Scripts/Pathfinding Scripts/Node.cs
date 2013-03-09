	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Node : MonoBehaviour 
{
	public float nodeRadius = 1f;
	public LayerMask nodeLayerMask;
	public LayerMask collisionLayerMask;
	public List<GameObject> neighbors;
	
	public List<Connection> connections = new List<Connection>();
	public bool renderNodes = true;
	public bool canGetNeighbours = true;
	public float cost = 2.0f;
	
	
	void OnDrawGizmos()
	{
		if(renderNodes)
		{
			Gizmos.DrawWireSphere(transform.position, 0.1f);
			foreach(GameObject n in neighbors)
			{
				//Gizmos.DrawWireSphere(gameObject.transform.position, 0.001f);
				Gizmos.DrawLine(gameObject.transform.position, n.transform.position);
			}
		}
	}
	
	void getNeighbouringNodes()
	{
		if(canGetNeighbours)
		{
			neighbors.Clear();
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
							neighbors.Add(col.gameObject);
						}
					}
				}
			}
			generateConnections();
			
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
		if( connections.Count > neighbors.Count)
		{
			connections = new List<Connection>();
		}
		else
		{
			Connection c;
			foreach(GameObject n in neighbors)
			{
				c= new Connection(gameObject, n);
				connections.Add(c);
			}
		}
	}
	
	
	
	[ContextMenu ("Connect Node to Neighbors")]
	void FindNeighbors()
	{
		neighbors.Clear();
		
		Collider [] cols = Physics.OverlapSphere (transform.position, nodeRadius, nodeLayerMask);
		foreach(Collider node in cols)
		{
			if (node.gameObject != gameObject)
			{
				RaycastHit hit;
				Physics.Raycast(transform.position, (node.transform.position - transform.position),out hit, nodeRadius, collisionLayerMask);
				
				if (hit.transform != null)
				{
					if (hit.transform.gameObject == node.gameObject)
					{
						neighbors.Add (node.gameObject);
					}
				}
				
			}
		}
		
	}
}
