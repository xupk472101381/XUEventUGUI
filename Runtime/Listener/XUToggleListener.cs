using UnityEngine;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(Toggle))]
    public class XUToggleListener : XUEventListener<XUToggleListenerData>
    {
        Toggle toggle = null;

        public void SetEventChange(string strEvent, object objParam, EventDelegateXU onEvent)
        {
            mData.eventChange.Set(strEvent, objParam, onEvent);
        }

        public void SetEventChange(string strEvent, object objParam)
        {
            mData.eventChange.Set(strEvent, objParam);
        }

        public void RemoveEventChange()
        {
            mData.eventChange.Reset();
        }

        public void GetEventChange(out string strEvent, out object objParam)
        {
            strEvent = mData.eventChange.strEvent;
            objParam = mData.eventChange.objParam;
        }

        protected override void Start()
        {
            toggle = GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(OnToggleChange);
            }
        }

        private void OnToggleChange(bool value)
        {
            if (toggle != null && mData != null && mData.eventChange.onEvent != null)
            {
                mData.eventChange.onEvent(XUEventType.ToggleValueChange, mData.eventChange.strEvent, mData.eventChange.objParam, toggle, value);
            }
        }
    }
}