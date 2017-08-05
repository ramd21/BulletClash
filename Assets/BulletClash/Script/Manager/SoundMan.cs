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
		public SeChannel[] _SeChannel;

		public AudioSource _BgmChannel;
		
		public AudioClip[] _BGMArr;
		public AudioClip[] _SEArr;


		[System.Serializable]
		public struct SeChannel
		{
			public int _MaxSe;
			public Queue<AudioSource> _Queue;
			public HashSet<SeType> _Req;

			public void Init(GameObject aGo)
			{
				_Queue = new Queue<AudioSource>();
				_Req = new HashSet<SeType>();
				for (int i = 0; i < _MaxSe; i++)
				{
					_Queue.Enqueue(aGo.AddComponent<AudioSource>());
				}
			}
		}


		public void Init()
		{
			GameObject go;
			go = new GameObject("bgm_channel");
			go.transform.parent = transform;
			_BgmChannel = go.AddComponent<AudioSource>();

			go = new GameObject("se_channel");
			go.transform.parent = transform;

			for (int i = 0; i < _SeChannel.Length; i++)
			{
				_SeChannel[i].Init(gameObject);
			}
		}


		public void PlaySeReq(SeType aType, int aChannel)
		{
			if (_SeChannel[aChannel]._Req.Contains(aType))
				return;

			_SeChannel[aChannel]._Req.Add(aType);

			AudioSource aus = _SeChannel[aChannel]._Queue.Dequeue();
			aus.clip = _SEArr[(int)aType];
			aus.volume = MasterMan.i._SeParam[(int)aType].vol;
			aus.Play();
			_SeChannel[aChannel]._Queue.Enqueue(aus);
		}


		void LateUpdate()
		{
			for (int i = 0; i < _SeChannel.Length; i++)
			{
				_SeChannel[i]._Req.Clear();
			}
			
		}

#if UNITY_EDITOR
#endif
	}
}


