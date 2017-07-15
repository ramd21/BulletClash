using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	[RequireComponent(typeof(EditorUpdate))]
	public class EditorUpdateBehaviour : RMBehaviour, IEditorUpdate
	{
		public virtual void EditorUpdate()
		{
		}
	}
}


