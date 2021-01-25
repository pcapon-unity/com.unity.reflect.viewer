using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Reflect;
using Unity.Reflect.Model;
using UnityEngine.Reflect.Pipeline;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Reflect.Viewer.Pipeline
{
    public class HierarchyConverterNode : ReflectNode<HierarchyConverter>
    {
        public GameObjectInput gameObjectInput = new GameObjectInput();

        private static int m_number;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Based on MetadataFilterNode found in MetadataFilter.cs
        /// </remarks>
        /// <param name="hook"></param>
        /// <param name="provider"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        protected override HierarchyConverter Create(ReflectBootstrapper hook, ISyncModelProvider provider, IExposedPropertyTable resolver)
        {
            m_number++;

            Debug.Log("HierarchyConverterNode: Create method " + m_number);

            var converter = new HierarchyConverter();

            gameObjectInput.streamEvent = converter.OnGameObjectStream;

            return converter;
        }
    }


    //public class MetadataFilterNode : ReflectNode<MetadataFilter>
    //{
    //    public StreamInstanceInput instanceInput = new StreamInstanceInput();
    //    public GameObjectInput gameObjectInput = new GameObjectInput();

    //    public MetadataFilterSettings settings;

    //    protected override MetadataFilter Create(ReflectBootstrapper hook, ISyncModelProvider provider, IExposedPropertyTable resolver)
    //    {
    //        var p = new MetadataFilter(settings);

    //        instanceInput.streamEvent = p.OnStreamInstanceEvent;
    //        instanceInput.streamEnd = p.OnStreamInstanceEnd;

    //        gameObjectInput.streamEvent = p.OnGameObjectAdded;
    //        gameObjectInput.streamEnd = p.OnGameObjectEnd;

    //        return p;
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// This class' serialized fields will show up in the inspector of the PipelineAsset.
    /// </remarks>
    public class HierarchyConverter : IReflectNodeProcessor
    {
        private HashSet<GameObject> m_gameObjects = new HashSet<GameObject>();

        private Dictionary<string, GameObject> m_parents = new Dictionary<string, GameObject>();

        public void OnPipelineInitialized()
        {
        }

        public void OnPipelineShutdown()
        {
        }

        public void OnGameObjectStream(SyncedData<GameObject> syncedData, StreamEvent streamEvent)
        {
            if (streamEvent == StreamEvent.Added)
            {
                OnGameObjectAdded(syncedData);
            }
            else if (streamEvent == StreamEvent.Removed)
            {
                OnGameObjectRemoved(syncedData);
            }
            else
            {
                Debug.Log("Something else");
            }
        }

        private void OnGameObjectAdded(SyncedData<GameObject> syncedData)
        {
            Debug.Log("GameObject added");

            m_gameObjects.Add(syncedData.data);

            if (syncedData.data.TryGetComponent<Metadata>(out var metadata))
            {
                Dictionary<string, Metadata.Parameter> parameters = metadata.GetParameters();

                //if(metadata.GetParameters().TryGetValue("Identity Data", out Metadata.Parameter identityData))
                //{
                //    identityData.
                //    string category =  parameter.value
                //    if(m_parents.ContainsKey())
                //}
            }
        }

        private void OnGameObjectRemoved(SyncedData<GameObject> syncedData)
        {
            Debug.Log("GameObject removed");

            m_gameObjects.Remove(syncedData.data);
        }
    }
}
