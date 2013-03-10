using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshGenerator : MonoBehaviour 
{
	public float MaxHeigth;
	
	public GameObject NodeObj;
	
	public GameObject ConnectionObj;
	
	public LayerMask nodeLayerMask;
	
	public LayerMask collisionLayerMask;
	
	//public float NodeToNodeDistance;	
	public float NodeToNodeDistance;
	//Distance to which a Node is allowed to exist from an object
	private float NodeToObjectDistance;
	
	private List<GameObject> OpenList = new List<GameObject>();
	//You can't modify a List in a foreach loop
	private List<Vector3> NodesToBeCreated = new List<Vector3>();

	
	[ContextMenu ("Create Nodes")]
	void generateNodes()
	{
		NodeToObjectDistance = NodeToNodeDistance/2;
		OpenList = new List<GameObject>();
		NodesToBeCreated = new List<Vector3>();
		createNode(transform.position);
		
		while(OpenList.Count > 0)
		{
			foreach(GameObject node in OpenList)
			{
				getLocationsForNodes(node);
			}
			OpenList.Clear();
			foreach(Vector3 location in NodesToBeCreated)
			{
				createNode(location);
			}
			NodesToBeCreated.Clear();
		}
		connectNodes();
 	}
	
	
	public void getLocationsForNodes(GameObject node)
	{
		RaycastHit hit;
		Vector3 temp = new Vector3((node.transform.position.x + NodeToNodeDistance), node.transform.position.y, node.transform.position.z);
		Physics.Raycast(node.transform.position, (temp - node.transform.position), out hit, NodeToNodeDistance);
		raycastHitAction(hit, temp);
		temp = new Vector3((node.transform.position.x - NodeToNodeDistance), node.transform.position.y, node.transform.position.z);
		Physics.Raycast(node.transform.position, (temp - node.transform.position), out hit, NodeToNodeDistance);
		raycastHitAction(hit, temp);
		temp = new Vector3(node.transform.position.x , node.transform.position.y, (node.transform.position.z + NodeToNodeDistance));
		Physics.Raycast(node.transform.position, (temp - node.transform.position), out hit, NodeToNodeDistance);
		raycastHitAction(hit, temp);
		temp = new Vector3(node.transform.position.x, node.transform.position.y, (node.transform.position.z - NodeToNodeDistance));
		Physics.Raycast(node.transform.position, (temp - node.transform.position), out hit, NodeToNodeDistance);
		raycastHitAction(hit, temp);
	}
	
	
	public void createNode(Vector3 location)
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		if(!(locationIsInArray(nodes, location)))
		{
			GameObject item = (GameObject)Instantiate(NodeObj, location, Quaternion.identity);
			bool create  = testDistance(item);
			if(create)
			{
				OpenList.Add(item);
			}
			else
			{
				DestroyImmediate(item);
			}
		}
	}
	
	public bool testDistance(GameObject node)
	{
		Collider[] cols = Physics.OverlapSphere(node.transform.position, NodeToObjectDistance, collisionLayerMask);
		if(cols.Length > 0 )
		{
			
			return false;
		}
		return true;
	}
	
	void raycastHitAction(RaycastHit hit, Vector3 location)
	{
		if(hit.transform == null)
		{
			NodesToBeCreated.Add(location);
		}
	}
	
	bool locationIsInArray(GameObject[] nodes, Vector3 location)
	{
		foreach(GameObject node in nodes)
		{
			if(node.transform.position.Equals(location))
			{
				return true;
			}
		}
		return false;
	}
	
	
	void connectNodes()
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		foreach(GameObject node in nodes)
		{
			node.GetComponent<Node>().nodeRadius = 1.1f* NodeToNodeDistance;
			node.GetComponent<Node>().getNeighbouringNodes();
			node.GetComponent<Node>().generateConnections();
		}
	}
}
