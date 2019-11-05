namespace XUEventUGUI.Data
{
    public class XUScrollbarListenerData : XUEventListenerData
    {
        public XUEventDataEx eventChange = new XUEventDataEx();

        public override void Reset()
        {
            eventChange.Reset();
            base.Reset();
        }
    }
}