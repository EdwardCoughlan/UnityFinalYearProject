using UnityEngine;
using System.Collections;

public class CurrentNode : MonoBehaviour
{
	public GameObject currentNode;
	public bool  IsAi =true;
	
	// Use this for initialization
	void Start ()
	{
		SetCurrentNode();
	}
	
	void SetCurrentNode()
	{
		float shortestDistance = Mathf.Infinity;
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Node"))
		{
			float dist = (obj.transform.position - transform.position).magnitude;
			if(dist < shortestDistance)
			{
				shortestDistance = dist;
				currentNode = obj;
			}
		}
	}
	
	// Update is called once per frame
	/*void Update ()
	{
		if(IsAi == false)
		{
			SetCurrentNode();
		}
	}*/
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Node")
		{
			currentNode = col.gameObject;
		}
	}
}
