using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is responsible for controlling the camera of the Pink Guard
/// </summary>
public class SG_PinkGuardCameraController : MonoBehaviour
{
    [Header("Generic Properties")]
    [SerializeField] private Vector3 initialCameraOffset;

    [Header("Non-ADS Properties")]
    [SerializeField] private float cameraNonADSensitivity;
    [SerializeField] private float cameraNonADSFOV;

    [Header("ADS Properties")]
    [SerializeField] private float cameraADSensitivity;
    [SerializeField] private float cameraADSFOV;

    [Header("Required Compoenents")]
    [SerializeField] private Transform cameraParentTransform;
    [SerializeField] private Camera guardCamera;
    [SerializeField] private GameObject scoppedIndication;
    [SerializeField] private GameObject unScoppedIndication;

    private void Start()
    {
        SetInitialLocalPosition();
    }

    private void Update()
    {
        RotateCameraParentObject();
        SetADSAndNonADSCameraChanges();
    }

    private void SetInitialLocalPosition()
    {
        guardCamera.transform.localPosition = initialCameraOffset;
    }

    //Rotates the parent object of the camera to look around.
    private void RotateCameraParentObject()
    {
        float sensitivity = SG_InputController.IsAiming ? cameraADSensitivity : cameraNonADSensitivity;

        float prevYRotation = cameraParentTransform.rotation.eulerAngles.y;
        float finalYRotation = (SG_InputController.LookInput.x * Time.deltaTime * sensitivity) + prevYRotation;

        float prevXRotation = cameraParentTransform.rotation.eulerAngles.x;
        float finalXRotation = (-SG_InputController.LookInput.y * Time.deltaTime * sensitivity) + prevXRotation;

        cameraParentTransform.transform.rotation = Quaternion.Euler(finalXRotation, finalYRotation, 0);
    }

    //When user does ADS/NON-ADS, this function will change the camera's field of view and position
    private void SetADSAndNonADSCameraChanges()
    {
        if (SG_InputController.IsAiming)
        {
            guardCamera.transform.localPosition = Vector3.Lerp(guardCamera.transform.localPosition, Vector3.zero, Time.deltaTime * 10f);
            guardCamera.fieldOfView = Mathf.Lerp(guardCamera.fieldOfView, cameraADSFOV, Time.deltaTime * 10f);

            scoppedIndication.SetActive(true);
            unScoppedIndication.SetActive(false);
        }
        else
        {
            guardCamera.transform.localPosition = Vector3.Lerp(guardCamera.transform.localPosition, initialCameraOffset, Time.deltaTime * 10f);
            guardCamera.fieldOfView = Mathf.Lerp(guardCamera.fieldOfView, cameraNonADSFOV, Time.deltaTime * 10f);

            unScoppedIndication.SetActive(true);
            scoppedIndication.SetActive(false);
        }
    }
}
