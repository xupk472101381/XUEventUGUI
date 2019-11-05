using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XUEventUGUI.Base;

namespace XUEventUGUI
{
    /// <summary>
    /// UI扩展事件
    /// </summary>
    public enum XUEventType
    {
        /// <summary>
        /// 点击按钮
        /// </summary>
        ButtonClick,
        /// <summary>
        /// Toggle状态改变
        /// </summary>
        ToggleValueChange,
        /// <summary>
        /// Slider进度值改变
        /// </summary>
        SliderValueChange,
        /// <summary>
        /// Scrollbar进度值改变
        /// </summary>
        ScrollbarValueChange,
        /// <summary>
        /// Dropdown切换选项
        /// </summary>
        DropdownValueChange,
        /// <summary>
        /// InputField的值改变
        /// </summary>
        InputFieldValueChange,
        /// <summary>
        /// InputField提交事件
        /// </summary>
        InputFieldSubmit,
    }

    /// <summary>
    /// 常规事件代理
    /// </summary>
    /// <param name="triggerType">事件类型</param>
    /// <param name="strEvent">事件消息</param>
    /// <param name="objParam">事件参数</param>
    /// <param name="sender">事件触发者</param>
    /// <param name="eventData">事件数据</param>
    public delegate void EventDelegate(EventTriggerType triggerType, string strEvent, object objParam, GameObject sender, BaseEventData eventData);

    /// <summary>
    /// 扩展事件代理
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="strEvent">事件消息</param>
    /// <param name="objParam">事件参数</param>
    /// <param name="sender">事件触发者</param>
    /// <param name="senderParam">
    /// 事件触发者发送的扩展参数
    /// Button 是 null
    /// Toggle 是 bool，表示开关
    /// Slider 是 float，表示进度值
    /// Scrollbar 是 float，表示进度值
    /// Dropdown 是 int，表示当前选项索引
    /// InputField 是 string，表示当前文本值
    /// </param>
    public delegate void EventDelegateXU(XUEventType eventType, string strEvent, object objParam, UIBehaviour sender, object senderParam);

    /// <summary>
    /// UGUI事件的扩展工具
    /// 使用者首先挂载 eventDelegate 和 eventDelegateXU 两个回调，用于接收事件
    /// 注：eventDelegate 是响应 EventTriggerType 类型的常规事件
    /// 注：eventDelegateXU 是响应 XUEventType 类型的扩展事件
    /// 使用者可通过 InsertEvent 和类似的其他方法来挂载监听器
    /// 例如：
    ///     image.InsertEvent()
    ///     button.InsertEventClick()
    ///     toggle.InsertEventChange()
    ///     等等...
    /// </summary>
    public static class XUEventHandler
    {
        /// <summary>
        /// 监听EventTriggerType类型的常规事件
        /// </summary>
        public static EventDelegate eventDelegate;
        /// <summary>
        /// 监听XUEventType类型的扩展事件
        /// </summary>
        public static EventDelegateXU eventDelegateXU;


        //================================== Common相关事件 ======================================

