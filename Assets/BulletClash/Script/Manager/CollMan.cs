using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class CollMan : Singleton<CollMan>
	{
		public int _BlockBase;
		public int _XCnt;
		public int _YCnt;
		public Vector2Int _Offset;
		public List<Coll>[,,] _BlockCollList;
		public int _CollBlockCnt;


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

			_CollBlockCnt = _XCnt * _YCnt;
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
			Vector3 posA, posB;

			//横線
			for (int i = 0; i < _YCnt + 1; i++)
			{
				posA = transform.position + Vector3.forward * _BlockBase * i;
				posB = posA + Vector3.right * _BlockBase * _XCnt;
				posA += _Offset.ToVector3XZ();
				posB += _Offset.ToVector3XZ();

				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}

			//縦線
			for (int i = 0; i < _XCnt + 1; i++)
			{
				posA = transform.position + Vector3.right * _BlockBase * i;
				posB = posA + Vector3.forward * _BlockBase * _YCnt;

				posA += _Offset.ToVector3XZ();
				posB += _Offset.ToVector3XZ();
				Gizmos.DrawLine(posA / GameMan.cDistDiv, posB / GameMan.cDistDiv);
			}
		}
#endif

	}
}


