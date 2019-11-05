using UnityEngine;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(Dropdown))]
    public class XUDropdownListener : XUEventListener<XUDropdownListenerData>
    {
        Dropdown dropdown = null;

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
            dropdown = GetComponent<Dropdown>();
            if (dropdown != null)
            {
                dropdown.onValueChanged.AddListener(OnDropdownChange);
            }
        }

        private void OnDropdownChange(int value)
        {
            if (dropdown != null && mData != null && mData.eventChange.onEvent != null)
            {
                mData.eventChange.onEvent(XUEventType.DropdownValueChange, mData.eventChange.strEvent, mData.eventChange.objParam, dropdown, value);
            }
        }
    }
}