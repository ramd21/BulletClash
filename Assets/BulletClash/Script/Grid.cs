using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<VectorGrid> _VectorGridList;

    public Vector2 _Scroll;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < _VectorGridList.Count; i++)
        {
            _VectorGridList[i].Scroll(_Scroll);
        }
    }
}
