using bat.util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bat.opt.Bake.unit
{

    /// <summary>
    /// Deep merging group colloctor.
    /// Save diffrent of MeshFilters in table,they are grouped by dirrent shader.
    /// Game objects using the same shader will be placed into the same group. 
    /// For the deep merging,Bake the meshes, material and textures.
    /// </summary>
    public class BAT_DMGroupCollector
    {
        /// <summary>
        /// all groups
        /// </summary>
        private Dictionary<Shader, BAT_BakeGroup_Deep> m_groupTables = new Dictionary<Shader, BAT_BakeGroup_Deep>();
        public List<BAT_BakeGroup_Deep> Groups
        {
            get
            {
                List<BAT_BakeGroup_Deep> groups = new List<BAT_BakeGroup_Deep>();
                foreach (var g in m_groupTables.Values)
                {
                    groups.Add(g);
                }
                return groups;
            }
        }

        /// <summary>
        /// Collect MeshFilters by diffrent shader in all MeshFilters of target Children 
        /// </summary>
        /// <param name="deepBaker">target deepBaker</param>
        public void Collect(BAT_DeepBaker deepBaker)
        {
            //all the MeshFilter
            List<MeshFilter> meshFilters = BAT_NodeUtil.ListAllInChildren<MeshFilter>(deepBaker.transform);
            for (int i = 0; i < meshFilters.Count; i++)
            {
                MeshFilter meshFilterI = meshFilters[i];
                if (meshFilterI == null || meshFilterI.mesh == null)
                {
                    continue;
                }
                //check if exist MeshRenderer on the MeshFilter
                MeshRenderer meshRendererI = meshFilterI.GetComponent<MeshRenderer>();
                if (meshRendererI == null)
                {
                    continue;
                }
                //check if exist sharedMaterial in MeshRenderer
                Material material = meshRendererI.sharedMaterial;
                if (material == null)
                {
                    continue;
                }
                Shader shader_i = material.shader;
                if (material == null)
                {
                    continue;
                }
                //grop by main material
                BAT_BakeGroup_Deep group = null;
                if (m_groupTables.ContainsKey(shader_i))
                {
                    group = m_groupTables[shader_i];
                }
                else
                {
                    group = new BAT_BakeGroup_Deep();
                    group.m_shader = shader_i;
                    group.SetBakeConfig(deepBaker.FindConfig(shader_i));
                    m_groupTables.Add(shader_i, group);
                }
                //place into the group
                group.CollectMeshFilter(meshFilterI,material);
            }
        }
        public void Clear()
        {
            m_groupTables.Clear();
        }

    }
  


   
}
