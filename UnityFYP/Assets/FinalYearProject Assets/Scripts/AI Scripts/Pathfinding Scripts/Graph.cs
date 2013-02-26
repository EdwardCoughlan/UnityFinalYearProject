using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph : MonoBehaviour
{
	public List<GameObject> nodes;
	public GameObject start;
	public GameObject goal;
	public float size = 2.0f;

	
	void OnDrawGizmos()
	{
		if(nodes.Count == 0)// if connections is empty
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, size);
		}
		else 
		{
			Gizmos.color= Color.green;
			Gizmos.DrawWireSphere(transform.position, size);
		}
		
		if(goal && start)
		{
			GameObject current = start;
			GameObject previous = start;
			
			//Graph g = new Graph(connections);
			//Debug.Log(g);
			/*List<GameObject> path = pathfinder.Dijkstra(start,goal);
			foreach(GameObject n in path)
			{
				current = n;
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(n.transform.position ,0.5f);
				if(!(previous == start))
				{
					Gizmos.DrawLine(previous.transform.position, n.transform.position);
				}
				if(n == goal)
				{
					break;
				}
				previous =n;
			}*/
		}
	}
}
