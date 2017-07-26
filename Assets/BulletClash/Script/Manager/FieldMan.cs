using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class FieldMan : Singleton<FieldMan>, IEditorUpdate
	{
		public Vector2 _Size;
		public Vector2 _Offset;

		public Transform[] _TraBorder;

		public void EditorUpdate()
		{
			Vector3 pos;

			if (_Size.y / GameMan.cDistDiv == 0)
				_TraBorder[0].SetScale(1, 1, 1);
			else
				_TraBorder[0].SetScale(1, 1, _Size.y / GameMan.cDistDiv);

			pos = transform.position;
			pos += Vector3.forward * _Size.y / 2;
			pos += _Offset.ToVector3XZ();

			_TraBorder[0].SetPosition(pos / GameMan.cDistDiv);

			if (_Size.y / GameMan.cDistDiv == 0)
				_TraBorder[1].SetScale(1, 1, 1);
			else
				_TraBorder[1].SetScale(1, 1, _Size.y / GameMan.cDistDiv);

			pos = transform.position;
			pos += Vector3.forward * _Size.y / 2;
			pos += Vector3.right * _Size.x;
			pos += _Offset.ToVector3XZ();
			_TraBorder[1].SetPosition(pos / GameMan.cDistDiv);

			if (_Size.x / GameMan.cDistDiv == 0)
				_TraBorder[2].SetScale(1, 1, 1);
			else
				_TraBorder[2].SetScale(_Size.x / GameMan.cDistDiv, 1, 1);

			pos = transform.position;
			pos += Vector3.right * _Size.x / 2;
			pos += _Offset.ToVector3XZ();
			_TraBorder[2].SetPosition(pos / GameMan.cDistDiv);

			if (_Size.x / GameMan.cDistDiv == 0)
				_TraBorder[3].SetScale(1, 1, 1);
			else
				_TraBorder[3].SetScale(_Size.x / GameMan.cDistDiv, 1, 1);

			pos = transform.position;
			pos += Vector3.right * _Size.x / 2;
			pos += Vector3.forward * _Size.y;
			pos += _Offset.ToVector3XZ();
			_TraBorder[3].SetPosition(pos / GameMan.cDistDiv);
		}




#if UNITY_EDITOR
		void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, _Size.ToVector3XZ() / GameMan.cDistDiv);
		}
#endif
	}
}


