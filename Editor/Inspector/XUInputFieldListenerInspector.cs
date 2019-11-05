using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUInputFieldListener))]
    public class XUInputFieldListenerInspector : XUEventListenerInspector
    {
        private bool foldoutChange = true;
        private bool foldoutSubmit = true;

        public override void OnInspectorGUI()
        {
            InspectorGUI<XUInputFieldListener, XUInputFieldListenerData>();

            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            XUInputFieldListener listener = target as XUInputFieldListener;

            foldoutChange = EditorGUILayout.Foldout(foldoutChange, "事件类型:" + XUEventType.InputFieldValueChange);
            if (foldoutChange)
            {
                string strEvent = "";
                object objParam = null;
                listener.GetEventChange(out strEvent, out objParam);

                string strEventNew = EditorGUILayout.TextField("事件名称:", strEvent);
                object objParamNew = DrawObject("事件参数", objParam);
                if (strEventNew != strEvent || objParamNew != objParam)
                {
                    listener.SetEventChange(strEventNew, objParamNew);
                }
            }

            foldoutSubmit = EditorGUILayout.Foldout(foldoutSubmit, "事件类型:" + XUEventType.InputFieldSubmit);
            if (foldoutSubmit)
            {
                string strEvent = "";
                object objParam = null;
                listener.GetEventSubmit(out strEvent, out objParam);

                string strEventNew = EditorGUILayout.TextField("事件名称:", strEvent);
                object objParamNew = DrawObject("事件参数", objParam);
                if (strEventNew != strEvent || objParamNew != objParam)
                {
                    listener.SetEventSubmit(strEventNew, objParamNew);
                }
            }

            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
