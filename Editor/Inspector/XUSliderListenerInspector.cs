using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUSliderListener))]
    public class XUSliderListenerInspector : XUEventListenerInspector
    {
        private bool foldout = true;

        public override void OnInspectorGUI()
        {
            InspectorGUI<XUSliderListener, XUSliderListenerData>();

            // 更新显示
            this.serializedObject.Update();

            // 自定义绘制

            XUSliderListener listener = target as XUSliderListener;

            string strEvent = "";
            object objParam = null;
            listener.GetEventChange(out strEvent, out objParam);

            foldout = EditorGUILayout.Foldout(foldout, "事件类型:" + XUEventType.SliderValueChange);
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
