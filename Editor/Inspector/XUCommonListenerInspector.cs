using UnityEditor;
using XUEventUGUI.Data;

namespace XUEventUGUI.Base
{
    [CanEditMultipleObjects, CustomEditor(typeof(XUCommonListener))]
    public class XUCommonListenerInspector : XUEventListenerInspector
    {
        public override void OnInspectorGUI()
        {
            InspectorGUI<XUCommonListener, XUCommonListenerData>();
        }
    }
}