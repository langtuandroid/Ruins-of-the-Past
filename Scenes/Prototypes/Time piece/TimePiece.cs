using UnityEngine;
using UnityEngine.SceneManagement;

public class TimePiece : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Camera activeCamera;

    [SerializeField] private Camera hiddenCamera;

    private bool isInPast = false;

    private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");
    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        _capsuleCollider = player.GetComponent<CapsuleCollider>();
        SceneManager.LoadScene("Past", LoadSceneMode.Additive);

        var texture = new RenderTexture(Screen.width, Screen.height, 24);
        Shader.SetGlobalTexture(TimepieceTexture, texture);
        hiddenCamera.targetTexture = texture;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;

        activeCamera.targetTexture = hiddenCamera.targetTexture;
        hiddenCamera.targetTexture = null;

        // Swap cameras
        (activeCamera, hiddenCamera) = (hiddenCamera, activeCamera);

        var mask = LayerMask.NameToLayer(isInPast ? "Present" : "Past");
        SetLayerRecursively(player, mask);
        _capsuleCollider.excludeLayers = mask;

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