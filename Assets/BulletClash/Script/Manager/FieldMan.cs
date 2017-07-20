using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class FieldMan : Singleton<FieldMan>, IEditorUpdate
	{
		public Vector2Int _FieldSize;

		public Transform[] _TraBorder;

		public void EditorUpdate()
		{
			_TraBorder[0].SetScale(1, 1, _FieldSize.y / GameMan.cDistDiv);
			_TraBorder[0].SetPosition(transform.position + (_FieldSize.x / 2) / GameMan.cDistDiv * Vector3.right);

			_TraBorder[1].SetScale(_FieldSize.x / GameMan.cDistDiv, 1, 1);
			_TraBorder[1].SetPosition(transform.position + (_FieldSize.y / 2) / GameMan.cDistDiv * Vector3.forward);

			_TraBorder[2].SetScale(1, 1, _FieldSize.y / GameMan.cDistDiv);
			_TraBorder[2].SetPosition(transform.position - (_FieldSize.x / 2) / GameMan.cDistDiv * Vector3.right);

			_TraBorder[3].SetScale(_FieldSize.x / GameMan.cDistDiv, 1, 1);
			_TraBorder[3].SetPosition(transform.position - (_FieldSize.y / 2) / GameMan.cDistDiv * Vector3.forward);
		}
#if UNITY_EDITOR
		void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, _FieldSize.ToVector3XZ() / 10);
		}
#endif
	}
}


