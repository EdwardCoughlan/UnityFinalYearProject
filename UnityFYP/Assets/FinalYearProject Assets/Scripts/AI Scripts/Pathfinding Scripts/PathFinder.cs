using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class pathfinder 
{	
	public static List<GameObject> Dijkstra(GameObject start, GameObject end)
	{
		List<Connection> connections = new List<Connection>();
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
			//Debug.Log ("going through openlist");
			current = getSmallestElement(openList);
			//Debug.Log (current);
			if(current.node.Equals(end))
			{
				//Debug.Log ("Found goal");
				break;
			}
		//Debug.Log("End not found contining");
			connections = current.node.GetComponent<Node>().connections;
			//Debug.Log ("Connections" + connections);
			foreach(Connection connection in connections)
			{
				//Debug.Log(connection);
				endNode = connection.toNode;
				endNodeCost = current.CostSoFar + connection.cost;
				
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
			List<GameObject> path = new List<GameObject>();
			while(current.node != start)
			{
				path.Add(current.node);
				current = findNodeInList(current.connection.fromNode, closedList);
			}
			path.Reverse();
			return path;
		}
	}
	
	
/*	public static void astar(List<Node> graph, Node start, Node end)
	{
		Heuristic heuristic = new Heuristic(end);
		NodeRecord startRecord = new NodeRecord();
		startRecord.setNode(start);
		startRecord.setCostSoFar(0);
		startRecord.estimateTotalCost(heuristic.euclideanDistanceEstimate(start));
		
		List<NodeRecord> openList = new List<NodeRecord>();
		List<NodeRecord> closedList = new List<NodeRecord>();
		openList.Add(startRecord);
		List<Connection> connections = new List<Connection>();
		
		
		NodeRecord current = startRecord;
		Node toNode;
		float toNodeCost = 0.0f;
		NodeRecord toNodeRecord = new NodeRecord();
		float toNodeHeuristic = 0;
		
		foreach(NodeRecord nr in openList)
		{
			current = getSmallestElement(openList);
			
			Debug.Log ("Checking if Current node is the goal");
			if(current.getNode() == end)
			{
				Debug.Log ("Goal found");
				break;
			}
			Debug.Log ("Goal not found");
			
			connections = current.getNode().getConnections();
			
			foreach(Connection c in connections)
			{
				toNode = c.getToNode();
				toNodeCost = current.getCostSoFar()  + c.getCost();
				
				if(isInList(endNode, closedList))
				{
					toNodeRecord = findNodeInList(toNode);
					
					if(toNodeRecord.getCostSoFar() <= toNodeCost)
					{
						continue;
					}
					closedList.Remove(toNodeRecord);
					
					//possibly wrong checck
					toNodeHeuristic = toNodeCost - toNodeRecord.getCostSoFar();
				}
				else if(isInList(toNode, openList))
				{
					toNodeRecord = findNodeInList(toNode, openList);
					
					if(toNodeRecord.getCostSoFar() <= toNodeCost)
					{
						continue;
					}
					toNodeHeuristic = toNodeCost - toNodeRecord.getCostSoFar();
				}
				else
				{
					toNodeRecord = new NodeRecord();
					toNodeRecord.setNode(toNode);
					
					
					toNodeRecord.estimateTotalCost(heuristic.euclideanDistanceEstimate(toNode));
				}
				toNodeRecord.setCostSoFar(toNodeCost);
				toNodeRecord.setConnection(c);
				toNodeRecord.estimateTotalCost(toNodeCost + toNodeHeuristic);
				openList.Add(toNodeRecord);
			}
			openList.Remove(current);
			closedList.Add(current);
			
		}
		if(current.getNode != end)
		{
			return null;
		}
		
		else
		{
			List<Node> path = new List<Node>();
			Node pathPiece = current.getConnection;
			while(pathPiece != start)
			{
				path.Add(pathPiece);
			}
		}
	}*/
		
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
		Connection temp;
		foreach(NodeRecord nr in l)
		{
			temp = new Connection(n, nr.node);
			temp = nr.connection;
			if(temp.fromNode.Equals(n))
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
	
	private static  NodeRecord findNodeInList(GameObject n, List<NodeRecord> l)
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
	
	private static  bool checkIsNodesEqual(GameObject n1, GameObject n2)
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
