using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshGenerator : MonoBehaviour 
{
	//public float MaxHeigth;
	//public float MinHeight;
	
	public GameObject NodeObj;
	
	//public float WallDistance;	
	public float NodeToNodeDistance;
	
	private List<GameObject> OpenList = new List<GameObject>();
	//You can't modify a List in a foreach loop
	private List<Vector3> NodesToBeCreated = new List<Vector3>();
	
	
	public LayerMask nodeLayerMask;
	public LayerMask collisionLayerMask;
	
	
	[ContextMenu ("Create Nodes")]
	void generateNodes()
	{
		OpenList = new List<GameObject>();
		NodesToBeCreated = new List<Vector3>();
		//OpenList.Add(transform.position);	
		createNode(transform.position);
		
		while(OpenList.Count > 0)
		{
			foreach(GameObject node in OpenList)
			{
				//createPossibleNodes(node);
				getLocationsForNodes(node);
			}
			OpenList.Clear();
			foreach(Vector3 location in NodesToBeCreated)
			{
				createNode(location);
			}
			NodesToBeCreated.Clear();
		}
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
		//node.GetComponent<Node>().setProcessed(true);
	}
	
	
	public void createNode(Vector3 location)
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		if(!(locationIsInArray(nodes, location)))
		{
			GameObject item = (GameObject)Instantiate(NodeObj, location, Quaternion.identity);
			OpenList.Add(item);
		}
		//NodesToBeCreated.Remove(location);
	}
	
	/*public void addToList(GameObject item)
	{
		OpenList.Add(item);
		
	}*/
	
	
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
}
