using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace BC
{
	public class SoundMan : Singleton<SoundMan>
	{
		public AudioClip[] _BGMArr;
		public AudioClip[] _SEArr;

		public AudioSource _BGMChannel;
		public AudioSource[] _SeChannelArr;

		FastList<int> _SeReq = new FastList<int>(20, 10);
		FastList<float> _VolList = new FastList<float>(20, 10);


		public void PlaySeReq(int aId, float aVol)
		{
			if (!_SeReq.Contains(aId))
			{
				_SeReq.Add(aId);
				_VolList.Add(aVol);
			}
		}

		public void Act()
		{
			for (int i = 0; i < _SeReq.Count; i++)
			{
				for (int j = 0; j < _SeChannelArr.Length; j++)
				{
					if (!_SeChannelArr[j].isPlaying)
					{
						_SeChannelArr[j].clip = _SEArr[_SeReq[i]];
						_SeChannelArr[j].Play();
						_SeChannelArr[j].volume = _VolList[i];
						break;
					}
				}
			}

			_SeReq.Clear();
		}


#if UNITY_EDITOR



#endif
	}
}


