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
        private CockpitMode mode = CockpitMode.GameNotRunning;

        public void OnCockpitUIModeChanged(CockpitMode newMode)
        {
            mode = newMode;
            if (newMode.HasFlag(CockpitMode.Cockpit))
            {
                TransitionToState(new CockpitIdleState());
            }
            else if (newMode.HasFlag(CockpitMode.Map))
            {
                TransitionToState(new MapState());
            }
            else if (newMode.HasFlag(CockpitMode.MenuMode))
            {
                TransitionToState(new MenuState());
            }
        }

        public void OnGrabbing(GrabbableControl control)
        {
            grabbing = control;

            switch (grabbing)
            {
                case GrabbableControl.Joystick:
                    TransitionToState(new JoystickState());
                    break;

                case GrabbableControl.Throttle:
                    TransitionToState(new ThrottleState());
                    break;

                default:
                    // ?
                    break;
            }
        }

        public void OnReleased(GrabbableControl control)
        {
            grabbing = null;
            if (mode.HasFlag(CockpitMode.Cockpit))
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
