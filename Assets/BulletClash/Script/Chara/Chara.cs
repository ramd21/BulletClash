using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public struct BattleParam
	{
		public int _Hp;
	}

	public class Chara : EditorUpdateBehaviour
	{
		[SerializeField]
		Coll[] _CollArr;

		public BattleParam _BPDef;
		public BattleParam _BPCur;

		public override void EditorUpdate()
		{
			_CollArr = GetComponents<Coll>();
		}
	}
}


