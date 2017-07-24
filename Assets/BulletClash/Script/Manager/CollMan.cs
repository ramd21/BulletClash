using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BC
{
	public class CollMan : Singleton<CollMan>
	{
		public int _DivDist;
		public int _XCnt;
		public int _YCnt;
		public Vector2Int _ZeroPosOffset;

		//public List<Coll>[,,] _BlockCollList;

		public FastList<Coll>[,,] _BlockCollList;
		public int _BlockCnt;

		public void Init()
		{
			_BlockCollList = new FastList<Coll>[2, (int)CharaType.max, _XCnt * _YCnt];
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < (int)CharaType.max; j++)
				{
					for (int k = 0; k < _BlockCollList.GetLength(2); k++)
						_BlockCollList[i, j, k] = new FastList<Coll>(50, 10);
				}
			}
		}

		public void Clear()
		{
			int len, len2;
			len = (int)CharaType.max;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < len; j++)
				{
					len2 = _BlockCollList.GetLength(2);
					for (int k = 0; k < len2; k++)
					{
						_BlockCollList[i, j, k].Clear();
						_BlockCollList[i, j, k].Clear();
					}
				}
			}
		}


#if UNITY_EDITOR
		public Color _Color;
		public bool _ShowNum;
		public Vector2Int _GizmoOffset;
		void OnDrawGizmos()
		{
			_BlockCnt = _XCnt * _YCnt;

			Gizmos.color = _Color;

			Vector3 posA, posB;

			//横線
			for (int i = 0; i < _YCnt + 1; i++)
			{
				posA = transform.position + Vector3.forward * _DivDist * i;
				posB = posA + Vector3.right * _DivDist * _XCnt;
				posA += _GizmoOffset.ToVector3XZ();
				posB += _GizmoOffset.ToVector3XZ();

				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}

			//縦線
			for (int i = 0; i < _XCnt + 1; i++)
			{
				posA = transform.position + Vector3.right * _DivDist * i;
				posB = posA + Vector3.forward * _DivDist * _YCnt;

				posA += _GizmoOffset.ToVector3XZ();
				posB += _GizmoOffset.ToVector3XZ();
				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}


			if (!_ShowNum)
				return;
			//ラベル
			Vector3 labelPos;
			GUIStyle gs = new GUIStyle();
			gs.fontStyle = FontStyle.Bold;
			gs.alignment = TextAnchor.MiddleCenter;
			gs.normal.textColor = Color.red;

			for (int i = 0; i < _BlockCnt; i++)
			{
				labelPos = transform.position + Vector3.right * _DivDist * (i % _XCnt) + Vector3.forward * _DivDist * (i / _XCnt);
				labelPos += Vector3.forward * _DivDist / 2;
				labelPos += Vector3.right * _DivDist / 2;
				Handles.Label((labelPos + _GizmoOffset.ToVector3XZ()) / GameMan.cDistDiv, i.ToString(), gs);
			}
		}
#endif

	}
}


