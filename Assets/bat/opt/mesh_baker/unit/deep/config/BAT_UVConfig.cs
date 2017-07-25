using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using bat.util;
using bat.opt.Bake.unit;

namespace bat.opt.Bake
{
    [Serializable]
    public class BAT_UVConfig
    {
        [SerializeField][Tooltip("Max texture size allowed")]
        public int m_maxTextureSize = 1024;
        [SerializeField][Tooltip("Shader texture names for this uv")]
        public BAT_TextureProperty[] m_uvTxtures;
        [SerializeField][Tooltip("Shader texture ids for this uv")]
        public int[] m_uvTxtureShaderIDs;

        public void Init()
        {
            int count=0;
            if (m_uvTxtures != null)
            {
                count = m_uvTxtures.Length;
            }
            m_uvTxtureShaderIDs = new int[count];
            for (int i = 0; i < m_uvTxtureShaderIDs.Length; i++)
            {
                int id = Shader.PropertyToID(m_uvTxtures[i].m_textureName);
                m_uvTxtureShaderIDs[i]=id;
            }
        }

        public void Refresh(Shader shader)
        {
#if UNITY_EDITOR
            m_uvTxtures = BAT_GfxUtil.ListTextures(shader).ToArray();
#endif
            Init();
        }

        public int UVTextureNum
        {
            get
            {
                return m_uvTxtureShaderIDs.Length;
            }
        }
        public void setMaxTureSize(int size)
        {
            if (size <= 32)
            {
                size = 32;
            }
            else
            {
                double pow = Math.Log(size, 2);
                int powInt = (int)Math.Ceiling(pow);
                size = (int)Math.Pow(2, powInt);
            }
            m_maxTextureSize = size;
        }
    }
    [Serializable]
    public class BAT_TextureProperty
    {
        [SerializeField][Tooltip("Texture Name")]
        public string m_textureName;
        [SerializeField][Tooltip("Is Normal?")]
        public bool m_isNormal;
        public BAT_TextureProperty()
        {
        }
        public BAT_TextureProperty(string textureName,bool isNormal)
        {
            m_textureName = textureName;
            m_isNormal = isNormal;
        }

    }
}