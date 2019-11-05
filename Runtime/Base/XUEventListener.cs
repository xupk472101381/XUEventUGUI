using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    public class XUEventListener<TDataType> : XUEventListenerBase where TDataType : XUEventListenerData, new()
    {
        protected TDataType mData = null;

        protected virtual void Awake()
        {
            mData = new TDataType();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void OnDestroy()
        {
            mData.Reset();
            mData = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void SetEvent(EventTriggerType triggerType, string strEvent, object objParam, EventDelegate onEvent)
        {
            if (mData != null)
            {
                mData.SetEventData(triggerType, strEvent, objParam, onEvent);
            }
        }

        public override void SetEvent(EventTriggerType triggerType, string strEvent, object objParam)
        {
            if (mData != null)
            {
                mData.SetEventData(triggerType, strEvent, objParam, null);
            }
        }

        public override void RemoveEvent(EventTriggerType triggerType)
        {
            if (mData != null)
            {
                mData.RemoveEventData(triggerType);
            }
        }

        public override void GetEvent(EventTriggerType triggerType, out string strEvent, out object objParam)
        {
            mData.GetEventData(triggerType, out strEvent, out objParam);
        }

        public override Dictionary<EventTriggerType, Dictionary<string, object>> GetEventMap()
        {
            Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = new Dictionary<EventTriggerType, Dictionary<string, object>>();
            foreach (EventTriggerType triggerType in Enum.GetValues(typeof(EventTriggerType)))
            {
                string strEvent;
                object objParam;
                GetEvent(triggerType, out strEvent, out objParam);
                if (string.IsNullOrEmpty(strEvent) == false)
                {
                    eventMap.Add(triggerType, new Dictionary<string, object>());
                    eventMap[triggerType].Add(strEvent, objParam);
                }
            }
            return eventMap;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.PointerEnter, eventData);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.PointerExit, eventData);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.PointerDown, eventData);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.PointerUp, eventData);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.PointerClick, eventData);
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Drag, eventData);
            }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Drop, eventData);
            }
        }

        public override void OnScroll(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Scroll, eventData);
            }
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.UpdateSelected, eventData);
            }
        }

        public override void OnSelect(BaseEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Select, eventData);
            }
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Deselect, eventData);
            }
        }

        public override void OnMove(AxisEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Move, eventData);
            }
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.InitializePotentialDrag, eventData);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.BeginDrag, eventData);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.EndDrag, eventData);
            }
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Submit, eventData);
            }
        }

        public override void OnCancel(BaseEventData eventData)
        {
            if (mData != null)
            {
                mData.HandleEvent(gameObject, EventTriggerType.Cancel, eventData);
            }
        }
    }
}