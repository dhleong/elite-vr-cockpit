namespace EVRC
{
    using ActionChange = ActionsController.ActionChange;
    using DirectionActionChange = ActionsController.DirectionActionChange;
    using EDControlButton = EDControlBindings.EDControlButton;
    using ActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.ActionChange>;
    using DirectionActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.DirectionActionChange>;
    using static KeyboardInterface;

    public static class DelegateFactory
    {
        public static DirectionActionChangePressHandler DirectionControlPress(EDControlButton button)
        {
            return (DirectionActionChange pEv) =>
            {
                var unpress = CallbackPress(EDControlBindings.GetControlButton(button));
                return (uEv) => unpress();
            };
        }

        public static DirectionActionChangePressHandler DirectionKeyPress(string keyName)
        {
            var key = Key(keyName);
            return (DirectionActionChange pEv) =>
            {
                var unpress = CallbackPress(key);
                return (uEv) => unpress();
            };
        }

        public static ActionChangePressHandler KeyPress(IKeyPress key)
        {
            return (ActionChange pEv) =>
            {
                var unpress = CallbackPress(key);
                return (uEv) => unpress();
            };
        }

        public static ActionChangePressHandler ControlPress(EDControlButton button)
        {
            return (ActionChange pEv) =>
            {
                var unpress = CallbackPress(EDControlBindings.GetControlButton(button));
                return (uEv) => unpress();
            };
        }
    }
}
