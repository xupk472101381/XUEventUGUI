using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using XUEventUGUI.Data;
using Object = UnityEngine.Object;

namespace XUEventUGUI.Base
{
    public class XUEventListenerInspector : Editor
    {
#if UNITY_EDITOR
        void Reset()
        {
            int tick = 0;
            EditorApplication.CallbackFunction callback = null;
            callback = () =>
            {
                if (tick > 0)
                {
                    EditorApplication.update -= callback;
                    DestroyImmediate(this, false);
                    var msg = GetType().Name + " : 此脚本不能编辑模式下绑定";
                    EditorUtility.DisplayDialog("错误", msg, "ok");
                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                    return;
                }
                tick++;
            };
            EditorApplication.update += callback;
        }
#endif

        protected object DrawObject(string label, object obj)
        {
            if (obj == null)
            {
                EditorGUILayout.LabelField(label + "（Unknown）");
            }
            else
            {
                EditorGUILayout.LabelField(label + "（" + obj.GetType() + "）");
            }

            if (obj is bool)
            {
                return EditorGUILayout.Toggle(label, (bool)obj);
            }
            else if (obj is sbyte)
            {
                bool result = sbyte.TryParse(EditorGUILayout.TextField(obj.ToString()), out sbyte outResult);
                return result ? outResult : obj;
            }
            else if (obj is byte)
            {
                bool result = byte.TryParse(EditorGUILayout.TextField(obj.ToString()), out byte outResult);
                return result ? outResult : obj;
            }
            else if (obj is short)
            {
                bool result = short.TryParse(EditorGUILayout.TextField(obj.ToString()), out short outResult);
                return result ? outResult : obj;
            }
            else if (obj is ushort)
            {
                bool result = ushort.TryParse(EditorGUILayout.TextField(obj.ToString()), out ushort outResult);
                return result ? outResult : obj;
            }
            else if (obj is int)
            {
                bool result = int.TryParse(EditorGUILayout.TextField(obj.ToString()), out int outResult);
                return result ? outResult : obj;
            }
            else if (obj is uint)
            {
                bool result = uint.TryParse(EditorGUILayout.TextField(obj.ToString()), out uint outResult);
                return result ? outResult : obj;
            }
            else if (obj is long)
            {
                bool result = long.TryParse(EditorGUILayout.TextField(obj.ToString()), out long outResult);
                return result ? outResult : obj;
            }
            else if (obj is ulong)
            {
                bool result = ulong.TryParse(EditorGUILayout.TextField(obj.ToString()), out ulong outResult);
                return result ? outResult : obj;
            }
            else if (obj is float)
            {
                bool result = float.TryParse(EditorGUILayout.TextField(obj.ToString()), out float outResult);
                return result ? outResult : obj;
            }
            else if (obj is double)
            {
                bool result = double.TryParse(EditorGUILayout.TextField(obj.ToString()), out double outResult);
                return result ? outResult : obj;
            }
            else if (obj is decimal)
            {
                bool result = decimal.TryParse(EditorGUILayout.TextField(obj.ToString()), out decimal outResult);
                return result ? outResult : obj;
            }
            else if (obj is char)
            {
                bool result = char.TryParse(EditorGUILayout.TextField(obj.ToString()), out char outResult);
                return result ? outResult : obj;
            }
            else if (obj is string)
            {
                return EditorGUILayout.TextField(obj.ToString());
            }
            else if (obj is System.Enum)
            {
                return EditorGUILayout.EnumPopup(obj as System.Enum);
            }
            else if (obj == null)
            {
                EditorGUILayout.TextField("当前参数为空");
                return obj;
            }
            else
            {
                EditorGUILayout.TextField("当前参数类型不支持显示");
                return obj;
            }
        }

        private Dictionary<EventTriggerType, bool> foldoutMap = new Dictionary<EventTriggerType, bool>();

        protected void InspectorGUI<TListener, TData>()
            where TData : XUEventListenerData, new()
            where TListener : XUEventListener<TData>
        {
            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            TListener listener = target as TListener;
            foreach (EventTriggerType triggerType in Enum.GetValues(typeof(EventTriggerType)))
            {
                string strEvent = "";
                object objParam = null;
                listener.GetEvent(triggerType, out strEvent, out objParam);
                if (string.IsNullOrEmpty(strEvent))
                {
                    continue;
                }

                if (foldoutMap.ContainsKey(triggerType) == false)
                {
                    foldoutMap.Add(triggerType, true);
                }
                foldoutMap[triggerType] = EditorGUILayout.Foldout(foldoutMap[triggerType], "事件类型:" + triggerType.ToString());
                if (foldoutMap[triggerType])
                {
                    string strEventNew = EditorGUILayout.TextField("事件名称:", strEvent);
                    object objParamNew = DrawObject("事件参数", objParam);
                    if (strEventNew != strEvent || objParamNew != objParam)
                    {
                        listener.SetEvent(triggerType, strEventNew, objParamNew);
                    }
                }
            }

            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}