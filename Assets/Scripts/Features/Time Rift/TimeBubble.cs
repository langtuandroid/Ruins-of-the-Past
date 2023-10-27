using UnityEngine;

public class TimeBubble : MonoBehaviour
{
    public GameObject source;

    public float radius = 0.0f;
    public float edgeThickness = 0.01f;

    private Material _material;

    private Camera _self;

    private static readonly int ScreenPos = Shader.PropertyToID("_ScreenPos");
    private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");
    private static readonly int Radius = Shader.PropertyToID("_Radius");
    private static readonly int EdgeThickness = Shader.PropertyToID("_EdgeThickness");

    private void Start()
    {
        _self = GetComponent<Camera>();
        _material = new Material(Shader.Find("Hidden/TimeBubble"));
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        _material.SetTexture(TimepieceTexture, Shader.GetGlobalTexture(TimepieceTexture));
        _material.SetVector(ScreenPos, _self.WorldToViewportPoint(source.transform.position));
        _material.SetFloat(Radius, radius);
        _material.SetFloat(EdgeThickness, edgeThickness);

        Graphics.Blit(src, dst, _material);
    }
}
