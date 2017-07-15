using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class ActivateOnlySelectedRoot : EditorUpdateBehaviour
	{
		public override void EditorUpdate()
		{
			if (Selection.activeTransform)
			{
				if (Selection.activeTransform.IsOffspringOf(transform))
				{
					Transform w = Selection.activeTransform;

					while (true)
					{
						if (!w)
							return;

						if (w.parent == transform)
							w.DeactivateSiblings();

						w = w.parent;
					}
				}
			}
		}
	}
}


