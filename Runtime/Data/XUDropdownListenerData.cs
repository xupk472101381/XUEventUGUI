namespace XUEventUGUI.Data
{
    public class XUDropdownListenerData : XUEventListenerData
    {
        public XUEventDataEx eventChange = new XUEventDataEx();

        public override void Reset()
        {
            eventChange.Reset();
            base.Reset();
        }
    }
}