using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace XUEventUGUI.Base
{
    [DisallowMultipleComponent]
    public class XUEventListenerBase : EventTrigger
    {
        public virtual void SetEvent(EventTriggerType triggerType, string strEvent, object objParam, EventDelegate onEvent)
        {

        }

        public virtual void SetEvent(EventTriggerType triggerType, string strEvent, object objParam)
        {

        }

        public virtual void RemoveEvent(EventTriggerType triggerType)
        {

        }

        public virtual void GetEvent(EventTriggerType triggerType, out string strEvent, out object objParam)
        {
            strEvent = string.Empty;
            objParam = null;
        }

        public virtual Dictionary<EventTriggerType, Dictionary<string, object>> GetEventMap()
        {
            return null;
        }
    }
}
