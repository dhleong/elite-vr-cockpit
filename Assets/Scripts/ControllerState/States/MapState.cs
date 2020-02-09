using System.Collections.Generic;

namespace EVRC
{
    using Direction = ActionsController.Direction;
    using EDControlButton = EDControlBindings.EDControlButton;
    using ActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.ActionChange>;
    using DirectionActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.DirectionActionChange>;
    using static DelegateFactory;
    using static KeyboardInterface;

    public class MapState : AbstractMenuState
    {

        public override void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            base.ConfigurePressManager(manager);

            manager
                .UITabPrevious(ControlPress(EDControlButton.CyclePreviousPanel))
                .UITabNext(ControlPress(EDControlButton.CycleNextPanel));
        }

        protected override void Back()
        {
            var bindings = EDStateManager.instance.controlBindings;
            if (bindings != null && bindings.HasKeyboardKeybinding(EDControlButton.GalaxyMapOpen))
            {
                // On the Galaxy map this will exit
                // On the System map/orrery this will go to the galaxy map, from where you can exit
                EDControlBindings.GetControlButton(EDControlButton.GalaxyMapOpen)?.Send();
            }
            else
            {
                SendEscape();
            }
        }

        protected override ActionChangePressHandler CreateOnSelectDelegate()
        {
            return ControlPress(EDControlButton.UI_Select);
        }

        protected override Dictionary<Direction, DirectionActionChangePressHandler> CreateDirectionNavigators()
        {
            return new Dictionary<Direction, DirectionActionChangePressHandler>()
            {
                { Direction.Up, DirectionControlPress(EDControlButton.UI_Up) },
                { Direction.Right, DirectionControlPress(EDControlButton.UI_Right) },
                { Direction.Down, DirectionControlPress(EDControlButton.UI_Down) },
                { Direction.Left, DirectionControlPress(EDControlButton.UI_Left) },
            };
        }
    }
}


