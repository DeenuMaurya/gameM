using UnityEngine;
using System.Collections;
using UnityEngine.VR;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    public static MovementController instance;
    public float speed = 3.0F;
    private CharacterController controllor;
    public static bool IsMoving = false;
    public static float curSpeed;
    private Transform vrHead;

    Vector3 forward;

    void Start()
    {
        instance = this;
        controllor = GetComponent<CharacterController>();
        vrHead = Camera.main.transform;
    }

    void Update()
    {
        if (VrSelector.instance.IsDaydream)
        {
            #if UNITY_EDITOR
            NormalController();
            #endif

            forward = vrHead.TransformDirection(Vector3.forward);
            Vector2 TouchAxis = GvrControllerInput.TouchPosCentered;

            if (!GvrController.IsTouching)
            {
                curSpeed = 0;
                return;
            }

            if (TouchAxis.magnitude > 0.3f)
            {
                if (Mathf.Abs(TouchAxis.x) <= Mathf.Abs(TouchAxis.y))
                {
                    curSpeed = speed * TouchAxis.y;
                    controllor.SimpleMove(forward * curSpeed);
                } 
            }


        }
        else if (VrSelector.instance.IsOculus)
        {
            #if UNITY_EDITOR
            NormalController();
            #endif

            forward = vrHead.TransformDirection(Vector3.forward);
            Vector2 TouchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

            if (!OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
            {
                curSpeed = 0;
                return;
            }

            if (TouchAxis.magnitude > 0.3f)
            {
                if (Mathf.Abs(TouchAxis.x) <= Mathf.Abs(TouchAxis.y))
                {
                    curSpeed = speed * TouchAxis.y;
                    controllor.SimpleMove(forward * curSpeed);
                } 
            }
        }
        else if (VrSelector.instance.IsCardboard)
        {
            NormalController();
        }

        if (curSpeed == 0)
            IsMoving = false;
        else
            IsMoving = true;
    }

    void NormalController()
    {
        forward = vrHead.TransformDirection(Vector3.forward);
        curSpeed = speed * Input.GetAxis("Vertical");
        controllor.SimpleMove(forward * curSpeed);
    }

}