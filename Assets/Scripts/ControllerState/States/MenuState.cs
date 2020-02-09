using System.Collections.Generic;

namespace EVRC
{
    using Direction = ActionsController.Direction;
    using EDControlButton = EDControlBindings.EDControlButton;
    using ActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.ActionChange>;
    using DirectionActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.DirectionActionChange>;
    using static DelegateFactory;
    using static KeyboardInterface;

    public class MenuState : AbstractMenuState
    {

        public override void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            base.ConfigurePressManager(manager);

            manager.MenuNestedToggle(ControlPress(EDControlButton.UI_Toggle));
        }

        protected override void Back()
        {
            SendEscape();
        }

        protected override ActionChangePressHandler CreateOnSelectDelegate()
        {
            return KeyPress(Space());
        }

        protected override Dictionary<Direction, DirectionActionChangePressHandler> CreateDirectionNavigators()
        {
            return new Dictionary<Direction, DirectionActionChangePressHandler>()
            {
                { Direction.Up, DirectionKeyPress("Key_UpArrow") },
                { Direction.Right, DirectionKeyPress("Key_RightArrow") },
                { Direction.Down, DirectionKeyPress("Key_DownArrow") },
                { Direction.Left, DirectionKeyPress("Key_LeftArrow") },
            };
        }
    }
}

