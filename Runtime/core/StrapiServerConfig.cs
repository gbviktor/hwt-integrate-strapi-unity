using System;

using UnityEngine;

namespace com.gbviktor.hwtintegratestrapiunity
{
    [HelpURL("http://localhost:1337/admin/settings/api-tokens?sort=name:ASC")]
    [Serializable]
    public class StrapiServerConfig
    {
        [Header("Bearer ....")]
        [Tooltip("word Bearer inclusive. Generate token on http://localhost:1337/admin/settings/api-tokens")]
        public string Token = "Bearer ....";
        [Tooltip("Base url like http://localhost:1337")]
        public string URL = "http://localhost:1337";
        [Tooltip("After Strapi Version 4 response structure was changed by grouping properties of entity in attributes prop as array")]
        public bool StrapiV4OrAbove;
        [Tooltip("Transformer plugin must be configured, to transform responce of strapi without attributes and data keys")]
        public bool IsTransformerPluginInstalled;
    }
}