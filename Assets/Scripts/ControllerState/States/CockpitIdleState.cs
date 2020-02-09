namespace EVRC
{
    public class CockpitIdleState : ActionsControllerState
    {
        private IControllerState[] myDelegates = new []{ new MenuState(), };
        protected override IControllerState[] delegates
        {
            get { return myDelegates; }
        }
    }
}
