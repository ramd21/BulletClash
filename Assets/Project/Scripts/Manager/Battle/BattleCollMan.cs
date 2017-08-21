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
	public class BattleCollMan : Singleton<BattleCollMan>
	{
		public int _DivDist;
		public int _XCnt;
		public int _YCnt;
		[UnityEngine.Serialization.FormerlySerializedAs("_ZeroPosOffset")]
		public Vector2Int _Offset;

		FastList<Coll>[] _BlockCollList;
		public int _BlockCnt;

		int _Max;
		int _PlayerOffset;

		public void Init()
		{
			_BlockCnt = _XCnt * _YCnt;
			_Max = 2 * (int)CharaType.max * _BlockCnt;
			_BlockCollList = new FastList<Coll>[_Max];
			for (int i = 0; i < _Max; i++)
			{
				_BlockCollList[i] = new FastList<Coll>(50, 10);
			}

			_PlayerOffset = _BlockCnt * (int)CharaType.max;
		}

		public void Clear()
		{
			for (int i = 0; i < _Max; i++)
			{
				_BlockCollList[i].Clear();
			}
		}

		public void AddColl(Coll aAdd, int aPlayerId, CharaType aType, int aBlock)
		{
			int pos = (_PlayerOffset * aPlayerId) + (_BlockCnt * (int)aType) + aBlock;
			_BlockCollList[pos].Add(aAdd);
		}

		public FastList<Coll> GetCollList(int aPlayerId, CharaType aType, int aBlock)
		{
			int pos = (_PlayerOffset * aPlayerId) + (_BlockCnt * (int)aType) + aBlock;
			return _BlockCollList[pos];
		}

#if UNITY_EDITOR
		public Color _Color;
		public bool _ShowNum;
		public Vector2 _GizmoOffset;
		void OnDrawGizmos()
		{
			_BlockCnt = _XCnt * _YCnt;

			Gizmos.color = _Color;

			Vector3 posA, posB;

			//â°ê¸
			for (int i = 0; i < _YCnt + 1; i++)
			{
				posA = transform.position + Vector3.forward * _DivDist * i;
				posB = posA + Vector3.right * _DivDist * _XCnt;
				posA += _GizmoOffset.ToVector3XZ();
				posB += _GizmoOffset.ToVector3XZ();

				Gizmos.DrawLine(posA / BattleGameMan.cDistDiv, posB / BattleGameMan.cDistDiv);
			}

			//ècê¸
			for (int i = 0; i < _XCnt + 1; i++)
			{
				posA = transform.position + Vector3.right * _DivDist * i;
				posB = posA + Vector3.forward * _DivDist * _YCnt;

				posA += _GizmoOffset.ToVector3XZ();
				posB += _GizmoOffset.ToVector3XZ();
				Gizmos.DrawLine(posA / BattleGameMan.cDistDiv, posB / BattleGameMan.cDistDiv);
			}

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position + (_GizmoOffset.ToVector3XZ() + _Offset.ToVector3XZ()) / BattleGameMan.cDistDiv, 5f);


			if (!_ShowNum)
				return;
			//ÉâÉxÉã
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
				Handles.Label((labelPos + _GizmoOffset.ToVector3XZ()) / BattleGameMan.cDistDiv, i.ToString(), gs);
			}


			

		}
#endif

	}
}