        public static void InsertEvent(this UIBehaviour target, EventTriggerType triggerType, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because UIBehaviour is null.");
                return;
            }
            InsertEvent(target.gameObject, triggerType, strEvent, objParam);
        }
        public static void InsertEvent(this GameObject target, EventTriggerType triggerType, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because GameObject is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase != null)
            {
                listenerBase.SetEvent(triggerType, strEvent, objParam, eventDelegate);
            }
            else
            {
                XUCommonListener listenerCommon = target.AddComponent<XUCommonListener>();
                listenerCommon.SetEvent(triggerType, strEvent, objParam, eventDelegate);
            }
        }

        public static void DeleteEvent(this UIBehaviour target, EventTriggerType triggerType)
        {
            if (target != null)
            {
                DeleteEvent(target.gameObject, triggerType);
            }
        }
        public static void DeleteEvent(this GameObject target, EventTriggerType triggerType)
        {
            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase != null)
            {
                listenerBase.RemoveEvent(triggerType);
            }
        }

        //================================== Button相关事件 ======================================

        public static void InsertEventClick(this Button target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because Button is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUButtonListener>().SetEventClick(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUButtonListener)
                {
                    (listenerBase as XUButtonListener).SetEventClick(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUButtonListener listener = target.gameObject.AddComponent<XUButtonListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventClick(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventClick(this Button target)
        {
            if (target != null)
            {
                XUButtonListener listener = target.GetComponent<XUButtonListener>();
                if (listener != null)
                {
                    listener.RemoveEventClick();
                }
            }
        }

        //================================== Toggle相关事件 ======================================

        public static void InsertEventChange(this Toggle target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because Toggle is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUToggleListener>().SetEventChange(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUToggleListener)
                {
                    (listenerBase as XUToggleListener).SetEventChange(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUToggleListener listener = target.gameObject.AddComponent<XUToggleListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventChange(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventChange(this Toggle target)
        {
            if (target != null)
            {
                XUToggleListener listener = target.GetComponent<XUToggleListener>();
                if (listener != null)
                {
                    listener.RemoveEventChange();
                }
            }
        }

        //================================== Slider相关事件 ======================================

        public static void InsertEventChange(this Slider target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because Slider is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUSliderListener>().SetEventChange(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUSliderListener)
                {
                    (listenerBase as XUSliderListener).SetEventChange(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUSliderListener listener = target.gameObject.AddComponent<XUSliderListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventChange(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventChange(this Slider target)
        {
            if (target != null)
            {
                XUSliderListener listener = target.GetComponent<XUSliderListener>();
                if (listener != null)
                {
                    listener.RemoveEventChange();
                }
            }
        }

        //================================== Scrollbar相关事件 ======================================

        public static void InsertEventChange(this Scrollbar target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because Scrollbar is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUScrollbarListener>().SetEventChange(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUScrollbarListener)
                {
                    (listenerBase as XUScrollbarListener).SetEventChange(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUScrollbarListener listener = target.gameObject.AddComponent<XUScrollbarListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventChange(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventChange(this Scrollbar target)
        {
            if (target != null)
            {
                XUScrollbarListener listener = target.GetComponent<XUScrollbarListener>();
                if (listener != null)
                {
                    listener.RemoveEventChange();
                }
            }
        }


        //================================== Dropdown相关事件 ======================================

        public static void InsertEventChange(this Dropdown target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because Dropdown is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUDropdownListener>().SetEventChange(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUDropdownListener)
                {
                    (listenerBase as XUDropdownListener).SetEventChange(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUDropdownListener listener = target.gameObject.AddComponent<XUDropdownListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventChange(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventChange(this Dropdown target)
        {
            if (target != null)
            {
                XUDropdownListener listener = target.GetComponent<XUDropdownListener>();
                if (listener != null)
                {
                    listener.RemoveEventChange();
                }
            }
        }

        //================================== InputField相关事件 ======================================

        public static void InsertEventChange(this InputField target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because InputField is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUInputFieldListener>().SetEventChange(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUInputFieldListener)
                {
                    (listenerBase as XUInputFieldListener).SetEventChange(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUInputFieldListener listener = target.gameObject.AddComponent<XUInputFieldListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventChange(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void InsertEventSubmit(this InputField target, string strEvent, object objParam)
        {
            if (target == null)
            {
                Debug.LogError("insert event fail,because InputField is null.");
                return;
            }
            if (string.IsNullOrEmpty(strEvent))
            {
                Debug.LogError("insert event fail,because string event is null.");
            }

            XUEventListenerBase listenerBase = target.GetComponent<XUEventListenerBase>();
            if (listenerBase == null)
            {
                target.gameObject.AddComponent<XUInputFieldListener>().SetEventSubmit(strEvent, objParam, eventDelegateXU);
            }
            else
            {
                if (listenerBase is XUInputFieldListener)
                {
                    (listenerBase as XUInputFieldListener).SetEventSubmit(strEvent, objParam, eventDelegateXU);
                }
                else
                {
                    Dictionary<EventTriggerType, Dictionary<string, object>> eventMap = listenerBase.GetEventMap();
                    Object.DestroyImmediate(listenerBase);

                    XUInputFieldListener listener = target.gameObject.AddComponent<XUInputFieldListener>();
                    foreach (var keyValue in eventMap)
                    {
                        foreach (var keyValue1 in keyValue.Value)
                        {
                            listener.SetEvent(keyValue.Key, keyValue1.Key, keyValue1.Value, eventDelegate);
                        }
                    }
                    listener.SetEventSubmit(strEvent, objParam, eventDelegateXU);
                }
            }
        }

        public static void DeleteEventChange(this InputField target)
        {
            if (target != null)
            {
                XUInputFieldListener listener = target.GetComponent<XUInputFieldListener>();
                if (listener != null)
                {
                    listener.RemoveEventChange();
                }
            }
        }

        public static void DeleteEventSubmit(this InputField target)
        {
            if (target != null)
            {
                XUInputFieldListener listener = target.GetComponent<XUInputFieldListener>();
                if (listener != null)
                {
                    listener.RemoveEventSubmit();
                }
            }
        }
    }
}