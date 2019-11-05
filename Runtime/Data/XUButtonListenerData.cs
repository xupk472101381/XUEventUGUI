namespace XUEventUGUI.Data
{
    public class XUButtonListenerData : XUEventListenerData
    {
        public XUEventDataEx eventClick = new XUEventDataEx();

        public override void Reset()
        {
            eventClick.Reset();
            base.Reset();
        }
    }
}