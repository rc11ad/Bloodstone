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

		public void SetScore(int g, int h)
		{

		}
	}
}