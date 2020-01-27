using UnityEngine;

namespace PathSystem
{
	public class Node
	{
		public Vector2 Position;
		public Vector2Int IndexPosition;
		public Node[] Neighbors;

		public float F { get { return G + H; } }
		public int G;
		public int H;

		public Node(Vector2 position, Vector2Int indexPos)
		{
			Position = position;
			IndexPosition = indexPos;
		}

		public void UpdateHScore(Node target)
		{
			H = (int)(Mathf.Abs(target.Position.x - Position.x) + Mathf.Abs(target.Position.y - Position.y));
		}

		public int GetHScore(Node target)
		{
			UpdateHScore(target);
			return H;
		}
	}
}