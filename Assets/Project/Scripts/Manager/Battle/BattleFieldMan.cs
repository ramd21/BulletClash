using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class BattleFieldMan : Singleton<BattleFieldMan>, IEditorUpdate
	{
		public Vector2Int _Size;
		public Vector2Int _Offset;

		public LineRenderer _LineRend;

		void SetBorder()
		{
			Vector3[] posArr = new Vector3[5];

			posArr[0] = transform.position;
			posArr[0] += Vector3.forward * _Size.y / 2;
			posArr[0] += Vector3.left * _Size.x / 2;
			posArr[0] /= BattleGameMan.cDistDiv;


			posArr[1] = transform.position;
			posArr[1] += Vector3.forward * _Size.y / 2;
			posArr[1] += Vector3.right * _Size.x / 2;
			posArr[1] /= BattleGameMan.cDistDiv;

			posArr[2] = transform.position;
			posArr[2] += Vector3.back * _Size.y / 2;
			posArr[2] += Vector3.right * _Size.x / 2;
			posArr[2] /= BattleGameMan.cDistDiv;

			posArr[3] = transform.position;
			posArr[3] += Vector3.back * _Size.y / 2;
			posArr[3] += Vector3.left * _Size.x / 2;
			posArr[3] /= BattleGameMan.cDistDiv;

			posArr[4] = transform.position;
			posArr[4] += Vector3.forward * _Size.y / 2;
			posArr[4] += Vector3.left * _Size.x / 2;
			posArr[4] /= BattleGameMan.cDistDiv;


			_LineRend.positionCount = 5;
			for (int i = 0; i < 5; i++)
			{
				_LineRend.SetPosition(i, posArr[i]);
			}

		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			SetBorder();
		}

#endif
	}
}


