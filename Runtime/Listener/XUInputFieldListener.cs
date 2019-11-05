using UnityEngine;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(InputField))]
    public class XUInputFieldListener : XUEventListener<XUInputFieldListenerData>
    {
        InputField inputField = null;

        public void SetEventChange(string strEvent, object objParam, EventDelegateXU onEvent)
        {
            mData.eventChange.Set(strEvent, objParam, onEvent);
        }

        public void SetEventChange(string strEvent, object objParam)
        {
            mData.eventChange.Set(strEvent, objParam);
        }

        public void SetEventSubmit(string strEvent, object objParam, EventDelegateXU onEvent)
        {
            mData.eventSubmit.Set(strEvent, objParam, onEvent);
        }

        public void SetEventSubmit(string strEvent, object objParam)
        {
            mData.eventSubmit.Set(strEvent, objParam);
        }

        public void RemoveEventChange()
        {
            mData.eventChange.Reset();
        }

        public void RemoveEventSubmit()
        {
            mData.eventSubmit.Reset();
        }

        public void GetEventChange(out string strEvent, out object objParam)
        {
            strEvent = mData.eventChange.strEvent;
            objParam = mData.eventChange.objParam;
        }

        public void GetEventSubmit(out string strEvent, out object objParam)
        {
            strEvent = mData.eventSubmit.strEvent;
            objParam = mData.eventSubmit.objParam;
        }

        protected override void Start()
        {
            inputField = GetComponent<InputField>();
            if (inputField != null)
            {
                inputField.onValueChanged.AddListener(OnInputFieldChange);
                inputField.onEndEdit.AddListener(OnInputFieldSubmit);
            }
        }

        private void OnInputFieldChange(string text)
        {
            if (inputField != null && mData != null && mData.eventChange.onEvent != null)
            {
                mData.eventChange.onEvent(XUEventType.InputFieldValueChange, mData.eventChange.strEvent, mData.eventChange.objParam, inputField, text);
            }
        }

        private void OnInputFieldSubmit(string text)
        {
            if (inputField != null && mData != null && mData.eventSubmit.onEvent != null)
            {
                mData.eventSubmit.onEvent(XUEventType.InputFieldSubmit, mData.eventSubmit.strEvent, mData.eventSubmit.objParam, inputField, text);
            }
        }
    }
}