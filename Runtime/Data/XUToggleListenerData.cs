namespace XUEventUGUI.Data
{
    public class XUToggleListenerData : XUEventListenerData
    {
        public XUEventDataEx eventChange = new XUEventDataEx();

        public override void Reset()
        {
            eventChange.Reset();
            base.Reset();
        }
    }
}