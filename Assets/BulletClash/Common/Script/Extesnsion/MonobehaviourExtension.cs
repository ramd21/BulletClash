using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	static public class MonobehaviourExtension
	{
		static public DiffObserve<T> StartObsserve<T>(this MonoBehaviour aThis, Func<T> aTarget, Action<T, T> aOnChanged, bool aRepeat)
		{
			DiffObserve<T> diff = new DiffObserve<T>();
			diff.StartObserve(aThis, aTarget, aOnChanged, aRepeat);
			return diff;
		}

		static public DiffObserve<T> StartObsserve<T>(this MonoBehaviour aThis, Func<T> aTarget, Action aOnChanged, bool aRepeat)
		{
			DiffObserve<T> diff = new DiffObserve<T>();
			diff.StartObserve(aThis, aTarget, aOnChanged, aRepeat);
			return diff;
		}

		static public Coroutine WaitForSeconds(this MonoBehaviour aThis, float aTime, Action aOnDone = null)
		{
			return aThis.StartCoroutine(WaitForSecondsCoroutine(aTime, aOnDone));
		}

		static public IEnumerator WaitForSecondsCoroutine(float aTime, Action aOnDone)
		{
			yield return new WaitForSeconds(aTime);
			if (aOnDone != null)
				aOnDone();
		}

		static public Coroutine WaitForEndOfFrame(this MonoBehaviour aThis, Action aOnDone = null)
		{
			return aThis.StartCoroutine(WaitForEndOfFrameCoroutine(aOnDone));
		}

		static public IEnumerator WaitForEndOfFrameCoroutine(Action aOnDone)
		{
			yield return new WaitForEndOfFrame();
			if (aOnDone != null)
				aOnDone();
		}

		static public Coroutine WaitUntil(this MonoBehaviour aThis, Func<bool> aCondition, Action aOnDone = null, bool aRepeat = false)
		{
			return aThis.StartCoroutine(WaitUntilCoroutine(aThis, aCondition, aOnDone, aRepeat));
		}

		static public IEnumerator WaitUntilCoroutine(MonoBehaviour aThis, Func<bool> aCondition, Action aOnDone, bool aRepeat)
		{
			yield return new WaitUntil(aCondition);
			if (aOnDone != null)
				aOnDone();

			if (aRepeat)
				aThis.WaitUntil(aCondition, aOnDone, aRepeat);
		}


		static public Coroutine DoUntil(this MonoBehaviour aThis, Func<bool> aCondition, Action aOnWait = null, Action aOnDone = null)
		{
			return aThis.StartCoroutine(DoUntilCoroutine(aCondition, aOnWait, aOnDone));
		}

		static public IEnumerator DoUntilCoroutine(Func<bool> aCondition, Action aOnWait, Action aOnDone)
		{
			while (true)
			{
				if (aCondition())
					break;

				if (aOnWait != null)
					aOnWait();
				
				yield return null;
			}
			if (aOnDone != null)
				aOnDone();
		}
	}
}


