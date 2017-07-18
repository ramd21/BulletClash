using System;
using System.Collections.Generic;
using UnityEngine;

namespace bat.opt.Bake.unit
{

    /// <summary>
    /// Table of diffrent MeshFilter,they are grouped by dirrent main material(SharedMaterial).
    /// </summary>
    public class BAT_BakeTable_Mild
    {
        /// <summary>
        /// all groups
        /// </summary>
        private Dictionary<Material, BAT_BakeGroup_Mild> m_groupTables = new Dictionary<Material, BAT_BakeGroup_Mild>();

        public List<BAT_BakeGroup_Mild> Groups
        {
            get
            {
                List<BAT_BakeGroup_Mild> groups = new List<BAT_BakeGroup_Mild>();
                foreach (var g in m_groupTables.Values)
                {
                    groups.Add(g);
                }
                return groups;
            }
        }

        /// <summary>
        /// group MeshFilters by main material(sharedMaterial).
        /// if exsit two MeshFilter,using the same main meterial(sharedMaterial),
        /// but the diffrent sharedMaterials,it would be recognized as the same meterials ,
        /// use the first marked sharedMaterials.
        /// </summary>
        /// <param name="meshFilters">MeshFilter list</param>
        public void Collect(List<MeshFilter> meshFilters)
        {
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
                //grop by main material
                BAT_BakeGroup_Mild group = null;
                if (m_groupTables.ContainsKey(material))
                {
                    group = m_groupTables[material];
                }
                else
                {
                    group = new BAT_BakeGroup_Mild();
                    group.m_sharedMaterial = material;
                    group.m_sharedMaterials = meshRendererI.sharedMaterials;
                    m_groupTables.Add(material, group);
                }
                //place into the group
                group.m_BakeUnits.Add(new BAT_BakeUnit_Mild(meshFilterI));

            }
        }
        public void Clear()
        {
            m_groupTables.Clear();
        }

    }
 
}
