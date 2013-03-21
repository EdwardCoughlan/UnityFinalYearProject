using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathPlanner : MonoBehaviour {
	
	
	public GameObject[] Characters;
	GameObject[] Objectives;
	GameObject Objective;
	public GameObject Influencer;
	
	Stack<GameObject> Path;
	
	public bool combine = true;
	public bool split = false;
	public bool splitCombine = false;
	public bool CombineSplit = false;
	

	
	
	public Stack<GameObject> requestPath(GameObject pointOfInterest)
	{
		//Select Cover Node From Region
		Objectives = pointOfInterest.GetComponent<Region>().coverNodesOpen.ToArray();
		int  temp  = (int)Random.Range(0f, (((float)Objectives.Length)-1f));
		Objective = Objectives[temp];
		Path = pathfinder.AStar(Character[0].GetComponent<CurrentNode>().currentNode, Objective.GetComponent<CurrentNode>().currentNode, new Heuristic(Objective.GetComponent<CurrentNode>().currentNode));
		InfluencePath(Path);
		return Path;
	}
	
	
	public void ResetWorldInfluence()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Node"))
		{
			obj.GetComponent<Node>().cost = 80;
		}
	}
	
	/*
	 * *
	 * 
	 * *
	 */
	public void InfluencePath(Stack<GameObject> pathTemp)
	{	
		GameObject nodeTemp = null;
		GameObject influencerTemp = null;
		int pathCount = pathTemp.Count;
		int counter = 0;
		while(pathTemp.Count != 0)
		{
			checkMultiChoice(counter, pathCount);
			nodeTemp = pathTemp.Pop();
			//If Split is true then it will be cheaper to find a path that is further away from the Main character
			influencerTemp = (GameObject)Instantiate(Influencer, nodeTemp.transform.position, Quaternion.identity);
			if(split == true)
			{
				influencerTemp.GetComponent<Influencer>().Size = 2f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(10f,0);
				influencerTemp.GetComponent<Influencer>().Size = 1f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(20f,0);
				influencerTemp.GetComponent<Influencer>().Size = .5f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(40f,0);	
			}
			
			//If Combine is true then it will be cheaper to find a cover node near to the main characters
			if(combine == true)
			{
				influencerTemp.GetComponent<Influencer>().Size = 2f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(40f,1);
				influencerTemp.GetComponent<Influencer>().Size = 1f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(20f,1);
				influencerTemp.GetComponent<Influencer>().Size = .5f;
				influencerTemp.GetComponent<Influencer>().CreateInfluenceSphereAndModifyInfluence(10f,1);	
			}
			nodeTemp.GetComponent<Node>().cost = 80;
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
}