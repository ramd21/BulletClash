using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class CharaMan : Singleton<CharaMan>
	{
		public List<Unit>[]		_UnitList;
		public List<Bullet>[]	_BulletList;
		public List<Tower>[]	_TowerList;

		public Transform[] _TraPlayerParent;

		//static int len;


		public void Init()
		{
			_UnitList = new List<Unit>[2];
			_UnitList[0] = new List<Unit>();
			_UnitList[1] = new List<Unit>();

			_BulletList = new List<Bullet>[2];
			_BulletList[0] = new List<Bullet>();
			_BulletList[1] = new List<Bullet>();

			_TowerList = new List<Tower>[2];
			_TowerList[0] = new List<Tower>();
			_TowerList[1] = new List<Tower>();

			_TraPlayerParent = new Transform[2];

			GameObject go;

			go = new GameObject("player_" + 0);
			go.transform.parent = transform;
			_TraPlayerParent[0] = go.transform;

			go = new GameObject("player_" + 1);
			go.transform.parent = transform;
			_TraPlayerParent[1] = go.transform;
		}

		public Unit GetPoolOrNewUnit(int aPlayerId, UnitType aType)
		{
			int len;
			len = _UnitList[aPlayerId].Count;
			Unit u;
			for (int i = 0; i < len; i++)
			{
				u = _UnitList[aPlayerId][i];
				if (u._State == ActiveState.inactive && u._ParamDef.Type == aType)
					return u;
			}

			u = ResourceMan.i.GetUnit(aType);
			u = Instantiate(u);
			u.transform.parent = _TraPlayerParent[aPlayerId];
			_UnitList[aPlayerId].Add(u);
			u.InstantiateInit(aPlayerId, aType);
			return u;
		}

		public Bullet GetPoolOrNewBullet(int aPlayerId, BulletType aType)
		{
			int len;
			len = _BulletList[aPlayerId].Count;
			Bullet b;
			for (int i = 0; i < len; i++)
			{
				b = _BulletList[aPlayerId][i];
				if (b._State == ActiveState.inactive && b._ParamDef.Type == aType)
					return b;
			}

			b = ResourceMan.i.GetBullet(aType);
			b = Instantiate(b);
			b.transform.parent = _TraPlayerParent[aPlayerId];
			_BulletList[aPlayerId].Add(b);

			b.InstantiateInit(aPlayerId, aType);
			return b;
		}

		void Activate<T>(List<T>[] aCharaList) where T : Chara
		{
			int len;
			T t;
			for (int i = 0; i < 2; i++)
			{
				len = aCharaList[i].Count;
				for (int j = 0; j < len; j++)
				{
					t = aCharaList[i][j];
					if (t._State == ActiveState.activate_req)
					{
						t._State = ActiveState.active;
						t.gameObject.SetActive(true);
					}
				}
			}
		}

		void Deactivate<T>(List<T>[] aCharaList) where T : Chara
		{
			int len;
			T t;
			for (int i = 0; i < 2; i++)
			{
				len = aCharaList[i].Count;
				for (int j = 0; j < len; j++)
				{
					t = aCharaList[i][j];
					if (t._State == ActiveState.deactivate_req)
					{
						t._State = ActiveState.inactive;
						t.gameObject.SetActive(false);
					}
				}
			}
		}

		void Act<T>(List<T>[] aCharaList, Action<T> aOnAct) where T : Chara
		{
			int len;
			T t;
			for (int i = 0; i < 2; i++)
			{
				len = aCharaList[i].Count;
				for (int j = 0; j < len; j++)
				{
					t = aCharaList[i][j];
					if (t._State == ActiveState.active)
					{
						aOnAct(t);
					}
				}
			}
		}

		public void Act()
		{
			//activate_req>>
			Activate(_UnitList);
			Activate(_BulletList);
			Activate(_TowerList);
			//activate_req<<

			//act>>

			//CollMan.i.Clear();

			Act(_UnitList, (a) => a.SetPos());
			Act(_BulletList, (a) => a.SetPos());

			Act(_TowerList, (a) => a.SearchTage());

			Act(_BulletList, (a) => a.HitBulletCheck());
			Act(_BulletList, (a) => a.HitUnitCheck());

			Act(_TowerList, (a) => a.Fire());
			Act(_UnitList, (a) => a.Fire());
			Act(_BulletList, (a) => a.DecTimer());


			Act(_UnitList, (a) => a.OMFrameEnd());
			Act(_BulletList, (a) => a.OMFrameEnd());



			//act<<


			//deactivate_req>>
			Deactivate(_UnitList);
			Deactivate(_BulletList);
			Deactivate(_TowerList);
			//deactivate_req<<
		}

		public void UpdateView()
		{
			int len, len2;
			Unit u;
			Bullet b;
			Tower tw;


			len = _UnitList.Length;
			for (int i = 0; i < len; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					u = _UnitList[i][j];
					if (u._State == ActiveState.active)
						u.UpdateView();
				}
			}


			len = _BulletList.Length;
			for (int i = 0; i < len; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					b = _BulletList[i][j];
					if (b._State == ActiveState.active)
						b.UpdateView();
				}
			}

			len = _TowerList.Length;
			for (int i = 0; i < len; i++)
			{
				len2 = _TowerList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					tw = _TowerList[i][j];
					if (tw._State == ActiveState.active)
						tw.UpdateView();
				}
			}
		}
	}
}




