using System.Collections.Generic;
using System.Linq;

namespace EVRC
{
    using Direction = ActionsController.Direction;
    using HatDirection = vJoyInterface.HatDirection;
    using ActionChange = ActionsController.ActionChange;
    using OutputAction = ActionsController.OutputAction;
    using DirectionActionChange = ActionsController.DirectionActionChange;
    using ActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.ActionChange>;
    using DirectionActionChangePressHandler = PressManager.PressHandlerDelegate<ActionsController.DirectionActionChange>;

    public class VirtualController : IControllerState
    {

        public vJoyInterface output;

        protected HashSet<uint> pressedButtons = new HashSet<uint>();
        protected HashSet<uint> pressedHatDirections = new HashSet<uint>();

        public void OnEnable()
        {
            // nop
        }

        public void OnDisable()
        {
            ReleaseAllInputs();
        }

        public ActionChangePressHandler CreateButtonDelegateFromMap(Dictionary<OutputAction, uint> map)
        {
            return (ActionChange pEv) =>
            {
                if (map.ContainsKey(pEv.action))
                {
                    uint btnIndex = map[pEv.action];
                    PressButton(btnIndex);

                    return (uEv) => { UnpressButton(btnIndex); };
                }

                return (uEv) => { };
            };
        }

        public DirectionActionChangePressHandler CreateHatDelegateFromMaps(
                Dictionary<OutputAction, uint> hats,
                Dictionary<Direction, HatDirection> directions)
        {
            return (DirectionActionChange pEv) =>
            {
                if (hats.ContainsKey(pEv.action))
                {
                    uint hatNumber = hats[pEv.action];
                    SetHatDirection(hatNumber, directions[pEv.direction]);

                    return (uEv) => { ReleaseHatDirection(hatNumber); };
                }

                return (uEv) => { };
            };
        }

        public void PressButton(uint btnNumber)
        {
            if (output)
            {
                pressedButtons.Add(btnNumber);
                output.SetButton(btnNumber, true);
            }
        }

        public void UnpressButton(uint btnNumber)
        {
            if (output && pressedButtons.Contains(btnNumber))
            {
                output.SetButton(btnNumber, false);
                pressedButtons.Remove(btnNumber);
            }
        }

        protected void SetHatDirection(uint hatNumber, HatDirection hatDirection)
        {
            if (output)
            {
                pressedHatDirections.Add(hatNumber);
                output.SetHatDirection(hatNumber, hatDirection);
            }
        }

        protected void ReleaseHatDirection(uint hatNumber)
        {
            if (output && pressedHatDirections.Contains(hatNumber))
            {
                output.SetHatDirection(hatNumber, HatDirection.Neutral);
                pressedHatDirections.Remove(hatNumber);
            }
        }

        protected void ReleaseAllInputs()
        {
            uint[] unpressButtons = pressedButtons.ToArray();
            foreach (uint btnNumber in unpressButtons)
            {
                UnpressButton(btnNumber);
            }

            uint[] unpressHats = pressedHatDirections.ToArray();
            foreach (uint hatNumber in unpressHats)
            {
                ReleaseHatDirection(hatNumber);
            }
        }
    }
}
