using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUButtonListener))]
    public class XUButtonListenerInspector : XUEventListenerInspector
    {
        private bool foldout = true;

        public override void OnInspectorGUI()
        {
            InspectorGUI<XUButtonListener, XUButtonListenerData>();

            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            XUButtonListener listener = target as XUButtonListener;

            string strEvent = "";
            object objParam = null;
            listener.GetEventClick(out strEvent, out objParam);

            foldout = EditorGUILayout.Foldout(foldout, "事件类型:" + XUEventType.ButtonClick);
            if (foldout)
            {
                string strEventNew = EditorGUILayout.TextField("事件名称:", strEvent);
                object objParamNew = DrawObject("事件参数", objParam);
                if (strEventNew != strEvent || objParamNew != objParam)
                {
                    listener.SetEventClick(strEventNew, objParamNew);
                }
            }

            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
