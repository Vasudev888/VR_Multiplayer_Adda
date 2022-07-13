using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{
    //Avatar Transforms
    public Transform mainAvatarTransform;
    public Transform avatarHead;
    public Transform avatarBody;

    public Transform avatarHand_Left;
    public Transform avatarHand_Right;

    //XRRig Transforms
    public Transform xrHead;
    public Transform xrHand_Left;
    public Transform xrHand_Right;

    public Vector3 headPositionOffset;
    public Vector3 handRotationOffset;


    void Update()
    {
        FollowXRRig();
    }

    public void FollowXRRig()
    {
        //Head and Body sync
        mainAvatarTransform.position = Vector3.Lerp(mainAvatarTransform.position, xrHead.position + headPositionOffset, 0.5f);
        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, xrHead.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        //HandSync
        avatarHand_Right.position = Vector3.Lerp(avatarHand_Right.position, xrHand_Right.position, 0.5f);
        avatarHand_Right.rotation = Quaternion.Lerp(avatarHand_Right.rotation, xrHand_Right.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);

        avatarHand_Left.position = Vector3.Lerp(avatarHand_Left.position, xrHand_Left.position, 0.5f);
        avatarHand_Left.rotation = Quaternion.Lerp(avatarHand_Left.rotation, xrHand_Left.rotation, 0.5f) * Quaternion.Euler(handRotationOffset);
    }

   

}
