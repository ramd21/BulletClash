using UnityEngine;

public class BulletParamMaster : ScriptableObject
{
	public BulletParam[] _DatArr;
	public BulletParam this[int i] { get { return _DatArr[i]; } }
}