using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class BattleCharaMan : Singleton<BattleCharaMan>
	{
		public List<Unit>[]		_UnitList;
		public List<Bullet>[]	_BulletList;
		public List<Tower>[]	_TowerList;
		public List<BulletHit> _BulletHitList;

		public Transform[] _TraPlayerParent;
		public Transform _TraEffParent;

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

			go = new GameObject("eff");
			go.transform.parent = transform;
			_TraEffParent = go.transform;
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
			u._PlayerId = aPlayerId;
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
			b._PlayerId = aPlayerId;
			b = Instantiate(b);
			b.transform.parent = _TraPlayerParent[aPlayerId];
			_BulletList[aPlayerId].Add(b);

			b.InstantiateInit(aPlayerId, aType);
			return b;
		}

		public BulletHit GetPoolOrNewBulletHit()
		{
			int len;
			len = _BulletHitList.Count;
			BulletHit bh;
			for (int i = 0; i < len; i++)
			{
				bh = _BulletHitList[i];
				if (bh._State == ActiveState.inactive)
					return bh;
			}
			GameObject go = ResourceMan.i.GetEffect(4);
			go = Instantiate(go);
			go.transform.parent = _TraEffParent;

			bh = go.AddComponent<BulletHit>();
			_BulletHitList.Add(bh);
			return bh;
		}

		void Activate<T>(List<T>[] aCharaList) where T : BHObj
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
						t.OnActivate();
					}
				}
			}
		}

		void Deactivate<T>(List<T>[] aCharaList) where T : BHObj
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
						t.OnDeactivate();
					}
				}
			}
		} 

		void Act<T>(List<T>[] aCharaList, Action<T> aOnAct) where T : BHObj
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

		void UpdateView<T>(List<T>[] aCharaList) where T : BHObj
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
						t.UpdateView();
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

			BattleCollMan.i.Clear();

			Act(_BulletList, (a) => a.OnFrameBegin());
			Act(_UnitList, (a) => a.OnFrameBegin());
			Act(_TowerList, (a) => a.OnFrameBegin());


			Act(_BulletList, (a) => a.SetPos());
			Act(_UnitList, (a) => a.SetPos());

			Act(_UnitList, (a) => a.SearchTage());
			Act(_TowerList, (a) => a.SearchTage());

			Act(_BulletList, (a) => a.HitCheck());
			Act(_UnitList, (a) => a.HitCheck());

			Act(_TowerList, (a) => a.Fire());
			Act(_UnitList, (a) => a.Fire());

			Act(_BulletList, (a) => a.DecTimer());


			Act(_BulletList, (a) => a.OnFrameEnd());
			Act(_UnitList, (a) => a.OnFrameEnd());
			Act(_TowerList, (a) => a.OnFrameEnd());


			int len;
			len = _BulletHitList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_BulletHitList[i]._State == ActiveState.active)
					_BulletHitList[i].Act();
			}

			//act<<


			//deactivate_req>>
			Deactivate(_UnitList);
			Deactivate(_BulletList);
			Deactivate(_TowerList);
			//deactivate_req<<
		}

		public void UpdateView()
		{
			UpdateView(_UnitList);
			UpdateView(_BulletList);
			UpdateView(_TowerList);

			int len;
			len = _BulletHitList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_BulletHitList[i]._State == ActiveState.active)
					_BulletHitList[i].UpdateView();
			}
		}
	}
}




