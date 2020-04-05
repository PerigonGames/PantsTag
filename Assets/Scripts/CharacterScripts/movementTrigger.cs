using UnityEngine;

public class MovementTrigger : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] AnimationClip m_clip;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Play Clip
        }
    }
}
