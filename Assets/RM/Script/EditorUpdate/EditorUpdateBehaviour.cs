using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	[RequireComponent(typeof(EditorUpdate))]
	public abstract class EditorUpdateBehaviour : RMBehaviour, IEditorUpdate
	{
#if UNITY_EDITOR
		public abstract void EditorUpdate();
#endif
	}
}


