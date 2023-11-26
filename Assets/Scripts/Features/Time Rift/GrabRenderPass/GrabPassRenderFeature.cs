using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Features.Time_Rift.GrabRenderPass
{
    [Serializable]
    public class GrabPassRenderFeature : ScriptableRendererFeature
    {
        private const string DefaultShaderLightMode = "UseColorTexture";
        private const string DefaultGrabbedTextureName = "_GrabbedTexture";

        [SerializeField] [Tooltip("When to grab color texture.")]
        private GrabTiming timing = GrabTiming.AfterTransparents;

        [SerializeField] [Tooltip("Texture name to use in the shader.")]
        private string grabbedTextureName = DefaultGrabbedTextureName;

        [SerializeField] [Tooltip("Light Mode of shaders that use grabbed texture.")]
        private List<string> shaderLightModes = new() {DefaultShaderLightMode};

        [SerializeField] [Tooltip("How to sort objects during rendering.")]
        private SortingCriteria sortingCriteria = SortingCriteria.CommonTransparent;

        private GrabColorTexturePass _grabColorTexturePass;
        private UseColorTexturePass _useColorTexturePass;

        public override void Create()
        {
            _grabColorTexturePass = new GrabColorTexturePass(timing, grabbedTextureName);
            _useColorTexturePass = new UseColorTexturePass(timing, shaderLightModes, sortingCriteria);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _grabColorTexturePass.BeforeEnqueue(renderer);
            renderer.EnqueuePass(_grabColorTexturePass);
            renderer.EnqueuePass(_useColorTexturePass);
        }
    }
}