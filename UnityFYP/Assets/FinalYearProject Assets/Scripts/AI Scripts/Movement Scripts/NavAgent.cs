using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavAgent : MonoBehaviour
{
	public float speed = 1.0f;
	public float turnSpeed = 60.0f;
	[HideInInspector]
	public GameObject currentNode;
	[HideInInspector]
	public GameObject previousNode;
	public bool goalreached = false;
	
	public GameObject Influencer;
	
	public bool isPrimary = false;
	
	public GameObject primary;
	public GameObject secondary;
	
	
	public bool combine = false;
	public bool split = true;
	public bool splitCombine = false;
	public bool CombineSplit = false;
	
	
	public Animator animator;
	
	
	[HideInInspector]
	public bool moveTo = false;
	
	Stack<GameObject> path = null;
//	int count = 0;
	
	GameObject goal;
	
	Heuristic hr;
	
	public GameObject objectiveNode;
	// Use this for initialization
	void Start ()
	{
		animator = gameObject.GetComponent<Animator>();
		currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
		previousNode = gameObject.GetComponent<CurrentNode>().currentNode;
		hr = new Heuristic(objectiveNode.GetComponent<CurrentNode>().currentNode);
		path = new Stack<GameObject>();
		animator.SetBool("Moving", true);
	}
	
	// Update is called once per frame
	void Update()
	{ 
		if(moveTo == true)
		{
			if(primary.GetComponent<NavAgent>().path.Count != 0 || isPrimary == true)
			{
				if (goalreached)
				{
					//goalreached = true;
					//Debug.Log("goal reached");
				}
				else
				{
					if(path.Count == 0)
					{
						path = new Stack<GameObject>();
						hr = new Heuristic(objectiveNode.GetComponent<CurrentNode>().currentNode);
						path = pathfinder.AStar(gameObject.GetComponent<CurrentNode>().currentNode,objectiveNode.GetComponent<CurrentNode>().currentNode, hr);
						if(isPrimary == true)
						{
							//ResetWorldInfluence();
							Stack<GameObject> pathTemp = pathfinder.AStar(gameObject.GetComponent<CurrentNode>().currentNode,objectiveNode.GetComponent<CurrentNode>().currentNode, hr);
							InfluencePath(pathTemp);
						}
						try{
							goal = path.Pop();
						}
						finally
						{
							movement();
						}
						
					}
					else if(currentNode != previousNode)
					{
						try{	
						goal = path.Pop();
						}
						finally
						{
							currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
							previousNode = gameObject.GetComponent<CurrentNode>().currentNode;
							movement();
						}
					}
					else
					{
						movement();
						currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
					}
				}
			}
		}
		
		else 
		{
			if (goalreached)
		{
			//goalreached = true;
			//Debug.Log("goal reached");
		}
		else
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			
			if(gameObject.GetComponent<CurrentNode>().currentNode.Equals(player.GetComponent<CurrentNode>().currentNode))
			{
				goalreached = false;
			}
			if(path.Count == 0)
			{
				
				path = new Stack<GameObject>();
				hr = new Heuristic(player.GetComponent<CurrentNode>().currentNode);
				path = pathfinder.AStar(gameObject.GetComponent<CurrentNode>().currentNode,player.GetComponent<CurrentNode>().currentNode, hr);
				
				if(isPrimary == true)
				{
					
					secondary.GetComponent<NavAgent>().path.Clear();
					Stack<GameObject> pathTemp = pathfinder.AStar(gameObject.GetComponent<CurrentNode>().currentNode,player.GetComponent<CurrentNode>().currentNode, hr);
					InfluencePath(pathTemp);
				}	
				try{
					goal = path.Pop();
				}
				finally
				{
					movement();
				}
				
			}
			else if(currentNode != previousNode)
			{
				try{	
				goal = path.Pop();
				}
				finally
				{
					currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
					previousNode = gameObject.GetComponent<CurrentNode>().currentNode;
					movement();
				}
			}
			else
			{
				movement();
				currentNode = gameObject.GetComponent<CurrentNode>().currentNode;
			}
		}
		}
	}
	
	
	
	void movement()
	{
		Vector3 goalPosition = goal.transform.position;
		Vector3 goalDirection = goalPosition - transform.position;
		goalDirection.y = 0.0f;
		Vector3 normalizedGoalDirection = goalDirection.normalized;
		transform.position += transform.forward * speed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(normalizedGoalDirection), turnSpeed*Time.deltaTime);
	}
	
	
	public void InfluencePath(Stack<GameObject> pathTemp)
	{	
		GameObject nodeTemp = null;
		GameObject influencerTemp = null;
		int pathCount = pathTemp.Count;
		int counter = 0;
		ResetWorldInfluence();
		while(pathTemp.Count != 0)
		{
			
			checkMultiChoice(counter, pathCount);
			nodeTemp = pathTemp.Pop();
			//If Split is true then it will be cheaper to find a path that is further away from the Main character
			influencerTemp = (GameObject)Instantiate(Influencer, nodeTemp.transform.position, Quaternion.identity);
			if(split == true)
			{
				influencerTemp.GetComponent<Influencer>().Size = 4f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(10f,0);
				influencerTemp.GetComponent<Influencer>().Size = 2f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(60f,0);
				influencerTemp.GetComponent<Influencer>().Size = 1f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(150f,0);	
			}
			
			//If Combine is true then it will be cheaper to find a cover node near to the main characters
			if(combine == true)
			{
				influencerTemp.GetComponent<Influencer>().Size = 4f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(150f,1);
				influencerTemp.GetComponent<Influencer>().Size = 2f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(60f,1);
				influencerTemp.GetComponent<Influencer>().Size = 1f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(10f,1);	
			}
			nodeTemp.GetComponent<Node>().cost = 700f;
			counter= counter + 1;
			DestroyImmediate(influencerTemp);
		}
		
	}
	
	void checkMultiChoice(int counter, int pathCount)
	{
		if(splitCombine == true)
		{
			split = true;
			combine = false;
			if(counter > (pathCount/2))
			{
				split = false;
				combine = true;
			}
		}
		if(CombineSplit == true)
		{
			split = false;
			combine = true;
			if(counter > (pathCount/2))
			{
				split = true;
				combine = false;
			}
		}
	}
	
	public void ResetWorldInfluence()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Node"))
		{
			obj.GetComponent<Node>().cost = 1000f;
		}
	}
}
