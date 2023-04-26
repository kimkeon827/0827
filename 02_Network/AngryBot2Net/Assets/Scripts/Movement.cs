using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class Movement : MonoBehaviourPunCallbacks
{
    // 컴포넌트 캐시 처리를 위한 변수
    private CharacterController controller;
    private new Transform transform;
    private Animator animator;
    private new Camera camera;

    // 가상의 Plane에 레이캐스팅하기 위한 변수
    private Plane plane;
    private Ray ray;
    private Vector3 hitPoint;

    // PhotonView 컴포넌트 캐시 처리를 위한 변수
    private PhotonView pv;

    // 시네머신 가상 카메라를 저장할 변수
    private CinemachineVirtualCamera virtualCamera;

    // 이동 속도
    public float moveSpeed = 10.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        // PhotonView가 자신의 것일 경우 시네머신 가상카메라를 연결
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        // 가상의 바닥을 주인공의 위치를 기준으로 생성
        plane = new Plane(transform.up, transform.position);
    }

    private void Update()
    {
        // 자신이 생성한 네트워크 객체만 컨트롤
        if (pv.IsMine)
        {
            Move();
            Turn();
        }
    }

    // 키보드 입력값 연결
    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    // 이동 처리하는 함수
    void Move()
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        // 이동할 방향 벡터 계산
        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        moveDir.Set(moveDir.x, 0.0f, moveDir.z);

        // 주인공 캐릭터 이동 처리
        controller.SimpleMove(moveDir * moveSpeed);

        // 주인공 캐릭터의 애니메이션 처리
        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);
    }

    // 회전 처리하는 함수
    void Turn()
    {
        // 마우스의 2차원 좌푯값을 이용해 3차원 광선(레이)을 생성
        ray = camera.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;
        
        // 가상의 바닥에 레이를 발사해 충돌한 지점의 거리를 enter 변수로 반환
        plane.Raycast(ray, out enter);
        // 가상의 바닥에 레이가 충돌한 좌푯값 추출
        hitPoint = ray.GetPoint(enter);

        // 회전해야 할 방향의 벡터를 계산
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;
        // 주인공 캐릭터의 회전값 지정
        transform.localRotation = Quaternion.LookRotation(lookDir);
    }
}
