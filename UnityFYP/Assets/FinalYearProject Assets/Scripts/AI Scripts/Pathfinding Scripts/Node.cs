using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	public float nodeRadius = 1f;
	public LayerMask nodeLayerMask;
	public LayerMask collisionLayerMask;
	public List<GameObject> neighbors;
	public List<GameObject> connections  = new List<GameObject>();
	public bool renderNodes = true;
	public bool canGetNeighbours = true;
	public float cost = 1000f;
	public GameObject ConnnectionObj;
	
	void OnDrawGizmos()
	{
		if(renderNodes)
		{
			if(cost == 150f)
			{
				Gizmos.color = Color.red;
			}
			else if(cost == 60f)
			{
				Gizmos.color = Color.yellow;
			}
			else if(cost == 10f)
			{
				Gizmos.color = Color.green;
			}
			else if(cost == 700f)
			{
				Gizmos.color = Color.blue;
			}
			else
			{
				Gizmos.color = Color.black;
			}
			Gizmos.DrawWireSphere(transform.position, 0.1f);
			foreach(GameObject n in neighbors)
			{
				//Gizmos.DrawWireSphere(gameObject.transform.position, 0.001f);
				Gizmos.DrawLine(gameObject.transform.position, n.transform.position);
			}
		}
	}
	
	public void generateConnections()
	{
		connections.Clear();
		if( connections.Count > neighbors.Count)
		{
			connections = new List<GameObject>();
		}
		else
		{
			foreach(GameObject n in neighbors)
			{
				GameObject temp = (GameObject)Instantiate(ConnnectionObj,transform.position, Quaternion.identity);
				
				temp.GetComponent<Connection>().setConnection(gameObject, n);
				connections.Add(temp);
			}
		}
		GetComponent<SphereCollider>().radius = 0.1f;
	}
	
	public List<GameObject> getConnections()
	{
		return this.connections;
	}
	
	public void getNeighbouringNodes()
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
		}
	}
	
}
