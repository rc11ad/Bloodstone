using UnityEngine;
using System.Collections.Generic;

namespace PathSystem
{
	public class Path
	{
		public Node Start;
		public Node End;
		public float Distance
		{
			get { return Vector2.Distance(Start, End); }
		}
		public Grid Grid;
		public Node[] NodePathArr;

		public Path(Node start, Node end, Grid grid)
		{
			Start = start;
			End = end;
			Grid = grid;
			GeneratePathNodeArr();
		}

		public void GeneratePathNodeArr()
		{
			List<Node> openNodes = new List<Node>();
			List<Node> closedNodes = new List<Node>();
			int steps = 0;
			
		}
	}
}