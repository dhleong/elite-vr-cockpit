using System.Collections.Generic;
using UnityEngine;

namespace EVRC
{
    using Direction = ActionsController.Direction;
    using ActionChange = ActionsController.ActionChange;
    using DirectionActionChange = ActionsController.DirectionActionChange;
    using ActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.ActionChange>;
    using ActionChangeUnpressHandler = PressManager.UnpressHandlerDelegate<ActionsController.ActionChange>;
    using DirectionActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.DirectionActionChange>;
    using DirectionActionChangeUnpressHandler = PressManager.UnpressHandlerDelegate<ActionsController.DirectionActionChange>;

    public abstract class AbstractMenuState : ActionsControllerState
    {
        /// <summary>
        /// How long can the menu button be pressed before not being considered a back button press. Should sync up with the SeatedPositionResetAction hold time to ensure a position resest is not considered a back button press.
        /// </summary>
        public float menuButtonReleaseTimeout = 1f;

        protected Dictionary<Direction, DirectionActionChangePressHandler> directionNavigators;

        public AbstractMenuState()
        {
            directionNavigators = CreateDirectionNavigators();
        }

        public override void ConfigurePressManager(ActionsControllerPressManager manager)
        {
            base.ConfigurePressManager(manager);

            manager
                .MenuBack(OnBack)
                .MenuSelect(CreateOnSelectDelegate())
                .MenuNavigate(OnNavigateDirection);
        }

        protected ActionChangeUnpressHandler OnBack(ActionChange pEv)
        {
            float menuPressTime = Time.time;

            return (uEv) =>
            {
                if (Time.time - menuPressTime < menuButtonReleaseTimeout)
                {
                    Back();
                }
            };
        }

        protected DirectionActionChangeUnpressHandler OnNavigateDirection(DirectionActionChange pEv)
        {
            if (directionNavigators.ContainsKey(pEv.direction))
            {
                return directionNavigators[pEv.direction](pEv);
            }

            return (uEv) => { };
        }

        abstract protected void Back();
        abstract protected ActionChangePressHandler CreateOnSelectDelegate();
        abstract protected Dictionary<Direction, DirectionActionChangePressHandler> CreateDirectionNavigators();
    }
}


