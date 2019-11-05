namespace XUEventUGUI.Data
{
    public class XUInputFieldListenerData : XUEventListenerData
    {
        public XUEventDataEx eventChange = new XUEventDataEx();
        public XUEventDataEx eventSubmit = new XUEventDataEx();

        public override void Reset()
        {
            eventChange.Reset();
            eventSubmit.Reset();
            base.Reset();
        }
    }
}