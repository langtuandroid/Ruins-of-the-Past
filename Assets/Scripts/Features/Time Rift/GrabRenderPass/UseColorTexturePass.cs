using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.Universal;

namespace Features.Time_Rift.GrabRenderPass
{
    public class UseColorTexturePass : ScriptableRenderPass
    {
        private readonly ProfilingSampler _profilingSampler = new ProfilingSampler(nameof(UseColorTexturePass));
        private readonly List<ShaderTagId> _shaderTagIds;
        private readonly SortingCriteria _sortingCriteria;

        private FilteringSettings _filteringSettings;

        public UseColorTexturePass(GrabTiming timing, IEnumerable<string> shaderLightModes,
            SortingCriteria sortingCriteria)
        {
            _filteringSettings = new FilteringSettings(RenderQueueRange.all);

            // Add 1 for processing after GrabColorTexturePass.
            renderPassEvent = timing.ToRenderPassEvent() + 1;

            _shaderTagIds = new List<ShaderTagId>();
            foreach (var shaderLightMode in shaderLightModes)
            {
                _shaderTagIds.Add(new ShaderTagId(shaderLightMode));
            }

            _sortingCriteria = sortingCriteria;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, _profilingSampler))
            {
                // Render objects with specified LightModes.
                var drawingSettings =
                    CreateDrawingSettings(_shaderTagIds, ref renderingData, _sortingCriteria);
                //context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref _filteringSettings);

                var rendererListParams = new RendererListParams(
                    renderingData.cullResults,
                    drawingSettings,
                    _filteringSettings
                );
                cmd.DrawRendererList(context.CreateRendererList(ref rendererListParams));

                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                CommandBufferPool.Release(cmd);
            }
        }
    }
}