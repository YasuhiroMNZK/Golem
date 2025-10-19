using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerCtrl : MonoBehaviour
{
    CharacterController charCtrl;
    Animator animCtrl;
    [SerializeField] float speed = 4;
    [SerializeField] float jumppow = 7;
    [SerializeField] float fallspd = 2.0f;

    [SerializeField] bool useCameraDir = true;
    [SerializeField] float movedirOffset = 0;

    [SerializeField] bool zEnable = true;
    [SerializeField] bool xEnable = true;
    [SerializeField] bool canJump = true;
    [SerializeField] bool FPSMove = false;

    [SerializeField] bool useAnimationRotate = false;

    Vector3 forwardVec;
    Vector3 rightVec;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        animCtrl = GetComponent<Animator>();

        var angles = new Vector3(0, movedirOffset, 0);
        forwardVec = Quaternion.Euler(angles) * Vector3.forward;
        rightVec = Quaternion.Euler(angles) * Vector3.right;
    }

    float fallpow = -2.0f;

    Transform groundobj = null;
    Transform beforeGroundObj = null;
    Vector3 beforePos;
    Vector3 floorOffset;





    // Update is called once per frame
    void Update()
    {

        var moveRate = 1.0f;
        if (Input.GetButton("Fire3"))
        {
            moveRate = 0.5f;
        }

        // input and calculate move direction
        float xaxis = Input.GetAxis("Horizontal") * moveRate;
        float yaxis = Input.GetAxis("Vertical") * moveRate;



        Vector3 cameraFwdVec = forwardVec;
        Vector3 cameraRightVec = rightVec;
        if (useCameraDir)
        {
            cameraFwdVec = Camera.main.transform.TransformDirection(forwardVec);
            cameraFwdVec.Scale(new Vector3(1, 0, 1));
            cameraFwdVec.Normalize();

            cameraRightVec = Camera.main.transform.TransformDirection(rightVec);
            cameraRightVec.Scale(new Vector3(1, 0, 1));
            cameraRightVec.Normalize();
        }

        var movementxaxis = xaxis;
        var movementyaxis = yaxis;

        if (!zEnable)
        {
            movementyaxis = 0;
        }
        if (!xEnable)
        {
            movementxaxis = 0;
        }
        Vector3 moveDir = cameraFwdVec * movementyaxis + cameraRightVec * movementxaxis;
        moveDir = Vector3.ClampMagnitude(moveDir, moveRate);



        //jump and falling support
        if (charCtrl.isGrounded)
        {
            fallpow = -2f;
            if (canJump && Input.GetButtonDown("Jump"))
            {
                fallpow = jumppow;
            }
            //Animation
            animCtrl.SetBool("isJumping", false);
            animCtrl.SetFloat("Speed", moveDir.magnitude);
        }
        else
        {
            if (fallpow > -2.0f || fallpow < -2.0f-fallspd )
            //if (!Mathf.Approximately(fallpow,-2))
            {
                animCtrl.SetBool("isJumping", true);
            }
            fallpow += Physics.gravity.y * Time.deltaTime * fallspd;
        }

        //moving floor support
        if (groundobj)
        {
            if (groundobj == beforeGroundObj)
            {
                floorOffset = beforePos - groundobj.position;
            }
            else
            {
                beforeGroundObj = groundobj;
                floorOffset = Vector3.zero;
            }
            beforePos = groundobj.position;
            beforeGroundObj = groundobj;
        }        //Movement
        charCtrl.Move(((new Vector3(0, fallpow, 0) + (moveDir * speed)) * Time.deltaTime) - floorOffset);

        // 常に-Z軸方向（前方向）を向く
        transform.rotation = Quaternion.identity;

        // アニメーション用の入力値設定
        if (useAnimationRotate)
        {
            animCtrl.SetFloat("X", xaxis);
            animCtrl.SetFloat("Y", yaxis);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Ground object detecting
        if (hit.gameObject != gameObject && !hit.collider.isTrigger)
            groundobj = hit.transform;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Ground object detecting
    //    if (other.gameObject != gameObject && !other.isTrigger)
    //        groundobj = other.transform;
    //}
}
