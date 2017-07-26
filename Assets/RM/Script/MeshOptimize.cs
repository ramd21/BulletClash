using System.Collections;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEditor;


namespace RM
{
	public class MeshOptimize : RMBehaviour
	{
		public Mesh _Mesh;

		[Button("Optimize")]
		public int _Optimize;

		void Optimize()
		{
			MeshUtility.Optimize(_Mesh);
		}
	}
}


#endif 