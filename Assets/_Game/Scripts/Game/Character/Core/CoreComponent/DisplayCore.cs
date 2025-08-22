using UnityEngine;

namespace Core.Display
{
    public class DisplayCore : BaseCore
    {
        [SerializeField]
        Animator anim;
        [SerializeField]
        Transform skinTf;
        [SerializeField]
        Transform sensorTf;

        string currentAnim;

        public override void Initialize()
        {
        }

        public void SetSkinRotation(Quaternion rotation, bool isLocal)
        {
            if (isLocal)
            {
                skinTf.localRotation = rotation;
                sensorTf.localRotation = rotation;
            }
            else
            {
                skinTf.rotation = rotation;
                sensorTf.localRotation = rotation;
            }
        }

        public void ChangeAnim(string animName)
        {
            if (animName != currentAnim)
            {
                anim.ResetTrigger(currentAnim);
                currentAnim = animName;
                anim.SetTrigger(currentAnim);
            }
        }

        public void ResetAnim()
        {
            anim.ResetTrigger(currentAnim);
        }
    }
}
