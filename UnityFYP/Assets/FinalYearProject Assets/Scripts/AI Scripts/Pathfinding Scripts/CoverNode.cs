using UnityEngine;
using System.Collections;

public class CoverNode : MonoBehaviour 
{
	public bool isUsed = false;
	
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
