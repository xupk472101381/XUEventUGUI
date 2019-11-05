using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(Button))]
    public class XUButtonListener : XUEventListener<XUButtonListenerData>
    {
        Button button = null;

        public void SetEventClick(string strEvent, object objParam, EventDelegateXU onEvent)
        {
            mData.eventClick.Set(strEvent, objParam, onEvent);
        }

        public void SetEventClick(string strEvent, object objParam)
        {
            mData.eventClick.Set(strEvent, objParam);
        }

        public void RemoveEventClick()
        {
            mData.eventClick.Reset();
        }

        public void GetEventClick(out string strEvent, out object objParam)
        {
            strEvent = mData.eventClick.strEvent;
            objParam = mData.eventClick.objParam;
        }

        protected override void Start()
        {
            button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        private void OnButtonClick()
        {
            if (button != null && mData != null && mData.eventClick.onEvent != null)
            {
                mData.eventClick.onEvent(XUEventType.ButtonClick, mData.eventClick.strEvent, mData.eventClick.objParam, button, null);
            }
        }
    }
}