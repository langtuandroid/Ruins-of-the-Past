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
    // The objects required to open the door
    public List<Collider> requiredTriggers;

    // The parts of the door, related to the required triggers
    public List<GameObject> objects;

    // The renderer, fade states and initial colors of each object
    private List<Renderer> renderers = new List<Renderer>();
    private List<MoveOnTrigger> moveOnTriggers = new List<MoveOnTrigger>();
    private List<FadeState> fadeStates = new List<FadeState>();
    private List<Vector3> initialColors = new List<Vector3>();

    // The shader property id for the emission map color
    private int shaderPropertyId;

    // The collider to disable when the door is opened
    public Collider toggleableCollider;

    // How many triggers are currently in the triggerbox
    private int triggersActive = 0;

    // How fast the emission map should fade
    public float fadeSpeed = 3f;


    private Vector3 black = new Vector3(0, 0, 0);
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the shader property id for emission color
        shaderPropertyId = Shader.PropertyToID("_EmissionColor");

        // Set the lists per object
        foreach (GameObject obj in objects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer) renderers.Add(renderer);
            fadeStates.Add(FadeState.None);
            initialColors.Add(renderer.material.GetVector(shaderPropertyId));
            var moveOnTrigger = obj.GetComponent<MoveOnTrigger>();
            moveOnTriggers.Add(moveOnTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Get the index for the various lists
        int index = requiredTriggers.IndexOf(other);
        if (index == -1) return;

        // Get the object
        GameObject obj = objects[index];
        if (obj == null) return;

        // Since trigger left the box, remove from the active count
        triggersActive--;

        // Has renderer
        Renderer renderer = renderers[index];
        if (renderer == null) return;

        // Set fade state
        fadeStates[index] = FadeState.In;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get the index for the various lists
        int index = requiredTriggers.IndexOf(other);
        if (index == -1) return;

        // Get the object
        GameObject obj = objects[index];
        if (obj == null) return;

        // Since trigger entered the box, add to the active count
        triggersActive++;

        // Has renderer
        Renderer renderer = renderers[index];
        if (renderer == null) return;

        // Set fade state
        fadeStates[index] = FadeState.Out;
    }

    void TriggerMove()
    {
        // disable the emission mask logic
        opened = true;

        // disable the collider
        toggleableCollider.enabled = false;

        // disable the objects
        foreach (MoveOnTrigger trigger in moveOnTriggers)
        {
            trigger.TriggerMove();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if door is opened, stop
        if (opened) return;

        for (int i = 0; i < objects.Count; i++)
        {
            // Get object
            GameObject obj = objects[i];
            if (obj == null) continue;

            // Get fade state, renderer, current color
            FadeState fadeState = fadeStates[i];
            Renderer renderer = renderers[i];
            Vector3 current = renderer.material.GetVector(shaderPropertyId);
            Vector3 color;

            // Check fade state
            switch (fadeState)
            {
                case FadeState.In:
                    // If fading in, fade from current color to initial color
                    color = Vector3.MoveTowards(current, initialColors[i], fadeSpeed * Time.deltaTime);
                    renderer.material.SetVector(shaderPropertyId, color);
                    if (color == initialColors[i]) fadeStates[i] = FadeState.None;
                    break;
                case FadeState.Out:
                    // If fading out, fade from current color to black
                    color = Vector3.MoveTowards(current, black, fadeSpeed * Time.deltaTime);
                    renderer.material.SetVector(shaderPropertyId, color);
                    if (color == black) fadeStates[i] = FadeState.None;
                    break;
                default:
                    break;
            }
        }

        // If nothing is fading and the amount of required triggers are met, trigger the door opening
        if (triggersActive == requiredTriggers.Count && fadeStates.FindIndex(x => x != FadeState.None) == -1) TriggerMove();
    }
}
