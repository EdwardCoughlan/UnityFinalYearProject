using UnityEngine;
using System.Collections;

public class CoverNode : MonoBehaviour 
{
	public bool isUsed = false;
	
	
	
	[ContextMenu ("Create Nodes")]
	public void getCurrentNode()
	{
		GetComponent<CurrentNode>().SetCurrentNode();
	}
	
	 void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Team1" || col.tag == "Tean2")
		{
			isUsed = true;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Team1" || col.tag == "Tean2")
		{
			isUsed = false;
		}
	}
}
