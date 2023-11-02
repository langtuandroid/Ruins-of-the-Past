using UnityEngine;

public class TimeBubbleUnlit : MonoBehaviour
{
    private Material _material;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");
    
    private void Start()
    {
        _material = GetComponent<Renderer>().sharedMaterial;
    }

    private void Update()
    {
        _material.SetTexture(MainTex, Shader.GetGlobalTexture(TimepieceTexture));
    }
}
