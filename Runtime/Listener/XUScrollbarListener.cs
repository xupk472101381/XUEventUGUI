using UnityEngine;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(Scrollbar))]
    public class XUScrollbarListener : XUEventListener<XUScrollbarListenerData>
    {
        Scrollbar scrollbar = null;

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
            scrollbar = GetComponent<Scrollbar>();
            if (scrollbar != null)
            {
                scrollbar.onValueChanged.AddListener(OnScrollbarChange);
            }
        }

        private void OnScrollbarChange(float value)
        {
            if (scrollbar != null && mData != null && mData.eventChange.onEvent != null)
            {
                mData.eventChange.onEvent(XUEventType.ScrollbarValueChange, mData.eventChange.strEvent, mData.eventChange.objParam, scrollbar, value);
            }
        }
    }
}