using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
	public float smooth = 3f;
	public GameObject navAgent;
	Transform standardPos;
	//Transform lookAtPos;

	// Use this for initialization
	void Start () 
	{
		// initialising references
		standardPos =  navAgent.transform;
	}
	
	//Can be called multiple times per frame if allowed
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
		transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
	}
}