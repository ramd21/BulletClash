using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	static public class RectTransformExtension
	{
		static public Vector2 GetSize(this RectTransform aThis)
		{
			Vector3[] wArr = new Vector3[4];
			aThis.GetWorldCorners(wArr);

			float w = wArr[2].x - wArr[0].x;
			float h = wArr[2].y - wArr[0].y;

			return new Vector2(w, h);
		}

		static public Vector2 ToAnchor(this RectTransform aThis, Vector3 aWPos)
		{
			RectTransform pRTra = aThis.parent.GetComponent<RectTransform>();

			Vector3[] wArr = new Vector3[4];
			pRTra.GetWorldCorners(wArr);

			float l, r, t, b;

			l = wArr[0].x;
			r = wArr[2].x;
			t = wArr[2].y;
			b = wArr[0].y;


			float x = (aWPos.x - l) / (r - l);
			float y = (aWPos.y - b) / (t - b);


			return new Vector2(x, y);
		}
	}
}


