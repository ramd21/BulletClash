using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RM
{
	public class CombineMeshRoot : MonoBehaviour
	{
		Vector3 _V3LocalPos;
		Vector3 _V3LocalAngle;
		Vector3 _V3LocalScale;
		MeshFilter _MeshFilter;
		MeshFilter _meshFilter
		{
			get
			{
				if (!_MeshFilter)
					_MeshFilter = gameObject.GetOrAddComponent<MeshFilter>();

				return _MeshFilter;
			}
		}

		MeshRenderer _MeshRenderer;
		MeshRenderer _meshRenderer
		{
			get
			{
				if (!_MeshRenderer)
					_MeshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();

				return _MeshRenderer;
			}
		}
		void Awake()
		{
			_V3LocalPos = transform.localPosition;
			_V3LocalAngle = transform.localEulerAngles;
			_V3LocalScale = transform.localScale;

			transform.ResetTransform();


			MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
			CombineInstance[] combine = new CombineInstance[meshFilters.Length];
			int i = 0;
			while (i < meshFilters.Length)
			{
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
				meshFilters[i].gameObject.SetActive(false);
				i++;
			}
			_meshFilter.mesh = new Mesh();
			_meshFilter.mesh.CombineMeshes(combine);
			//meshFilters[i].gameObject.SetActive(true);
			_meshRenderer.sharedMaterial = meshFilters[1].GetComponent<MeshRenderer>().sharedMaterial;

			transform.ResetTransform();


			transform.localPosition = _V3LocalPos;
			transform.localEulerAngles = _V3LocalAngle;
			transform.localScale = _V3LocalScale;
			//transform.ResetPosition();

			//StaticBatchingUtility.Combine(gameObject);

			//List<MeshRenderer> mrList = gameObject.GetComponentsInChildrenList<MeshRenderer>();

			//for (int i = 0; i < mrList.Count; i++)
			//{
			//	if (i != 0)
			//		Destroy(mrList[i].gameObject);
			//}





		}

	}
}


