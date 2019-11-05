using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XUEventUGUI.Data
{
    public abstract class XUEventDataBase
    {
        /// <summary>
        /// 触发事件
        /// </summary>
        public string strEvent;
        /// <summary>
        /// 事件参数
        /// </summary>
        public object objParam;

        public virtual void Set(string strEvent, object objParam)
        {
            this.strEvent = strEvent;
            this.objParam = objParam;
        }

        public virtual void Reset()
        {
            strEvent = null;
            objParam = null;
        }
    }

    public class XUEventData : XUEventDataBase
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        public EventDelegate onEvent;

        public void Set(string strEvent, object objParam, EventDelegate onEvent)
        {
            base.Set(strEvent, objParam);
            this.onEvent = onEvent;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            onEvent = null;
        }
    }

    public class XUEventDataEx : XUEventDataBase
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        public EventDelegateXU onEvent;

        public void Set(string strEvent, object objParam, EventDelegateXU onEvent)
        {
            base.Set(strEvent, objParam);
            this.onEvent = onEvent;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            onEvent = null;
        }
    }

    /// <summary>
    /// 监听器需要的数据基类
    /// </summary>
    public class XUEventListenerData
    {
        /// <summary>
        /// 监听器字典
        /// </summary>
        private Dictionary<EventTriggerType, XUEventData> mEventDataMap = new Dictionary<EventTriggerType, XUEventData>();

        public virtual bool IsHave(EventTriggerType triggerType)
        {
            return mEventDataMap.ContainsKey(triggerType);
        }

        public virtual XUEventData GetEventData(EventTriggerType triggerType)
        {
            return IsHave(triggerType) ? mEventDataMap[triggerType] : null;
        }

        public virtual void SetEventData(EventTriggerType triggerType, string strEvent, object objParam, EventDelegate onEvent)
        {
            XUEventData eventData = null;
            if (IsHave(triggerType))
            {
                eventData = GetEventData(triggerType);
            }
            else
            {
                eventData = new XUEventData();
            }
            eventData.Set(strEvent, objParam, onEvent ?? eventData.onEvent);
            if (IsHave(triggerType))
            {
                mEventDataMap[triggerType] = eventData;
            }
            else
            {
                mEventDataMap.Add(triggerType, eventData);
            }
        }

        public virtual void HandleEvent(GameObject sender, EventTriggerType triggerType, BaseEventData triggerEventData)
        {
            XUEventData eventData = GetEventData(triggerType);
            if (eventData != null && eventData.onEvent != null)
            {
                eventData.onEvent(triggerType, eventData.strEvent, eventData.objParam, sender, triggerEventData);
            }
        }

        public virtual void RemoveEventData(EventTriggerType triggerType)
        {
            if (IsHave(triggerType))
            {
                XUEventData eventData = GetEventData(triggerType);
                if (eventData != null)
                {
                    eventData.Reset();
                }
                mEventDataMap.Remove(triggerType);
            }
        }

        public virtual void GetEventData(EventTriggerType triggerType, out string strEvent, out object objParam)
        {
            if (IsHave(triggerType))
            {
                XUEventData eventData = GetEventData(triggerType);
                strEvent = eventData.strEvent;
                objParam = eventData.objParam;
            }
            else
            {
                strEvent = string.Empty;
                objParam = null;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
            foreach (var kv in mEventDataMap)
            {
                kv.Value.Reset();
            }
            mEventDataMap.Clear();
        }
    }
}