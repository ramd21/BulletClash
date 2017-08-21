using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;


namespace BC
{
	public class TapMan : ManagedBehaviour
	{
		public GameObject _TapParticle;

		public float _Time;

		public ParticleController _ParticleController;

		Coroutine _Co;

		public override void ManagedUpdate()
		{
			if (RMInput.i.GetInptState(0) == RMInput.InputState.start)
			{
				if (_Co != null)
					StopCoroutine(_Co);

				_TapParticle.SetActive(false);
				_ParticleController.EnableEmit();
				_TapParticle.SetActive(true);
				_TapParticle.transform.position = RMInput.i.GetInputInfo(0)._V3WorldInputCur;
			}
			else
			{
				if (RMInput.i.GetInptState(0) == RMInput.InputState.input)
				{
					_TapParticle.transform.position = RMInput.i.GetInputInfo(0)._V3WorldInputCur;
				}
				else
				{
					if (RMInput.i.GetInptState(0) == RMInput.InputState.end)
					{
						_ParticleController.DisableEmit();

						if (_Co != null)
							StopCoroutine(_Co);

						_Co = this.WaitForSeconds(_Time, 
						() =>
						{
							_TapParticle.SetActive(false);
							
							_Co = null;
						});
					}
				}
			}
		}
	}
}



