using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimePiece : MonoBehaviour
{
    [SerializeField] private GameObject[] players;

    [SerializeField] private Camera activeCamera;

    [SerializeField] private Camera hiddenCamera;

    [SerializeField] private int hiddenSceneIndex;

    private bool isInPast = false;

    private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");

    private List<Rigidbody> _capsuleColliders = new();

    private void Start()
    {
        foreach (var player in players)
        {
            _capsuleColliders.Add(player.GetComponent<Rigidbody>());
        }

        SceneManager.LoadScene(hiddenSceneIndex, LoadSceneMode.Additive);

        var texture = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture(TimepieceTexture, texture);
        hiddenCamera.targetTexture = texture;
    }

    private void Update()
    {
        //hiddenCamera.Render();

        if (!Input.GetKeyDown(KeyCode.R)) return; // TODO: Convert to new input system

        activeCamera.targetTexture = hiddenCamera.targetTexture;
        hiddenCamera.targetTexture = null;

        // Swap cameras
        (activeCamera, hiddenCamera) = (hiddenCamera, activeCamera);

        var mask = LayerMask.NameToLayer(isInPast ? "Present" : "Past");
        foreach (var p in players)
        {
            SetLayerRecursively(p, mask);
        }

        foreach (var c in _capsuleColliders)
        {
            c.excludeLayers = mask;
        }

        isInPast = !isInPast;
    }

    private static void SetLayerRecursively(GameObject o, int layer)
    {
        o.layer = layer;
        for (var i = 0; i < o.transform.childCount; i++)
        {
            var child = o.transform.GetChild(i);
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}