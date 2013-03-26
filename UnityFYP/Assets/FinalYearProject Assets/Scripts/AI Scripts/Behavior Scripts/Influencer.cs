using UnityEngine;
using System.Collections;

public class Influencer : MonoBehaviour {


	public float Size = 0f;
	
	public LayerMask nodeLayerMask;
	
	
	/*
	 **
	 *This method controls the influence of the nodes which occur in the overlapSphere or the area of affect of the character.
	 *If the influence type is split then nodes with cost below the influence are modified while if the type is combine the nodes greater
	 *than the influence are modified.
	 **
	 */
	public void CreateInfluenceSphereAndModifyInfluence(float influence, int type)
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, Size, nodeLayerMask);
		foreach(Collider col in cols)
		{
			if(col.gameObject != gameObject)
			{
				if(col.gameObject.CompareTag("Node"))
				{
					if(type == 0)
					{
						if(col.gameObject.GetComponent<Node>().cost < influence || col.gameObject.GetComponent<Node>().cost == 1000f &&  col.gameObject.GetComponent<Node>().cost != 700f)
						{
							col.gameObject.GetComponent<Node>().cost = influence;
						}
					}
					else if(type ==1)
					{
						if(col.gameObject.GetComponent<Node>().cost > influence || col.gameObject.GetComponent<Node>().cost == 1000f  &&  col.gameObject.GetComponent<Node>().cost != 700f)
						{
							col.gameObject.GetComponent<Node>().cost = influence;
						}
					}
				}
			}
		}
	}
}
