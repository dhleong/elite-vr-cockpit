using System.Collections.Generic;

namespace EVRC
{
    using Direction = ActionsController.Direction;
    using HatDirection = vJoyInterface.HatDirection;
    using OutputAction = ActionsController.OutputAction;

    public class JoystickState : ActionsControllerState
    {
        // Map of abstracted action presses to vJoy joystick button numbers
        private static Dictionary<OutputAction, uint> joyBtnMap = new Dictionary<OutputAction, uint>()
        {
            { OutputAction.ButtonPrimary, 1 },
            { OutputAction.ButtonSecondary, 2 },
            { OutputAction.ButtonAlt, 3 },
            { OutputAction.POV1, 4 },
            { OutputAction.POV2, 5 },
        };

        private static Dictionary<OutputAction, uint> joyHatMap = new Dictionary<OutputAction, uint>()
        {
            { OutputAction.POV1, 1 },
            { OutputAction.POV2, 2 },
        };

        private static Dictionary<Direction, HatDirection> directionMap = new Dictionary<Direction, HatDirection>()
        {
            { Direction.Up, HatDirection.Up },
            { Direction.Right, HatDirection.Right },
            { Direction.Down, HatDirection.Down },
            { Direction.Left, HatDirection.Left },
        };

        // TODO acquire this somehow
        private VirtualController controller;

        public override void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            base.ConfigurePressManager(manager);

            var onAction = controller.CreateButtonDelegateFromMap(joyBtnMap);
            var onDirectionAction = controller.CreateHatDelegateFromMaps(joyBtnMap, directionMap);
            manager
                .ButtonPrimary(onAction)
                .ButtonSecondary(onAction)
                .ButtonAlt(onAction)
                .ButtonPOV1(onAction)
                .ButtonPOV2(onAction)
                .DirectionPOV1(onDirectionAction)
                .DirectionPOV2(onDirectionAction);
        }

    }
}



