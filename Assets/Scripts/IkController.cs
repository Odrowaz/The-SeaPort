using UnityEngine;

public class IkController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] bool ikActive = true;
    [SerializeField] Transform rightHandObj = null;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = Vector3.zero;
    
    void Start (){
        _animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(_animator) {
       
            if(ikActive) {
                
                if (rightHandObj != null)
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1); //Position
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1); //Rotation

                    _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.TransformPoint(positionOffset));
                    _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation * Quaternion.Euler(rotationOffset));
                }
            }
            else {          
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
            }
        }
    }
}
