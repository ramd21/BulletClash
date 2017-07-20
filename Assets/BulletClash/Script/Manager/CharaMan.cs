using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class CharaMan : Singleton<CharaMan>
	{
		public List<Unit>[] _UnitList;
		public List<Bullet>[] _BulletList;

		Transform[] _TraPlayerParent;


		public void Init()
		{
			_UnitList = new List<Unit>[2];
			_UnitList[0] = new List<Unit>();
			_UnitList[1] = new List<Unit>();

			_BulletList = new List<Bullet>[2];
			_BulletList[0] = new List<Bullet>();
			_BulletList[1] = new List<Bullet>();

			_TraPlayerParent = new Transform[2];

		}

		public Unit GetPoolOrNewUnit(int aPlayerId, UnitType aType)
		{
			int len = _UnitList[aPlayerId].Count;
			Unit unit;
			for (int i = 0; i < len; i++)
			{
				unit = _UnitList[aPlayerId][i];
				if (unit._State == ActiveState.inactive && unit._ParamDef.Type == aType)
					return unit;
			}

			unit = ResourceMan.i.GetUnit(aType);
			unit = Instantiate(unit);

			if (!_TraPlayerParent[aPlayerId])
			{
				GameObject go = new GameObject("player_" + aPlayerId);
				go.transform.parent = transform;
				_TraPlayerParent[aPlayerId] = go.transform;
			}

			unit.transform.parent = _TraPlayerParent[aPlayerId];

			unit._ParamDef = MasterMan.i._UnitParam[(int)aType];
			unit._PlayerId = aPlayerId;

			_UnitList[aPlayerId].Add(unit);
			return unit;
		}

		public Bullet GetPoolOrNewBullet(int aPlayerId, BulletType aType)
		{
			int len = _BulletList[aPlayerId].Count;
			Bullet bullet;
			for (int i = 0; i < len; i++)
			{
				bullet = _BulletList[aPlayerId][i];
				if (bullet._State == ActiveState.inactive && bullet._ParamDef.Type == aType)
					return bullet;
			}

			bullet = ResourceMan.i.GetBullet(aType);
			bullet = Instantiate(bullet);

			if (!_TraPlayerParent[aPlayerId])
			{
				GameObject go = new GameObject("player_" + aPlayerId);
				go.transform.parent = transform;
				_TraPlayerParent[aPlayerId] = go.transform;
			}

			bullet.transform.parent = _TraPlayerParent[aPlayerId];
			bullet._ParamDef = MasterMan.i._BulletParam[(int)aType];
			bullet._PlayerId = aPlayerId;

			_BulletList[aPlayerId].Add(bullet);
			return bullet;
		}

		public void Act()
		{
			int len2;


			//activate_req>>
			Unit unit;
			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.activate_req)
					{
						unit._State = ActiveState.active;
						unit.gameObject.SetActive(true);
					}
				}
			}

			Bullet bullet;
			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.activate_req)
					{
						bullet._State = ActiveState.active;
						bullet.gameObject.SetActive(true);
					}
				}
			}
			//activate_req<<

			//act>>
			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.Move();
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.active)
						bullet.Move();
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.HitCheck();
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.active)
						bullet.HitCheck();
				}
			}


			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.Act();
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.active)
						bullet.Act();
				}
			}





			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.OMFrameEnd();
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.active)
						bullet.OMFrameEnd();
				}
			}


			//act<<


			//deactivate_req>>
			for (int i = 0; i < 2; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.deactivate_req)
					{
						unit._State = ActiveState.inactive;
						unit.gameObject.SetActive(false);
					}
				}
			}

			for (int i = 0; i < 2; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.deactivate_req)
					{
						bullet._State = ActiveState.inactive;
						bullet.gameObject.SetActive(false);
					}
				}
			}
			//deactivate_req<<
		}

		public void UpdateView()
		{
			int len, len2;
			len = _UnitList.Length;
			Unit unit;
			for (int i = 0; i < len; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.UpdateView();
				}
			}


			len = _BulletList.Length;
			Bullet bullet;
			for (int i = 0; i < len; i++)
			{
				len2 = _BulletList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					bullet = _BulletList[i][j];
					if (bullet._State == ActiveState.active)
						bullet.UpdateView();
				}
			}
		}
	}
}




