using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
	public class GridManager : MonoBehaviour
	{
		public bool ShowGrid;
		public Grid Grid;

		public float Spacing;
		public int Size;

		private void Start()
		{
			Grid = new Grid().SetGrid(Spacing, Size, transform.position.x, transform.position.y);
		}

		private void OnValidate()
		{
			if (!ShowGrid)
				return;

			Grid = new Grid().SetGrid(Spacing, Size, transform.position.x, transform.position.y);
		}

		private void OnDrawGizmos()
		{
			if (!ShowGrid || Grid == null)
				return;

			Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
			foreach (Node n in Grid.NodeArr)
			{
				Gizmos.DrawWireCube(n.Position, Vector2.one * Grid.Spacing);
			}
		}
	}
}