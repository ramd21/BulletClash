using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class DeactivateSiblings
	{
		[MenuItem("Tools/DeactivateSiblings")]
		static public void RunMenu()
		{
			if (Selection.activeTransform)
			{
				if (Selection.activeTransform.parent)
				{
					Selection.activeTransform.DeactivateSiblings();
				}
			}
		}
	}
}


