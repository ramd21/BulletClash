#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM
{
	public class GizmoUtil
	{
		static GUIContent gCon;
		static GUIStyle gStyle;

		public static void DrawArc(Vector3 aPos, float aRadius, float aStartDeg, float aEndDeg, Color aColor, Vector3 aUp, Vector3 aForward)
		{
			Gizmos.color = aColor;
			float degDiff = aEndDeg - aStartDeg;
			float cnt = degDiff / 5;

			for (int i = 0; i < cnt; i++)
			{
				float deg0, deg1;
				float degInter = 5;

				deg0 = aStartDeg + degInter * i;
				deg1 = deg0 + degInter;
				if (deg1 > aEndDeg)
					deg1 = aEndDeg;

				Quaternion qt = Quaternion.identity;
				qt.SetLookRotation(aForward, aUp);
				Vector3 right = qt * Vector3.right;
				Vector3 forward = qt * Vector3.forward;

				Vector3 pos0, pos1;
				pos0 = aPos + right * Mathf.Sin(deg0 * Mathf.Deg2Rad) * aRadius + forward * Mathf.Cos(deg0 * Mathf.Deg2Rad) * aRadius;
				pos1 = aPos + right * Mathf.Sin(deg1 * Mathf.Deg2Rad) * aRadius + forward * Mathf.Cos(deg1 * Mathf.Deg2Rad) * aRadius;

				Gizmos.DrawWireCube(pos1, Vector3.one);
				Gizmos.DrawLine(pos0, pos1);
			}
		}

		public static void DrawCircle(Vector3 aPos, float aRadius, Color aColor, Vector3 aUp)
		{
			Gizmos.color = aColor;
			for (int i = 0; i < 36 * 2; i++)
			{
				Quaternion qt = Quaternion.identity;
				qt.SetLookRotation(aUp);

				Vector3 right = qt * Vector3.right;
				Vector3 up = qt * Vector3.up;

				float deg0, deg1;
				deg0 = i * 5;
				deg1 = (i + 1) * 5;
				Vector3 pos0, pos1;
				pos0 = aPos + right * Mathf.Sin(deg0 * Mathf.Deg2Rad) * aRadius + up * Mathf.Cos(deg0 * Mathf.Deg2Rad) * aRadius;
				pos1 = aPos + right * Mathf.Sin(deg1 * Mathf.Deg2Rad) * aRadius + up * Mathf.Cos(deg1 * Mathf.Deg2Rad) * aRadius;

				Gizmos.DrawLine(pos0, pos1);
			}
		}

		public static void DrawArrow(Vector3 aPosFrom, Vector3 aPosTo, Color aColor, Vector3 aUp, float aArrowCapLength)
		{
			Gizmos.color = aColor;
			Gizmos.DrawLine(aPosFrom, aPosTo);

			Vector3 dir = aPosTo - aPosFrom;
			Quaternion qt = Quaternion.identity;

			qt.SetLookRotation(dir, Vector3.up);

			Vector3 left = qt * new Vector3(-1, 0, -1).normalized;
			Vector3 right = qt * new Vector3(1, 0, -1).normalized;

			qt.SetLookRotation(dir, aUp);

			Gizmos.DrawLine(aPosTo, aPosTo + left * aArrowCapLength);
			Gizmos.DrawLine(aPosTo, aPosTo + right * aArrowCapLength);
		}

		public static void DrawLabel(Vector3 aPos, string aStr, Color aColor, FontStyle aFontStyle)
		{
			if (gCon == null)
				gCon = new GUIContent();

			if (gStyle == null)
				gStyle = new GUIStyle();


			gCon.text = aStr;
			gStyle.normal.textColor = aColor;
			gStyle.fontStyle = aFontStyle;
			Handles.Label(aPos, gCon, gStyle);
		}

		public static void DrawLabelWithOutLine(Vector3 aPos, string aStr, Color aColor, Color aOutLineColor, FontStyle aFontStyle)
		{
			if (gCon == null)
				gCon = new GUIContent();

			if (gStyle == null)
				gStyle = new GUIStyle();

			gCon.text = aStr;
			gStyle.normal.textColor = aColor;
			gStyle.fontStyle = aFontStyle;

			DrawLabelOutLine(aPos, gCon, gStyle, aOutLineColor);

			gStyle.normal.textColor = aColor;
			Handles.Label(aPos, gCon, gStyle);
		}

		public static void DrawLabelOutLine(Vector3 aPos, GUIContent aGCon, GUIStyle aGStyle, Color aColor)
		{
			aGStyle.normal.textColor = aColor;

			float offset = HandleUtility.GetHandleSize(aPos) / 80f;

			Vector3 v3Offset;
			v3Offset = Vector3.forward;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.left;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.right;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.back;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);


			v3Offset = Vector3.forward + Vector3.left;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.forward + Vector3.right;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.back + Vector3.left;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
			v3Offset = Vector3.back + Vector3.right;
			Handles.Label(aPos + v3Offset.normalized * offset, aGCon, aGStyle);
		}
	}
}




#endif
