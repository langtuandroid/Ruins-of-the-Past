using UnityEngine;

namespace Features.Time_Rift
{
    public class TimeRift : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private static readonly int TimepieceTexture = Shader.PropertyToID("_HiddenSceneTexture");

        private Material _material;

        private void Start()
        {
            _material = GetComponent<Renderer>().sharedMaterial;
        }

        private void Update()
        {
            _material.SetTexture(MainTex, Shader.GetGlobalTexture(TimepieceTexture));
        }
    }
}