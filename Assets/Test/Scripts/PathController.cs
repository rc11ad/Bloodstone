using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PathSystem
{
	public class PathController : MonoBehaviour
	{
		public GridManager GridManager;
		public PathController Target;
		public Path Path = null;

		//DEBUG
		public Node[] NodesBeingChecked = new Node[0];
		public Node LowestFScorNode = null;
		public Node CurrentNodeInPath = null;
		public List<Node> Walls = new List<Node>();
		List<Node> closedList = new List<Node>();
		List<Node> openList = new List<Node>();

		private void Start()
		{
			if (Target)
				GetPathToTarget();
		}

		private void GetPathToTarget()
		{
			Path = new Path(GridManager.Grid.GetClosestNode(transform.position), GridManager.Grid.GetClosestNode(Target.transform.position), GridManager.Grid);
		}

		private void Update()
		{
			if (Path == null)
				return;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Path.StartNode = GridManager.Grid.GetClosestNode(transform.position);
				StartCoroutine(GeneratePath());
			}
		}

		private IEnumerator GeneratePath()
		{
			float spd = 0.05f;
			NodesBeingChecked = new Node[4];

			Path.StartNode = GridManager.Grid.GetClosestNode(transform.position);
			CurrentNodeInPath = Path.StartNode;
			closedList.Add(CurrentNodeInPath);
			int g = 0; //Steps in path

			while(true)
			{
				g++;
				Walls.Clear();
				NodesBeingChecked = GridManager.Grid.GetNodeNeighbors(CurrentNodeInPath);
				yield return new WaitForSeconds(spd);

				foreach (Node n in NodesBeingChecked)
				{
					//Wall
					if (Physics2D.OverlapCircle(n.Position, 0.1f))
					{
						closedList.Add(n);
						Walls.Add(n);
					}
					else if(closedList.Contains(n))
					{

					}
					else
					{
						openList.Add(n);
						n.G = g;
					}
				}

				int low = int.MaxValue;
				foreach (Node n in openList)
				{
					n.GetHScore(Path.EndNode);
					if (n.H <= low)
					{
						if (LowestFScorNode != null && n.G < LowestFScorNode.G)
						{
							continue;
						}
						low = n.H;
						LowestFScorNode = n;
					}
				}
				yield return new WaitForSeconds(spd);

				CurrentNodeInPath = LowestFScorNode;
				closedList.Add(CurrentNodeInPath);
				openList.Remove(CurrentNodeInPath);

				yield return new WaitForSeconds(spd);
				LowestFScorNode = null;
			}
		}

		GUIStyle style = new GUIStyle();

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(GridManager.Grid.GetClosestNode(transform.position).Position, Vector3.one * GridManager.Grid.Spacing);

			//DEBUG
			foreach(Node n in openList)
			{
				Gizmos.color = new Color(0f, 0f, 1f, 0.2f);
				Gizmos.DrawCube(n.Position, GridManager.Grid.Spacing * Vector2.one);
				style.fontSize = 8;
				Handles.Label(n.Position + new Vector2(-GridManager.Spacing*0.5f, 0f), n.G.ToString() + " / " + n.H.ToString(), style);
			}

			foreach(Node n in Walls)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawCube(n.Position, GridManager.Grid.Spacing * Vector2.one);
			}

			if(LowestFScorNode != null)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawCube(LowestFScorNode.Position, GridManager.Grid.Spacing * Vector2.one);
			}

			if(CurrentNodeInPath != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawCube(CurrentNodeInPath.Position, GridManager.Grid.Spacing * Vector2.one * 0.5f);
			}
		}
	}
}