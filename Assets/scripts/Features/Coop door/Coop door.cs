using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum FadeState
{
    None,
    In,
    Out
}

public class Coopdoor : MonoBehaviour
{
    public Vector4 leftColor;
    public Vector4 rightColor;

    public List<Collider> requiredTriggers;
    public List<GameObject> objects;
    private List<Renderer> renderers = new List<Renderer>();
    private List<FadeState> fadeStates = new List<FadeState>();
    private List<Vector3> initialColors = new List<Vector3>();
    private int shaderPropertyId;

    public Collider toggleableCollider;
    private int triggersActive = 0;
    public float fadeSpeed = 3f;
    private Vector3 black = new Vector3(0, 0, 0);
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        shaderPropertyId = Shader.PropertyToID("_EmissionColor");
        foreach (GameObject obj in objects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer) renderers.Add(renderer);
            fadeStates.Add(FadeState.None);
            initialColors.Add(renderer.material.GetVector(shaderPropertyId));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int index = requiredTriggers.IndexOf(other);
        if (index == -1) return;

        GameObject obj = objects[index];
        if (obj == null) return;

        triggersActive--;

        Renderer renderer = renderers[index];
        if (renderer == null) return;

        fadeStates[index] = FadeState.In;
    }

    private void OnTriggerEnter(Collider other)
    {
        int index = requiredTriggers.IndexOf(other);
        if (index == -1) return;

        GameObject obj = objects[index];
        if (obj == null) return;

        triggersActive++;

        Renderer renderer = renderers[index];
        if (renderer == null) return;

        fadeStates[index] = FadeState.Out;
    }

    void TriggerMove()
    {
        opened = true;
        toggleableCollider.enabled = false;

        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opened) return;
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            if (obj == null) continue;

            FadeState fadeState = fadeStates[i];
            Renderer renderer = renderers[i];
            Vector3 current = renderer.material.GetVector(shaderPropertyId);
            Vector3 color;
            switch (fadeState)
            {
                case FadeState.In:
                    color = Vector3.MoveTowards(current, initialColors[i], fadeSpeed * Time.deltaTime);
                    renderer.material.SetVector(shaderPropertyId, color);
                    if (color == initialColors[i]) fadeStates[i] = FadeState.None;
                    break;
                case FadeState.Out:
                    color = Vector3.MoveTowards(current, black, fadeSpeed * Time.deltaTime);
                    renderer.material.SetVector(shaderPropertyId, color);
                    if (color == black) fadeStates[i] = FadeState.None;
                    break;
                default:
                    break;
            }
        }
        if (triggersActive == requiredTriggers.Count && fadeStates.FindIndex(x => x != FadeState.None) == -1) TriggerMove();
    }
}
