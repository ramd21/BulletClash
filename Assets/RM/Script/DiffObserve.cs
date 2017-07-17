using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class DiffObserve<T>
	{
		public T _Cur;
		public T _Last;

		public Action<T, T> _OnChanged;

		Coroutine _Coroutine;
		MonoBehaviour _Mono;
		public void StartObserve(MonoBehaviour aMono, Func<T> aTarget, Action<T, T> aOnChanged, bool aRepeat)
		{
			if (_Coroutine != null)
				return;

			_Mono = aMono;
			_Cur = aTarget();
			_Last = _Cur;

			_Coroutine = aMono.DoUntil(() =>
			{
				return !_Cur.Equals(_Last);
			},
			() =>
			{
				_Cur = aTarget();
			},
			() =>
			{
				aOnChanged(_Cur, _Last);
				_Coroutine = null;

				if (aRepeat)
					StartObserve(aMono, aTarget, aOnChanged, aRepeat);
			});
		}

		public void StartObserve(MonoBehaviour aMono, Func<T> aTarget, Action aOnChanged, bool aRepeat)
		{
			if (_Coroutine != null)
				return;

			_Mono = aMono;
			_Cur = aTarget();
			_Last = _Cur;

			_Coroutine = aMono.DoUntil(() =>
			{
				return !_Cur.Equals(_Last);
			},
			() =>
			{
				_Cur = aTarget();
			},
			() =>
			{
				aOnChanged();
				_Coroutine = null;

				if (aRepeat)
					StartObserve(aMono, aTarget, aOnChanged, aRepeat);
			});
		}

		public void EndObserve()
		{
			_Mono.StopCoroutine(_Coroutine);
			_Coroutine = null;
		}
	}
}


