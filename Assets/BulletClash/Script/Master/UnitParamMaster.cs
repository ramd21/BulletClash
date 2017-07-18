using UnityEngine;

public class UnitParamMaster : ScriptableObject
{
	public UnitParam[] _DatArr;
	public UnitParam this[int i] { get { return _DatArr[i]; } }
}