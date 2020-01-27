using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathSystem
{
	public class Path
	{
		public Node StartNode;
		public Node EndNode;
		public float Distance
		{
			get { return Vector2.Distance(StartNode.Position, EndNode.Position); }
		}
		public Grid Grid;
		public Node[] NodePathArr;

		public Path(Node start, Node end, Grid grid)
		{
			StartNode = start;
			EndNode = end;
			Grid = grid;
		}

		public void GeneratePathNodeArrCoroutine()
		{
			List<Node> openNodes = new List<Node>();
			List<Node> closedNodes = new List<Node>();
			Node curNode = StartNode;
			int steps = 0;
			bool pathFound = false;
			
			Node[] nodeNeighbors = Grid.GetNodeNeighbors(StartNode);
			Debug.Log(StartNode.Position);
			foreach(Node n in nodeNeighbors)
			{
				Debug.Log(n.Position);
				Debug.DrawLine(curNode.Position, n.Position, Color.yellow, 5f);
			}
		}
	}
}