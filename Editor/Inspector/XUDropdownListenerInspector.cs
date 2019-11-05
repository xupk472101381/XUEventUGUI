using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUDropdownListener))]
    public class XUDropdownListenerInspector : XUEventListenerInspector
    {
        private bool foldout = true;

        public override void OnInspectorGUI()
        {
            InspectorGUI<XUDropdownListener, XUDropdownListenerData>();

            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            XUDropdownListener listener = target as XUDropdownListener;

            string strEvent = "";
            object objParam = null;
            listener.GetEventChange(out strEvent, out objParam);

            foldout = EditorGUILayout.Foldout(foldout, "事件类型:" + XUEventType.DropdownValueChange);
            if (foldout)
            {
                string strEventNew = EditorGUILayout.TextField("事件名称:", strEvent);
                object objParamNew = DrawObject("事件参数", objParam);
                if (strEventNew != strEvent || objParamNew != objParam)
                {
                    listener.SetEventChange(strEventNew, objParamNew);
                }
            }

            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
