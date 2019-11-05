using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUToggleListener))]
    public class XUToggleListenerInspector : XUEventListenerInspector
    {
        private bool foldout = true;

        public override void OnInspectorGUI()
        {
            InspectorGUI<XUToggleListener, XUToggleListenerData>();

            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            XUToggleListener listener = target as XUToggleListener;

            string strEvent = "";
            object objParam = null;
            listener.GetEventChange(out strEvent, out objParam);

            foldout = EditorGUILayout.Foldout(foldout, "事件类型:" + XUEventType.ToggleValueChange);
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
