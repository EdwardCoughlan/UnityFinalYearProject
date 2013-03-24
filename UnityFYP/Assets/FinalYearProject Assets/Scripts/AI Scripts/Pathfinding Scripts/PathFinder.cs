using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pathfinder 
{	
	//Wiki Dijkstra similar to the one used by pushy pixels
	public static Stack<GameObject> wikiDijkstra(GameObject[] Graph, GameObject source, GameObject target)
	{
		Dictionary<GameObject,float> dist = new Dictionary<GameObject, float>();
		Dictionary<GameObject,GameObject> previous = new Dictionary<GameObject, GameObject>();
		List<GameObject> Q = new List<GameObject>();
		
		foreach(GameObject v in Graph)
		{
			dist[v] = Mathf.Infinity;
			previous[v] = null;
			Q.Add(v);
		}
		
		dist[source] = 0;
		
		while(Q.Count > 0)
		{
			float shortestDistance = Mathf.Infinity;
			GameObject shortestDistanceNode = null;
			foreach(GameObject obj in Q)
			{
				if(dist[obj] < shortestDistance)
				{
					shortestDistance = dist[obj];
					shortestDistanceNode = obj;
				}
			}
			
			GameObject u = shortestDistanceNode;
			
			Q.Remove(u);
			
			if(u == target)
			{
				Stack<GameObject> S = new Stack<GameObject>();
				while(previous[u] != null)
				{
					S.Push (u);
					u = previous[u];
				}
				return S;
			}
			
			if(dist[u] == Mathf.Infinity)
			{
				break;
			}
			
			foreach(GameObject v in u.GetComponent<Node>().neighbors)
			{
				float alt = dist[u] + (u.transform.position - v.transform.position).magnitude;
				
				if(alt < dist[v])
				{
					 dist[v] = alt;
					 previous[v] = u;
				}
			}
		}
		return null;
	}
	public static Stack<GameObject> Dijkstra(GameObject start, GameObject end)
	{
		//Debug.Log (start.GetComponent<Node>().getConnections());
		if(start.Equals(end))
		{
			Stack<GameObject> temp = new Stack<GameObject>();
			temp.Push(end);
			return temp;
		}
		List<GameObject> connections = new List<GameObject>();
		GameObject endNode = start;
		float endNodeCost = 0;
		NodeRecord endNodeRecord = new NodeRecord();
		
		NodeRecord startRecord = new NodeRecord(start, 0);
		
		List<NodeRecord> openList = new List<NodeRecord>();
		List<NodeRecord> closedList = new List<NodeRecord>();
		openList.Add(startRecord);
		
		NodeRecord current = startRecord;
		
		//Debug.Log (current);
		while(openList.Count > 0)
		{
			Debug.Log ("going through openlist");
			current = getSmallestElement(openList);
			//Debug.Log (current);
			if(current.node.Equals(end))
			{
				//Debug.Log ("Found goal");
				break;
			}
		//Debug.Log("End not found contining");
			connections = current.node.GetComponent<Node>().getConnections();
			//Debug.Log ("Connections" + connections);
			foreach(GameObject connection in connections)
			{
				//Debug.Log(connection);
				endNode = connection.GetComponent<Connection>().toNode;
				endNodeCost = current.CostSoFar + connection.GetComponent<Connection>().cost + endNode.GetComponent<Node>().cost;
				
				if(isInList(endNode ,closedList))  
				{
					continue;
				}
				else if(isInList(endNode, openList))
				{
					endNodeRecord = findNodeInList(endNode, openList);
					
					if(endNodeRecord.CostSoFar <= endNodeCost)
					{
						continue;
					}
				}
				else
				{
					endNodeRecord = new NodeRecord(endNode);
				}
				
				endNodeRecord.connection = connection;
				endNodeRecord.CostSoFar = endNodeCost;
				
				if(!(isInList (endNode, openList)))
				{
					openList.Add(endNodeRecord);
				}
			}
			openList.Remove(current);
			closedList.Add(current);
		}
		if(current.node != end)
		{
			return null;
		}
		else
		{
			Stack<GameObject> path = new Stack<GameObject>();
			while(current.node != start)
			{
				path.Push(current.node);
				current = findNodeInList(current.connection.GetComponent<Connection>().fromNode, closedList);
			}
			return path;
		}
	}
	
	public static Stack<GameObject> AStar(GameObject start, GameObject end, Heuristic heuristic)
	{
		List<GameObject> connections = new List<GameObject>();
		GameObject endNode = start;
		float endNodeCost  = 0f;
		float endNodeHeuristic = 0.0f;
		NodeRecord endNodeRecord = new NodeRecord();
		
		NodeRecord startRecord = new NodeRecord();
		startRecord.node = start;
		startRecord.CostSoFar = 0.0f;
		startRecord.estimatedTotalCost = heuristic.euclideanDistanceEstimate(start);
		
		List<NodeRecord> openList = new List<NodeRecord>();
		List<NodeRecord> closedList = new List<NodeRecord>();
		openList.Add(startRecord);
		
		NodeRecord current = startRecord;
		
		while(openList.Count > 0)
		{
			current = getSmallestElement(openList);
			
			if(current.node.Equals(end))
			{
				break;
			}
			connections = current.node.GetComponent<Node>().connections;
			foreach(GameObject connection in connections)
			{
				endNode = connection.GetComponent<Connection>().toNode;
				endNodeCost = current.CostSoFar+connection.GetComponent<Connection>().cost + endNode.GetComponent<Node>().cost;
				
				if(isInList(endNode, closedList))
				{
					endNodeRecord = findNodeInList(endNode, closedList);
					
					if(endNodeRecord.CostSoFar <= endNodeCost)
					{
						continue;
					}
					closedList.Remove(endNodeRecord);
					
					endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.CostSoFar;
				}
				else if(isInList(endNode, openList))
				{
					endNodeRecord = findNodeInList(endNode, openList);
					if(endNodeRecord.CostSoFar <= endNodeCost)
					{
						continue;
					}
					endNodeHeuristic = endNodeCost - endNodeRecord.CostSoFar;
				}
				else
				{
					endNodeRecord = new NodeRecord(endNode);
					endNodeHeuristic = heuristic.euclideanDistanceEstimate(endNode);
				}
				endNodeRecord.CostSoFar = endNodeCost;
				endNodeRecord.connection = connection;
				endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;
				
				if(!(isInList(endNode, openList)))
				{
					openList.Add(endNodeRecord);
				}
			}
			
			openList.Remove(current);
			closedList.Add(current);
		}
		if(current.node != end)
		{
			return null;
		}
		else
		{
			Stack<GameObject> path = new Stack<GameObject>();
			while(current.node != start)
			{
				path.Push(current.node);
				current = findNodeInList(current.connection.GetComponent<Connection>().fromNode, closedList);
			}
			path.Push(end);
			//path.Reverse();
			return path;
		}
	}
		
	public static NodeRecord getSmallestElement(List<NodeRecord> l)
	{
		if(l.Count == 1)
		{
			foreach(NodeRecord nr in l)
			{
				return nr;
			}
		}
		NodeRecord min = l[0];
		float minCost = min.CostSoFar;
		foreach(NodeRecord nr in l)
		{
			if(minCost > nr.CostSoFar)
			{
				minCost = nr.CostSoFar;
				min = nr;
			}
		}
		//Debug.Log("get Min Node");
		//Debug.Log(min.node);
		return min;
	}
	
	public static NodeRecord findNodeRecordUsingFromNode(GameObject n, List<NodeRecord> l)
	{
		GameObject temp = null;
		foreach(NodeRecord nr in l)
		{
			temp = nr.connection;
			if(temp.GetComponent<Connection>().fromNode.Equals(n))
			{
				return nr;
			}
		}
		return null;
	}
	
	
	public static bool isInList(GameObject n, List<NodeRecord> l)
	{
		foreach(NodeRecord nr in l)
		{
			if(nr.node.Equals(n))
			{
				return true;
			}
		}
		return false;
	}
	
	private static NodeRecord findNodeInList(GameObject n, List<NodeRecord> l)
	{
		foreach(NodeRecord nr in l)
		{
			if(nr.node.Equals(n))
			{
				return nr;
			}
		}
		return null;
	}
	
	private static bool checkIsNodesEqual(GameObject n1, GameObject n2)
	{
		if(n1 == n2)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
