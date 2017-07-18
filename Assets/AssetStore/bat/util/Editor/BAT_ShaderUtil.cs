using bat.opt.Bake;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace bat.util
{
    public class BAT_ShaderUtil
    {
        /// <summary>
        /// check the shader and try to list all the texture properties.
        /// </summary>
        /// <param name="shader">shader</param>
        /// <returns>Texture properties</returns>
        public static List<BAT_TextureProperty> ListTextures(Shader shader)
        {
            List<BAT_TextureProperty> list = new List<BAT_TextureProperty>();
            int propertyCount =  ShaderUtil.GetPropertyCount(shader);
            for (int i = 0; i < propertyCount; i++)
            {
                if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
                {
                    string propertyName = ShaderUtil.GetPropertyName(shader, i);
                    BAT_TextureProperty tp = new BAT_TextureProperty();
                    tp.m_textureName = propertyName;
                    string pLow=propertyName.ToLower();
                    tp.m_isNormal = pLow.Contains("normal")||pLow.Contains("bump");
                    list.Add(tp);
                }
            }
            return list;
        }

    }
}
