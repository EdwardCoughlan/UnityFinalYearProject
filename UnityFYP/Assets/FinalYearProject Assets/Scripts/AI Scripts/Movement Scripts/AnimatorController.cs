using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour
{
	[HideInInspector]
	public GameObject agent;
	[HideInInspector]
	public Animator animator;

	public GameObject dummy;

	public float tightness = 2.0f;

	void Start()
	{
		agent = dummy;
		animator = GetComponent<Animator>();
	}
	
	void update()
	{
		transform.LookAt(agent.transform);
		float dist = Vector3.Distance(transform.position, agent.transform.position);
		animator.speed = dist * tightness;
	}

	void LateUpdate()
	{
		Vector3 rot = transform.rotation.eulerAngles;
		rot.x = 0;
		transform.rotation = Quaternion.Euler(rot);
	}
}
