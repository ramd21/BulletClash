using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RM
{
	public class SwitchRoot : EditorUpdateBehaviour
	{
#if UNITY_EDITOR
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
#endif
	}
}
