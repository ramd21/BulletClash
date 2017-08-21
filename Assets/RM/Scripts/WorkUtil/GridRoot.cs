using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace RM
{
	public class GridRoot : RMBehaviour
	{
#if UNITY_EDITOR
		public Vector2 _Inter;

		public int _RowCnt;

		public enum Mode
		{
			xy,
			xz,
			yx,
		}

		public Mode _Mode;

		public bool _Centering;

		[Button("Apply")]
		public int _Apply;

		void Apply()
		{
			int x;
			int y;

			int len = transform.childCount;

			for (int i = 0; i < len; i++)
			{
				x = i;
				if (_RowCnt != 0)
					x %= _RowCnt;

				y = 0;
				if (_RowCnt != 0)
					y = i / _RowCnt;


				transform.GetChild(i).ResetLocalPosition();

				switch (_Mode)
				{
					case Mode.xy:
						transform.GetChild(i).SetLocalPositionX(_Inter.x * x);
						transform.GetChild(i).SetLocalPositionY(_Inter.y * y);
						break;
					case Mode.xz:
						transform.GetChild(i).SetLocalPositionX(_Inter.x * x);
						transform.GetChild(i).SetLocalPositionZ(_Inter.y * y);
						break;
					case Mode.yx:
						transform.GetChild(i).SetLocalPositionY(_Inter.x * x);
						transform.GetChild(i).SetLocalPositionZ(_Inter.y * y);
						break;
				}
			}

			if (_Centering)
			{
				int hMax = len;
				if (_RowCnt != 0)
					hMax = _RowCnt;

				int yMax = 0;
				if (_RowCnt != 0)
					yMax = len / _RowCnt;

				float width = _Inter.x * (hMax - 1);
				float height = _Inter.y * (yMax - 1);

				for (int i = 0; i < len; i++)
				{
					switch (_Mode)
					{
						case Mode.xy:
							transform.GetChild(i).AddLocalPositionX(-width / 2);
							transform.GetChild(i).AddLocalPositionY(-height / 2);
							break;
						case Mode.xz:
							transform.GetChild(i).AddLocalPositionX(-width / 2);
							transform.GetChild(i).AddLocalPositionZ(-height / 2);
							break;
						case Mode.yx:
							transform.GetChild(i).AddLocalPositionY(-width / 2);
							transform.GetChild(i).AddLocalPositionZ(-height / 2);
							break;
					}
				}
			}
		}
#endif
	}
}

