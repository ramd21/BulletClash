﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace bat.util
{
    public class BAT_NodeUtil
    {
        /// <summary>
        /// List all children in target's children ,no including current game object.
        /// </summary>
        /// <param name="_transform">target transform</param>
        /// <returns>game object list</returns>
        public static List<GameObject> ListAllChildren(Transform _transform)
        {
            List<GameObject> childrenGo = new List<GameObject>();
            if (_transform != null)
            {
                GameObject _gameObject = _transform.gameObject;
                Transform[] array = _transform.GetComponentsInChildren<Transform>();
                for (int i = 0; i < array.Length; i++)
                {
                    var goI = array[i].gameObject;
                    if (!goI.Equals(_gameObject))
                    {
                        childrenGo.Add(goI);
                    }
                }
            }
            return childrenGo;
        }

        /// <summary>
        /// List all components in target's children ,no including current game object.
        /// </summary>
        /// <typeparam name="T">the type of component</typeparam>
        /// <param name="_transform">target transform</param>
        /// <returns>compnent list</returns>
        public static List<T> ListAllInChildren<T>(Transform _transform) where T:Component
        {
            List<T> compList = new List<T>();
            if (_transform != null)
            {
                GameObject _gameObject = _transform.gameObject;
                T[] array = _transform.GetComponentsInChildren<T>();
                for (int i = 0; i < array.Length; i++)
                {
                    var goI = array[i].gameObject;
                    if (!goI.Equals(_gameObject))
                    {
                        compList.Add(array[i]);
                    }
                }
            }
            return compList;
        }


        public static Transform CreateChild(Transform _tranform,string childName)
        {
            GameObject child = new GameObject(childName);
            var childTrans=child.transform;
            childTrans.SetParent(_tranform);
            return childTrans;
        }
    }
}
