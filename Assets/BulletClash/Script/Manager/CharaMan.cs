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



		public void Init()
		{
			_UnitList = new List<Unit>[2];
			_UnitList[0] = new List<Unit>();
			_UnitList[1] = new List<Unit>();

			_BulletList = new List<Bullet>[2];
			_BulletList[0] = new List<Bullet>();
			_BulletList[1] = new List<Bullet>();
		}

		public Unit GetUnit(int aPlayerId, UnitType aType)
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
			unit._ParamDef = MasterMan.i._UnitParam[(int)aType];

			_UnitList[aPlayerId].Add(unit);
			return unit;
		}

		public void UnitActivateReq(int aPlayerId, Unit aUnit, Vector3 aPos)
		{
			aUnit.gameObject.SetActive(false);
			aUnit._State = ActiveState.activate_req;
			aUnit._Pos = aPos;
			aUnit._Param = aUnit._ParamDef;
		}

		public void UnitDeactivateReq(Unit aUnit)
		{
			aUnit._State = ActiveState.deactivate_req;
		}

		public void Act()
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
					if (unit._State == ActiveState.activate_req)
					{
						unit._State = ActiveState.active;
						unit.gameObject.SetActive(true);
					}
				}
			}

			for (int i = 0; i < len; i++)
			{
				len2 = _UnitList[i].Count;
				for (int j = 0; j < len2; j++)
				{
					unit = _UnitList[i][j];
					if (unit._State == ActiveState.active)
						unit.Act();
				}
			}


			for (int i = 0; i < len; i++)
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



