using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;
using UnityEditor;

namespace BC
{
	public class CollMan : Singleton<CollMan>
	{
		public int _DivDist;
		public int _XCnt;
		public int _YCnt;
		public Vector2Int _Offset;
		public List<Coll>[,,] _BlockCollList;
		public int _BlockCnt;


		public void Init()
		{
			_BlockCollList = new List<Coll>[2, (int)CharaType.max, _XCnt * _YCnt];
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < (int)CharaType.max; j++)
				{
					for (int k = 0; k < _BlockCollList.GetLength(2); k++)
						_BlockCollList[i, j, k] = new List<Coll>(100);
				}
			}

			
		}

		public void Clear()
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < (int)CharaType.max; j++)
				{
					for (int k = 0; k < _BlockCollList.GetLength(2); k++)
						_BlockCollList[i, j, k].Clear();
				}
			}
		}



#if UNITY_EDITOR
		void OnDrawGizmos()
		{
			_BlockCnt = _XCnt * _YCnt;

			Vector3 posA, posB;

			//横線
			for (int i = 0; i < _YCnt + 1; i++)
			{
				posA = transform.position + Vector3.forward * _DivDist * i;
				posB = posA + Vector3.right * _DivDist * _XCnt;
				posA += _Offset.ToVector3XZ();
				posB += _Offset.ToVector3XZ();

				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}

			//縦線
			for (int i = 0; i < _XCnt + 1; i++)
			{
				posA = transform.position + Vector3.right * _DivDist * i;
				posB = posA + Vector3.forward * _DivDist * _YCnt;

				posA += _Offset.ToVector3XZ();
				posB += _Offset.ToVector3XZ();
				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}

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
				//int x, y;
				//int morton;
				//x = (int)(labelPos.x / _DivDist);
				//y = (int)(labelPos.z / _DivDist);

				//morton = Get2DMortonNumber((ushort)x, (ushort)y);

				Handles.Label((labelPos + _Offset.ToVector3XZ()) / GameMan.cDistDiv, i.ToString(), gs);
			}
		}
#endif

	}
}


