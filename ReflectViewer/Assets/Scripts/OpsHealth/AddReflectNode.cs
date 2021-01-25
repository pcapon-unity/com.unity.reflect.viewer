using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Reflect;
using Unity.Reflect.Model;
using UnityEngine.Reflect.Pipeline;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Reflect.Viewer.Pipeline
{
    public class AddReflectNode : MonoBehaviour
    {
        [SerializeField]
        private ReflectPipeline m_reflectPipeline;

        private void Awake()
        {
            m_reflectPipeline.beforeInitialize += OnBeforeInitialize;
        }

        private void OnDestroy()
        {
            // Attempt to remove node added at runtime.
            if (m_reflectPipeline.pipelineAsset.TryGetNode<HierarchyConverterNode>(out var node))
            {
                Debug.Log("Removing Node");
                m_reflectPipeline.pipelineAsset.nodes.Remove(node);
            }
        }

        private void OnBeforeInitialize()
        {
            Debug.Log("Before Initialize. Adding Node");

            // CreateNode seems like it was designed to only be called in Edit Mode. Because it's actually adding the node to the SO so it persists even after you exit Play Mode.
            if (!m_reflectPipeline.pipelineAsset.TryGetNode<HierarchyConverterNode>(out var node))
            {
                m_reflectPipeline.pipelineAsset.CreateNode<HierarchyConverterNode>();
            }
        }
    }
}
