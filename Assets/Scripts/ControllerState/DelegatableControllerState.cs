namespace EVRC
{
    public abstract class DelegatableControllerState : IControllerState
    {
        protected virtual IControllerState[] delegates
        {
            get { return null; }
        }

        public virtual void OnEnable()
        {
            var delegates = this.delegates;
            if (delegates == null) return;

            foreach (var d in delegates)
            {
                d.OnEnable();
            }
        }

        public virtual void OnDisable()
        {
            var delegates = this.delegates;
            if (delegates == null) return;

            foreach (var d in delegates)
            {
                d.OnDisable();
            }
        }

    }
}
