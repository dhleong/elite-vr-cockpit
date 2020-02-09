namespace EVRC
{
    public class ActionsControllerState : DelegatableControllerState, IControllerState
    {
        private ActionsControllerPressManager actionsPressManager;

        public override void OnEnable()
        {
            base.OnEnable();

            actionsPressManager = new ActionsControllerPressManager(null);
            ConfigurePressManager(actionsPressManager);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            actionsPressManager.Clear();
        }

        public virtual void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            var delegates = this.delegates;
            if (delegates == null) return;

            foreach (var d in delegates)
            {
                if (d is ActionsControllerState)
                {
                    (d as ActionsControllerState).ConfigurePressManager(manager);
                }
            }
        }

    }
}
