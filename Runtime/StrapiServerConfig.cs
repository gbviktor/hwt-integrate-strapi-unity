using System;

using UnityEngine;

[HelpURL("http://localhost:1337/admin/settings/api-tokens?sort=name:ASC")]
[Serializable]
public class StrapiServerConfig
{
    [Header("Bearer ....")]
    [Tooltip("word Bearer inclusive. Generate token on http://localhost:1337/admin/settings/api-tokens")]
    public string Token = "Bearer ....";
    [Tooltip("Base url like http://localhost:1337")]
    public string URL = "http://localhost:1337";

}
