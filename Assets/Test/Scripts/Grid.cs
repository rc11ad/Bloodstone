using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
	public class Grid
	{
		public float[] XPositions;
		public float[] YPositions;
		public float Spacing;
		public int Size;
		public float StartX;
		public float StartY;
		public List<Node> Nodes;
		public Node[,] NodeArr;

		public Grid SetGrid(float spacing, int size, float startx = 0f, float starty = 0f)
		{
			Spacing = spacing;
			Size = size;
			StartX = startx;
			StartY = starty;
			
			NodeArr = new Node[size, size];

			float xpos = startx - (spacing * size * 0.5f);
			float ypos = starty + (spacing * size * 0.5f);

			//Build node list.
			int nodeIndex = 0;
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					NodeArr[x,y] = new Node(new Vector2(xpos, ypos), new Vector2Int(x, y));
					nodeIndex++;
					ypos -= spacing;
				}
				xpos += spacing;
				ypos = starty + (spacing * size * 0.5f);
			}

			return this;
		}
		
		public Node GetClosestNode(Vector2 position)
		{
			float nearDist = Mathf.Infinity;
			Node nearNode = null;
			float d;
			foreach (Node n in NodeArr)
			{
				d = (position - n.Position).sqrMagnitude;
				if (d < nearDist)
				{
					nearNode = n;
					nearDist = d;
				}
			}

			return nearNode;
		}

		public Node[] GetNodeNeighbors(Node node)
		{
			//up,left,down,right
			Node[] nodes = new Node[4];

			nodes[0] = NodeArr[node.IndexPosition.x, Mathf.Min(node.IndexPosition.y + 1, Size-1)]; //Limit to bounds of array.
			nodes[1] = NodeArr[Mathf.Max(node.IndexPosition.x - 1, 0), node.IndexPosition.y];
			nodes[2] = NodeArr[node.IndexPosition.x, Mathf.Max(node.IndexPosition.y - 1, 0)];
			nodes[3] = NodeArr[Mathf.Min(node.IndexPosition.x + 1, Size-1), node.IndexPosition.y];

			return nodes;
		}
	}
}