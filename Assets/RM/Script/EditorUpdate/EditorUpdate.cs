using UnityEngine;
using System.Collections;

namespace RM
{
	interface IEditorUpdate
	{
		void EditorUpdate();
	}

	[ExecuteInEditMode]
	public class EditorUpdate : RMBehaviour
	{
		void Update()
		{
			IEditorUpdate[] w = GetComponents<IEditorUpdate>();

			if (w.Length == 0)
				DestroyImmediate(this);

			for (int i = 0; i < w.Length; i++)
			{
				w[i].EditorUpdate();
			}
		}
	}
}


