using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[RequireComponent(typeof(BCTra))]
	public abstract class Chara : EditorUpdateBehaviour
	{
		public int _PlayerId;
		public ActiveState _State;
		public BCTra _Tra;

		static protected CharaMan			gCharaMan;
		static protected int				gLen;
		static protected int				gVs;
		static protected Unit				gU;
		static protected Bullet				gB;
		static protected Tower				gTW;
		static protected List<Unit>			gUnitList;
		static protected List<Bullet>		gBulletList;
		static protected List<Tower>		gTowerList;


		protected virtual void Start()
		{
			this.WaitForEndOfFrame(()=> 
			{
				gCharaMan = CharaMan.i;
			});
		}

		public abstract void UpdateView();
#if UNITY_EDITOR
		
#endif
	}
}


