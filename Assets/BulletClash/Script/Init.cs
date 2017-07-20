using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RM;

namespace BC
{
	public class Init : MonoBehaviour
	{
		public int _ScreenW = 540;
		public int _ScreenH = 960;

		void Awake()
		{
			Screen.SetResolution(_ScreenH, _ScreenW, true);

			this.WaitForFrames(1, ()=> 
			{
				SceneManager.LoadScene("battle");
			});
		}
	}
}



