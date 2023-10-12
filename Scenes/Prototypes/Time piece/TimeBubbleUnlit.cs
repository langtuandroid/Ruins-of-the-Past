using UnityEngine;

public class TimeBubbleUnlit : MonoBehaviour
{
    public float scale = 1.0f;

    private Material _material;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");
    
    private void Start()
    {
        _material = GetComponent<Renderer>().sharedMaterial;
    }

    private void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        _material.SetTexture(MainTex, Shader.GetGlobalTexture(TimepieceTexture));
    }
}
