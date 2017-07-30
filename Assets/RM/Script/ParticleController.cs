using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RM
{
	public class ParticleController : RMBehaviour
	{
		public ParticleSystem[] _PSArr;

		void Awake()
		{
			_PSArr = GetComponentsInChildren<ParticleSystem>(true);
		}

		public void EnableEmit()
		{
			ParticleSystem.EmissionModule em;
			for (int i = 0; i < _PSArr.Length; i++)
			{
				em = _PSArr[i].emission;
				em.enabled = true;
			}
		}

		public void DisableEmit()
		{
			ParticleSystem.EmissionModule em;
			for (int i = 0; i < _PSArr.Length; i++)
			{
				em = _PSArr[i].emission;
				em.enabled = false;
			}
		} 
	}
}

