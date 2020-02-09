using System.Collections.Generic;

namespace EVRC
{
    using OutputAction = ActionsController.OutputAction;

    public class ThrottleState : ActionsControllerState
    {
        // Map of abstracted action presses to vJoy joystick button numbers
        private static Dictionary<OutputAction, uint> joyBtnMap = new Dictionary<OutputAction, uint>()
        {
            { OutputAction.ButtonPrimary, 8 },
            { OutputAction.ButtonSecondary, 7 },
        };

        // TODO acquire this somehow
        private VirtualController controller;

        public override void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            base.ConfigurePressManager(manager);

            var onAction = controller.CreateButtonDelegateFromMap(joyBtnMap);
            manager
                .ButtonPrimary(onAction)
                .ButtonSecondary(onAction);
        }

    }
}




