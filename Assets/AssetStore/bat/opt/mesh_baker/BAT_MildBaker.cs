using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using bat.util;
using bat.opt.Bake.unit;

namespace bat.opt.Bake
{

	public class BAT_MildBaker : BAT_BakerBase
	{
		/// <summary>
        /// Bake all game objects under current GameObject,including meshes and materials.
        /// By default, baking will group the meshes by diffrent material(ShareMaterial).
		/// </summary>
        protected override GameObject Bake()
		{
            //all the MeshFilter
            List<MeshFilter> meshFilters = BAT_NodeUtil.ListAllInChildren<MeshFilter>(m_transform);
            //collect all groups
            BAT_BakeTable_Mild BakeTable = new BAT_BakeTable_Mild();
            BakeTable.Collect(meshFilters);
			//create the target game object of merging to
            var allBakedTo = BAT_NodeUtil.CreateChild(m_transform,"__AllBaked");

            int BakedID = 0;
            List<BAT_BakeGroup_Mild> groups = BakeTable.Groups;
            //merging by groups
			foreach(BAT_BakeGroup_Mild group in groups)
			{
				if(group.Count <= 0)
				{
					continue;
				}
                var childNode = BAT_NodeUtil.CreateChild(allBakedTo, "__shader_" + BakedID);
                BakeGroup(group, childNode);
                
                //clear mesh coponents
                ClearMeshComponents(group);

                BakedID++;
			}

            //clear resource not needed
			BakeTable.Clear();
			Resources.UnloadUnusedAssets();
            //Baked event
			if(OnBaked != null)
			{
				OnBaked();
			}
			return allBakedTo.gameObject;
		}

        protected void BakeGroup(BAT_BakeGroup_Mild group,Transform _root)
        {
            //Bake mesh of current group,if mesh vertexCount>=64k,
            //it would be seperated into several children
            int beginID = 0;
            int vertexCount = 0;
            int meshBakeID = 0;
            int currentID = 0;
            while (beginID < group.Count)
            {
                Mesh mesh_i = group[beginID].m_meshFilter.sharedMesh;
                int vertexCountI = mesh_i.vertexCount;
                //whether exceed the vertextCount
                bool exceedVC = (vertexCount + vertexCountI >= 65536);
                //the end of group
                bool endOfGroup = currentID >= group.Count - 1;
                //need Bake now? 
                bool needBake = false;
                if (exceedVC)
                {
                    needBake = true;
                }
                else if (endOfGroup)
                {
                    needBake = true;
                }
                //need merging now
                if (needBake)
                {
                    //create new child node
                    var childNode = BAT_NodeUtil.CreateChild(_root, _root.name + "__Baked_" + meshBakeID).gameObject;
                    int count;
                    int beginIDNext;
                    //one mesh's vertexCount exceed the max,Bake one
                    if (currentID == beginID)
                    {
                        count = 1;
                        beginIDNext = currentID + 1;
                        vertexCount = 0;
                    }
                    else
                    {
                        //exceed ,Bake [beginID,currentID)
                        if (exceedVC)
                        {
                            count = currentID - beginID;
                            beginIDNext = currentID;
                            vertexCount = vertexCountI;
                        }
                        else //end of group ,and not exceed,Bake [beginID,currentID]
                        {
                            count = group.Count - beginID;
                            beginIDNext = currentID + 1;
                            vertexCount = 0;
                        }
                    }
                    //start merging
                    Mesh BakedMesh = new Mesh();
                    BAT_BakerBase.Current.MarkAsset(BakedMesh);
                    CombineInstance[] combineInsts = new CombineInstance[count];

                    //collect all meshes of this group
                    for (int i = 0; i < count; i++)
                    {
                        var BakeUnitI = group[beginID + i] as BAT_BakeUnit_Mild;
                        var mfCI = BakeUnitI.m_meshFilter;
                        combineInsts[i].mesh = mfCI.sharedMesh;
                        combineInsts[i].transform = mfCI.transform.localToWorldMatrix;
                    }

                    try
                    {
                        BakedMesh.CombineMeshes(combineInsts, true, true);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("Error occured in merging " + count + " items \n " + e.Message);
                    }

                    //add MeshFilter to Baked child
                    MeshFilter mf_Baked = childNode.AddComponent<MeshFilter>();
                    mf_Baked.sharedMesh = BakedMesh;

                    //add MeshRenderer to Baked child
                    MeshRenderer mr_Baked = childNode.AddComponent<MeshRenderer>();
                    mr_Baked.sharedMaterials = group.m_sharedMaterials;

                    //set same layer with current 
                    childNode.layer = gameObject.layer;
 
                    //ready for next
                    beginID = beginIDNext;
                    meshBakeID++;
                }
                else //not need Bake, add the vertexCount
                {
                    vertexCount += vertexCountI;
                }
                currentID++;
            }

        }

	}
}