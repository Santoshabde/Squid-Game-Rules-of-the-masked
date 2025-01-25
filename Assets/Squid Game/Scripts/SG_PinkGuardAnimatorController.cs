using UnityEngine;

public class SG_PinkGuardAnimatorController : MonoBehaviour
{
    [SerializeField] private bool enableTurnAnimation;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (enableTurnAnimation)
            animator.SetFloat("Turn", SG_InputController.LookInput.x);
    }
}
