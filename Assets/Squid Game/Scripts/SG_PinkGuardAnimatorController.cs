using UnityEngine;

public class SG_PinkGuardAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.SetFloat("Turn", SG_InputController.LookInput.x);
    }
}
