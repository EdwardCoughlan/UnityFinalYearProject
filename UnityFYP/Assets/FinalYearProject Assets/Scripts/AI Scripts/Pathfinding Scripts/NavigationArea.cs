using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavigationArea : MonoBehaviour {
	
	public GameObject[] baseMesh;
	public GameObject[] navMeshs;
	public GameObject NodeObj;
	
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(transform.position, 2f);
	}
	
	
	[ContextMenu ("Get Nodes from Mesh")]
	void getNodes()
	{
		//gets the offset of the base mesh from world 0,0,0
		Vector3 offsetOfBaseMesh;
		
		for(int position  =0; position < baseMesh.Length; position++)
		{
				offsetOfBaseMesh = baseMesh[position].gameObject.transform.position;
			
			Mesh mesh = navMeshs[position].GetComponent<MeshFilter>().sharedMesh;
			Vector3[] vertices = mesh.vertices;
			foreach(Vector3 vert in vertices)
			{
				Vector3 vertice = new Vector3((vert.x+offsetOfBaseMesh.x),(vert.z + offsetOfBaseMesh.y), (-vert.y + offsetOfBaseMesh.z));
				Instantiate(NodeObj,vertice,  Quaternion.identity);
			}
		}
		
		/*Mesh mesh = navMesh.GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = mesh.vertices;
		foreach(Vector3 vert in vertices)
		{
			Vector3 vertice = new Vector3(vert.x,vert.z, -vert.y);
			Instantiate(NodeObj,vertice,Quaternion.identity);
		}*/
	}
}
