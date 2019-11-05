namespace XUEventUGUI.Data
{
    public class XUSliderListenerData : XUEventListenerData
    {
        public XUEventDataEx eventChange = new XUEventDataEx();

        public override void Reset()
        {
            eventChange.Reset();
            base.Reset();
        }
    }
}