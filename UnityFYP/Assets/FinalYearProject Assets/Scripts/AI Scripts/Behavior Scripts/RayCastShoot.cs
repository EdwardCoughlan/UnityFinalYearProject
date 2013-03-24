using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayCastShoot : MonoBehaviour
{
	public string fireButton = "Fire1";
	
	public GameObject region;
	public GameObject points;
	
	
	public GameObject primaryChar;
	public GameObject secondaryChar;
	
	GameObject Objective;

	void Start ()
	{
	
	}
	

	void Update ()
	{
		if(Input.GetButtonDown(fireButton))
		{
			
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.0f));
			
			RaycastHit info;
			GameObject nearest = null;
			GameObject secondNearest = null;
			float dis = Mathf.Infinity;
			float tempDis = 0;
			
			if(Physics.Raycast(ray, out info, 50f))
			{
				if(info.transform  != null)
				{
					Vector3 target = info.transform.position;
					Collider[] cols = Physics.OverlapSphere(target, 5f);
					foreach(Collider col in cols)
					{
						if(col.gameObject.CompareTag("CoverNode"))
						{
							tempDis = (col.transform.position - target).magnitude;
							if(tempDis < dis)
							{
								dis = tempDis;
								nearest = col.gameObject;
							}
								
						}
					}
					primaryChar.GetComponent<NavAgent>().objectiveNode = nearest;
					primaryChar.GetComponent<NavAgent>().moveTo = true;
					dis = Mathf.Infinity;
					foreach(Collider col in cols)
					{
						if(col.gameObject.CompareTag("CoverNode"))
						{
							if(col.gameObject != nearest)
							{
								tempDis = (col.transform.position - target).magnitude;
							if(tempDis < dis)
							{
								dis = tempDis;
								secondNearest = col.gameObject;
							}
								
							}
							
						}
					}
					secondaryChar.GetComponent<NavAgent>().objectiveNode = secondNearest;
					secondaryChar.GetComponent<NavAgent>().moveTo = true;
				}
			}
		}
	}
}
/*
 * GameObject item = (GameObject)Instantiate(NodeObj, location, Quaternion.identity);
			bool create  = testDistance(item);
			if(create)
			{
				OpenList.Add(item);
			}
			else
			{
				DestroyImmediate(item);
			}
 */