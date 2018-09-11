using UnityEngine;

namespace EVRC
{
    /**
     * A grabbable virtual control that offers 6-axis control of the ship based on the controller's movement
     */
    public class Virtual6DOFController : MonoBehaviour
    {
        public vJoyInterface output;
        protected CockpitStateController controller;
        private bool highlighted = false;
        private ControllerInteractionPoint attachedInteractionPoint;
        private Transform zeroPoint;
        private Transform rotationPoint;
        private Transform translationPoint;

        void Start()
        {
            controller = CockpitStateController.instance;

            var zeroPointObject = new GameObject("[ZeroPoint]");
            zeroPoint = zeroPointObject.transform;
            zeroPoint.SetParent(transform);
            zeroPoint.localPosition = Vector3.zero;
            zeroPoint.localRotation = Quaternion.identity;

            var rotationPointObject = new GameObject("[RotationPoint]");
            rotationPoint = rotationPointObject.transform;
            rotationPoint.SetParent(zeroPoint);
            rotationPoint.localPosition = Vector3.zero;
            rotationPoint.localRotation = Quaternion.identity;

            var translationPointObject = new GameObject("[TranslationPoint]");
            translationPoint = translationPointObject.transform;
            translationPoint.SetParent(zeroPoint);
            translationPoint.localPosition = Vector3.zero;
            translationPoint.localRotation = Quaternion.identity;

            Refresh();
        }

        public bool Grabbed(ControllerInteractionPoint interactionPoint)
        {
            if (attachedInteractionPoint != null) return false;
            // Don't allow joystick use when editing is unlocked, so the movable surface can be used instead
            if (!controller.editLocked) return false;

            attachedInteractionPoint = interactionPoint;

            // @fixme We should probably separate the rotation and translation zero points
            // so translation can always be in the direction of movement
            // @fixme We should also find a way to switch controller rotation axis handling depending
            // on what angle the user grabbed the controller at (i.e. pointing forward vs upward)
            zeroPoint.rotation = attachedInteractionPoint.transform.rotation;
            zeroPoint.position = attachedInteractionPoint.transform.position;
            rotationPoint.rotation = attachedInteractionPoint.transform.rotation;
            translationPoint.position = attachedInteractionPoint.transform.position;

            return true;
        }

        public void Ungrabbed(ControllerInteractionPoint interactionPoint)
        {
            if (interactionPoint == attachedInteractionPoint)
            {
                attachedInteractionPoint = null;

                if (output)
                {
                    // output.SetStickAxis(StickAxis.Zero);
                }
            }
        }

        public void OnHover()
        {
            highlighted = true;
            Refresh();
        }

        public void OnUnhover()
        {
            highlighted = false;
            Refresh();
        }

        void Refresh()
        {
        }

        void Update()
        {
            if (attachedInteractionPoint == null) return;

            rotationPoint.rotation = attachedInteractionPoint.transform.rotation;

            // var axis = new StickAxis(rotationPoint.localEulerAngles);

            if (output)
            {
                // output.SetStickAxis(axis);
            }
        }
    }
}
