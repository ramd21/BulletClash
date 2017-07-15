using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
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
			base.EditorUpdate();
			_CollArr = GetComponents<Coll>();
		}
	}
}


