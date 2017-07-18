using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public abstract class Chara : EditorUpdateBehaviour
	{
		public int _PlayerId;
		public ActiveState _State;
		public Vector3 _Pos;

		[SerializeField]
		Coll[] _CollArr;

		public abstract void UpdateView();

		public override void EditorUpdate()
		{
			_CollArr = GetComponents<Coll>();
		}
	}
}


