using UnityEngine;

public class SeParamMaster : ScriptableObject
{
	public SeParam[] _DatArr;
	public SeParam this[int i] { get { return _DatArr[i]; } }
}