using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
	public class PathController : MonoBehaviour
	{
		public GridManager GridManager;
		public PathController Target;
		public Path Path;

		private void Start()
		{
			if (Target)
				GetPathToTarget();
		}

		private void GetPathToTarget()
		{
			Path = new Path(GridManager.Grid.GetClosestNode(transform.position), GridManager.Grid.GetClosestNode(Target.transform.position), GridManager.Grid);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(GridManager.Grid.GetClosestNode(transform.position).Position, Vector3.one * GridManager.Grid.Spacing);
		}
	}
}