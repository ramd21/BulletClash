using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class SetAnchorToCur
	{
		[MenuItem("Tools/SetAnchorToCur")]
		static public void RunMenu()
		{
			for (int i = 0; i < Selection.gameObjects.Length; i++)
			{
				RectTransform rTra = Selection.gameObjects[i].GetComponent<RectTransform>();
				if (rTra)
					Sub(rTra);
			}
		}

		static void Sub(RectTransform aRTra)
		{
			Vector3[] wArr = new Vector3[4];
			Vector2 min;
			Vector2 max;

			aRTra.GetWorldCorners(wArr);
			min = aRTra.ToAnchor(wArr[0]);
			max = aRTra.ToAnchor(wArr[2]);

			aRTra.anchorMin = min;
			aRTra.anchorMax = max;
			aRTra.sizeDelta = Vector2.zero;
			aRTra.anchoredPosition = Vector2.zero;
		}
	}
}


