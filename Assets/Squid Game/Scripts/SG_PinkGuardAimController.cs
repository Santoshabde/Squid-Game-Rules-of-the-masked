using UnityEngine;

public class SG_PinkGuardAimController : MonoBehaviour
{
[SerializeField] private Transform cameraParentTransform;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, cameraParentTransform.rotation.eulerAngles.y, 0);
    }
}
