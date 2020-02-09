using UnityEngine;

namespace EVRC
{
    using CockpitMode = CockpitUIMode.CockpitMode;

    public enum GrabbableControl
    {
        Throttle,
        Joystick,
    }

    public class ControllerStateManager : MonoBehaviour
    {

        private IControllerState state;
        private GrabbableControl? grabbing;

        public void OnCockpitUIModeChanged(CockpitMode newMode)
        {
            if (newMode.HasFlag(CockpitMode.Cockpit))
            {
                if (grabbing == GrabbableControl.Joystick)
                {
                    TransitionToState(new JoystickState());
                }
                else
                {
                    TransitionToState(new CockpitIdleState());
                }
            }

            // TODO transition state
        }

        public void OnGrabbing(GrabbableControl control)
        {
            grabbing = control;
            if (state is CockpitIdleState)
            {
                if (grabbing == GrabbableControl.Joystick)
                {
                    TransitionToState(new JoystickState());
                }
            }

            // TODO transition state
        }

        public void OnReleased(GrabbableControl control)
        {
            grabbing = null;
            if (state is JoystickState)
            {
                TransitionToState(new CockpitIdleState());
            }
        }

        private void TransitionToState(IControllerState newState)
        {
            var oldState = state;
            if (oldState != null)
            {
                oldState.OnDisable();
            }

            state = newState;
            newState.OnEnable();
        }
    }
}
