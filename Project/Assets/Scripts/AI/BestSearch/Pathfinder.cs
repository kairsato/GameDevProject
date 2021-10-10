using UnityEngine;
using System.Collections;

// THE FOLLOWING CODE IS TAKEN FROM THE WEEK 7 WORKSHOP SCRIPT FOR A* PATHFINDING - WHICH HAS BEEN MODIFIED TO USE BEST SEARCH INSTEAD

public class Pathfinder : MonoBehaviour {

	private Transform startPos, endPos;
	public Node startNode { get; set; }
	public Node goalNode { get; set; }
	
	public ArrayList pathArray;
	
	GameObject objStart, objEnd;
	private float elapsedTime = 5.0f;
	//Interval time between pathfinding
	
	public float intervalTime = 5.0f;

	Rigidbody objStart_rigidbody;
	
	void Start () {
		objStart = GameObject.Find("Boss");
		objEnd = GameObject.FindGameObjectWithTag("Player");


		objStart_rigidbody = objStart.GetComponent<Rigidbody>();
		
		pathArray = new ArrayList();
		FindPath();
	}
	
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= intervalTime) {
			elapsedTime = 0.0f;
			FindPath();

			// Once path has been found - go
			if (pathArray.Count > 0)
			{
				int i = 0;
				foreach (Node n in pathArray)
				{
					if (i < pathArray.Count)
					{
						Node cn = (Node)pathArray[i];
						//Turn towards node
						objStart.transform.LookAt(cn.position);

						// Move to node
						//if (Vector3.Distance(objStart.transform.position, objEnd.transform.position) > 1) // If distance to target is greater than 1 - move forward
						//{
							objStart_rigidbody.velocity = transform.forward * 2;
						//}
						if (Vector3.Distance(objStart.transform.position, cn.position) < 5) //If close the node - move to next node
						{
							i++;
						}


					}

				}
			}


			if ((pathArray.Count <= 2) || (Vector3.Distance(objStart.transform.position, objEnd.transform.position) < 1)) // If less than 2 nodes - stop moving

			{
				objStart_rigidbody.velocity = transform.forward * 0;
			}


		}

		if (Vector3.Distance(objStart.transform.position, objEnd.transform.position) < 1)
		{
			objStart_rigidbody.velocity = transform.forward * 0;
			elapsedTime = 0;
		}
		/*
		
		if (Input.GetButtonUp("Fire1")) {
			Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
			
			// Generate a ray from the cursor position
			Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			// Determine the point where the cursor ray intersects the plane.
			float HitDist = 0;
			
			// If the ray is parallel to the plane, Raycast will return false.
			if (groundPlane.Raycast(RayCast, out HitDist))
			{
				// Get the point along the ray that hits the calculated distance.
				Vector3 RayHitPoint = RayCast.GetPoint(HitDist);
				
				objStart.transform.position = RayHitPoint;
			}
		}
		else if (Input.GetButtonUp("Fire2")) {
			Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
			
			// Generate a ray from the cursor position
			Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			// Determine the point where the cursor ray intersects the plane.
			float HitDist = 0;
			
			// If the ray is parallel to the plane, Raycast will return false.
			if (groundPlane.Raycast(RayCast, out HitDist))
			{
				// Get the point along the ray that hits the calculated distance.
				Vector3 RayHitPoint = RayCast.GetPoint(HitDist);
				
				objEnd.transform.position = RayHitPoint;
			}
		}
		*/

	}

	void FindPath()
	{
		startPos = objStart.transform;
		endPos = objEnd.transform;

		startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
		goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

		pathArray = BSearch.FindPath(startNode, goalNode); // Use Best search to find best path

	}


	
	void OnDrawGizmos() {
		if (pathArray == null)
			return;
		
		if (pathArray.Count > 0) {
			int index = 1;
			foreach (Node node in pathArray) {
				if (index < pathArray.Count) {
					Node nextNode = (Node)pathArray[index];
					Debug.DrawLine(node.position + new Vector3(0.0f,0.05f,0.0f), nextNode.position + new Vector3(0.0f,0.05f,0.0f), Color.green);
					index++;
				}
			}
		}
	}
}
