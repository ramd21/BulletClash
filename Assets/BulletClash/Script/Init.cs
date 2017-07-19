using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
	public int _ScreenW = 540;
	public int _ScreenH = 960;
	void Awake()
	{
		Screen.SetResolution(_ScreenW, _ScreenH, true);

		SceneManager.LoadScene("BulletClash");
	}
}
