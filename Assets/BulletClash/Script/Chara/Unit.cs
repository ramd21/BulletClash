using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using UnityEngine.UI;

namespace BC
{
	public class Unit : Chara, IEditorUpdate
	{
		static int gCnt;
		public UnitParam _Param;
		public UnitParam _ParamDef;
		public Canvas _CvsHp;
		public Image _ImgHp;
		bool _AngleSet;

		public Coll _Coll;

		public bat.opt.Bake.BAT_DeepBaker _Bat;

		Chara _Tage;
		int _TageDist;

		Vector2Int _Force;

		public void InstantiateInit(int aPlayerId, UnitType aType)
		{
			_Id = gCnt;
			gCnt++;

			_PlayerId = aPlayerId;
			_VSPlayerId = (_PlayerId + 1) % 2;
			_Type = CharaType.unit;
			_ParamDef = MasterMan.i._UnitParam[(int)aType];

			_Coll.InstantiateInit(_PlayerId, this);
		}

		public override void OnFrameBegin()
		{
			base.OnFrameBegin();

			_Coll.UpdateBlock();
			_Coll.AddToCollMan();
		}

		public override void ActivateReq(Vector2Int aPos)
		{
			base.ActivateReq(aPos);
			_Param = _ParamDef;
		}

		public void SetPos()
		{
			if (_PlayerId == 0)
				_Tra._Pos.y += _Param.SpdY;
			else
				_Tra._Pos.y -= _Param.SpdY;

			_Tra._Pos += _Force;

			if (_Tage)
			{
				if (_Tage._Tra._Pos.x > _Tra._Pos.x)
				{
					_Tra._Pos.x += _Param.SpdX;
					if (_Tra._Pos.x > _Tage._Tra._Pos.x)
						_Tra._Pos.x = _Tage._Tra._Pos.x;
				}
				else
				{
					if (_Tage._Tra._Pos.x < _Tra._Pos.x)
					{
						_Tra._Pos.x -= _Param.SpdX;
						if (_Tra._Pos.x < _Tage._Tra._Pos.x)
							_Tra._Pos.x = _Tage._Tra._Pos.x;
					}
				}
			}

			if (_Tra._Pos.y > FieldMan.i._Size.y)
			{
				DeactivateReq();
				return;
			}

			if (_Tra._Pos.y < 0)
			{
				DeactivateReq();
				return;

			}
		}

		public void HitCheck()
		{
			FastList<Coll> collList;
			Coll c;
			int len;
			for (int i = 0; i < 9; i++)
			{
				collList = CollMan.i.GetCollList(_VSPlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
						AddForce(_Tra._Pos - c._Tra._Pos, 50);
				}

				collList = CollMan.i.GetCollList(_PlayerId, CharaType.unit, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
						AddForce(_Tra._Pos - c._Tra._Pos, 50);
				}

				collList = CollMan.i.GetCollList(_VSPlayerId, CharaType.tower, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
						AddForce(_Tra._Pos - c._Tra._Pos, 50);
				}

				collList = CollMan.i.GetCollList(_PlayerId, CharaType.tower, _Coll._CollBlock[i]);
				len = collList.Count;
				for (int j = 0; j < len; j++)
				{
					c = collList[j];
					if (_Coll.IsHit(c))
						AddForce(_Tra._Pos - c._Tra._Pos, 50);
				}
			}
		}

		public void AddForce(Vector2Int aDir, int aPow)
		{
			_Force = aDir.normalized * aPow;
		}

		public void Dmg(int aDmg)
		{
			_Param.Hp -= aDmg;
		}

		public void SearchTage()
		{
			_TageDist = int.MaxValue;
			int dist;
			int len;
			_Tage = null;

			Unit u;
			len = CharaMan.i._UnitList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				u = CharaMan.i._UnitList[_VSPlayerId][i];
				if (u._State == ActiveState.active)
				{
					if (_PlayerId == 0)
					{
						if (u._Tra._Pos.y <= _Tra._Pos.y)
							continue;
					}
					else
					{
						if (u._Tra._Pos.y >= _Tra._Pos.y)
							continue;
					}

					dist = RMMath.GetApproxDist(_Tra._Pos.x, _Tra._Pos.y, u._Tra._Pos.x, u._Tra._Pos.y);
					if (dist < _TageDist)
					{
						_Tage = u;
						_TageDist = dist;
					}
				}
			}

			Tower tw;
			len = CharaMan.i._TowerList[_VSPlayerId].Count;
			for (int i = 0; i < len; i++)
			{
				tw = CharaMan.i._TowerList[_VSPlayerId][i];
				if (tw._State == ActiveState.active)
				{
					if (_PlayerId == 0)
					{
						if (tw._Tra._Pos.y <= _Tra._Pos.y)
							continue;
					}
					else
					{
						if (tw._Tra._Pos.y >= _Tra._Pos.y)
							continue;
					}

					dist = RMMath.GetApproxDist(_Tra._Pos.x, _Tra._Pos.y, tw._Tra._Pos.x, tw._Tra._Pos.y);
					if (dist < _TageDist)
					{
						_Tage = tw;
						_TageDist = dist;
					}
				}
			}
		}

		public void Fire()
		{
			//if (!_Tage)
			//	return;

			//if (_TageDist > _Param.Range)
			//	return;

			if (_Param.FireInter == 0)
			{
				Bullet b = CharaMan.i.GetPoolOrNewBullet(_PlayerId, _Param.Bullet);
				if (_PlayerId == 0)
					b.ActivateReq(_Tra._Pos, Vector2Int.up);
				else
					b.ActivateReq(_Tra._Pos, Vector2Int.down);
				_Param.FireInter = _ParamDef.FireInter;
			}

			_Param.FireInter--;
		}

		public override void OnFrameEnd()
		{
			if (_Param.Hp <= 0)
				DeactivateReq();

			_Force.x = (_Force.x * 975) / 1000;
			_Force.y = (_Force.y * 975) / 1000;
		}

		public override void UpdateView()
		{
			base.UpdateView();

			if (_Param.Hp == _ParamDef.Hp)
			{
				_CvsHp.gameObject.SetActive(false);
			}
			else
			{
				_CvsHp.gameObject.SetActive(true);
				_ImgHp.fillAmount = (float)_Param.Hp / _ParamDef.Hp;
			}

			if (!_AngleSet)
			{
				if (_PlayerId == 1)
					transform.SetEulerAnglesY(180);
				_AngleSet = true;
			}
		}

#if UNITY_EDITOR
		public void EditorUpdate()
		{
			_CvsHp = GetComponentInChildren<Canvas>();
			_ImgHp = transform.FindRecurcive<Image>("hp", true);
			_Bat = GetComponentInChildren<bat.opt.Bake.BAT_DeepBaker>();

			_Coll = GetComponent<Coll>();
			_Tra = GetComponent<BCTra>();
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1.5f);
		}
#endif

	}
}


