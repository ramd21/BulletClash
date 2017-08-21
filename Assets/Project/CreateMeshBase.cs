using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RM;

[ExecuteInEditMode]
public abstract class CreateMeshBase : MonoBehaviour 
{
	//public Color _Color;
	protected MeshFilter _MeshFilter;
	protected MeshRenderer _MeshRenderer;
	protected Mesh _Mesh;

	[SerializeField]
	protected bool _EditorAwake;

	protected virtual void Awake()
	{
		if(!Application.isPlaying)
			_EditorAwake = true;

		_MeshFilter = gameObject.GetOrAddComponent<MeshFilter>();
		_MeshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
	}
}
