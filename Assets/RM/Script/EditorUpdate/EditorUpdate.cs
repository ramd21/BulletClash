using UnityEngine;
using System.Collections;

namespace RM
{
	interface IEditorUpdate
	{
#if UNITY_EDITOR
		void EditorUpdate();
		bool enabled { get; }
#endif
	}

	[ExecuteInEditMode]
	public class EditorUpdate : RMBehaviour
	{
#if UNITY_EDITOR
		void Update()
		{
			if (Application.isPlaying)
				return;

			IEditorUpdate[] w = GetComponents<IEditorUpdate>();

			if (w.Length == 0)
				DestroyImmediate(this);

			for (int i = 0; i < w.Length; i++)
			{
				w[i].EditorUpdate();
			}

			Component[] comArr = GetComponents<Component>();

			if (comArr[comArr.Length - 1] != this)
				UnityEditorInternal.ComponentUtility.MoveComponentDown(this);
		}
#endif

	}
}


