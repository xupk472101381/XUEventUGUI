using UnityEngine;
using UnityEngine.UI;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [RequireComponent(typeof(Slider))]
    public class XUSliderListener : XUEventListener<XUSliderListenerData>
    {
        Slider slider = null;

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
            slider = GetComponent<Slider>();
            if (slider != null)
            {
                slider.onValueChanged.AddListener(OnSliderChange);
            }
        }

        private void OnSliderChange(float value)
        {
            if (slider != null && mData != null && mData.eventChange.onEvent != null)
            {
                mData.eventChange.onEvent(XUEventType.SliderValueChange, mData.eventChange.strEvent, mData.eventChange.objParam, slider, value);
            }
        }
    }
}