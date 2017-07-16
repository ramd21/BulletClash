using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace RM
{
	public class UIMan : Singleton<UIMan>, IEditorUpdate
	{
		public Image[] _ImgTacticsPointGaugeArr;

		public void Init()
		{
			for (int i = 0; i < _ImgTacticsPointGaugeArr.Length; i++)
			{
				Image img = _ImgTacticsPointGaugeArr[i];
				img.fillAmount = 0;
				this.StartObsserve(() => img.fillAmount,
				(cur, last) =>
				{
					if (last != 0 && cur == 1)
					{
						img.transform.parent.DOScale(1.5f, 0.25f).OnComplete(() =>
						{
							img.transform.parent.DOScale(1f, 0.25f);
						});
					}
				}, true);
			}
		}

		public void SetTacticsPoint(int aTacticsPoint)
		{
			int oneGauge = GameMan.i._MaxTacticsPoint / _ImgTacticsPointGaugeArr.Length;

			float fill;

			for (int i = 0; i < _ImgTacticsPointGaugeArr.Length; i++)
			{
				fill = (float)(aTacticsPoint - i * oneGauge) / oneGauge;
				_ImgTacticsPointGaugeArr[i].fillAmount = fill;
			}
		}

		public void EditorUpdate()
		{
			_ImgTacticsPointGaugeArr = transform.FindAllRecurcive<Image>("fill", true);
		}
	}
}


