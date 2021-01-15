using System.Collections;
using System.Collections.Generic;
using SharpFlux;
using Unity.TouchFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Reflect.Viewer.UI
{
    [RequireComponent(typeof(DialogWindow))]
    public class OpsHealthOptionsUIController : MonoBehaviour
    {
#pragma warning disable 649

        [SerializeField]
        Button m_DialogButton;
        [SerializeField]
        SlideToggle m_ShowIotGameObjectsToggle;

#pragma warning restore 649

        DialogWindow m_DialogWindow;
        Image m_DialogButtonImage;
        OpsHealthData m_CurrentOpsHealthOptionsData;

        private void Awake()
        {
            UIStateManager.stateChanged += OnStateDataChanged;

            m_DialogButtonImage = m_DialogButton.GetComponent<Image>();
            m_DialogWindow = GetComponent<DialogWindow>();
        }

        void Start()
        {
            m_DialogButton.onClick.AddListener(OnDialogButtonClicked);
            m_ShowIotGameObjectsToggle.onValueChanged.AddListener(OnShowIotToggleChanged);
        }

        void OnDialogButtonClicked()
        {
            var dialogType = m_DialogWindow.open ? DialogType.None : DialogType.OpsHealthOptions;
            UIStateManager.current.Dispatcher.Dispatch(Payload<ActionTypes>.From(ActionTypes.OpenDialog, dialogType));
        }

        void OnShowIotToggleChanged(bool on)
        {
            var data = UIStateManager.current.stateData.opsHealthData;
            data.showGameObjectsWithIotData = on;
            UIStateManager.current.Dispatcher.Dispatch(Payload<ActionTypes>.From(ActionTypes.SetShowIotDataOption, data));
        }

        void OnStateDataChanged(UIStateData data)
        {
            m_DialogButtonImage.enabled = data.activeDialog == DialogType.OpsHealthOptions;
            m_DialogButton.interactable = data.toolbarsEnabled;

            if (m_CurrentOpsHealthOptionsData != data.opsHealthData)
            {
                if (m_CurrentOpsHealthOptionsData.showGameObjectsWithIotData != data.opsHealthData.showGameObjectsWithIotData)
                {
                    m_CurrentOpsHealthOptionsData.showGameObjectsWithIotData = data.opsHealthData.showGameObjectsWithIotData;
                }

                m_CurrentOpsHealthOptionsData = data.opsHealthData;
            }
        }
    }
}
