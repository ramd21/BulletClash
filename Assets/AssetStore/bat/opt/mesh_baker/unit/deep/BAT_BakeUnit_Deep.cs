using bat.util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bat.opt.Bake.unit
{
    public class BAT_BakeUnit_Deep:BAT_BakeUnit
    {

        //Mesh ID in baking group
        public int m_meshID = -1;
        //uv of mesh after recalculated
        public Vector2[] m_uvs;
    }
}
