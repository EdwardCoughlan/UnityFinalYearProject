using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Region : MonoBehaviour 
{
	public List<GameObject> coverNodesOpen = new List<GameObject>();
	
	public List<GameObject> coverNodesClosed = new List<GameObject>();
	
	public LayerMask CoverNodeLayerMask;
	
	public float size = 10.079f;
		
	
	
	void putInClosedList(GameObject coverNode)
	{
		coverNodesClosed.Add(coverNode);
		coverNodesOpen.Remove(coverNode);
	}
	
	void putInOpenList(GameObject coverNode)
	{
		coverNodesOpen.Add(coverNode);
		coverNodesClosed.Remove(coverNode);
	}
	
	
	[ContextMenu ("getCoverNodes")]
	void getCoverNodes()
	{
		coverNodesOpen.Clear ();
		coverNodesClosed.Clear();
		Collider[] cols = 	Physics.OverlapSphere(transform.position, size);
		foreach(Collider col in cols)
		{
			if(col.gameObject != gameObject)
			{
				if(col.gameObject.CompareTag("CoverNode"))
				{
					putInOpenList(col.gameObject); 	
				}
			}
		}
		
	}
}
