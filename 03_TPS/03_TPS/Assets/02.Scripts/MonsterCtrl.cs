using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 내비게이션 기능을 사용하기위해 추가해야 하는 네임스페이스
using UnityEngine.AI;
public class MonsterCtrl : MonoBehaviour
{
    // 몬스터의 상태 정보
    public enum State{
        IDLE,
        TRACE,
        ATTCK,
        DIE
    }

    // 몬스터의 현재 상태
    public State state = State.IDLE;
    // 추적 사정거리
    public float traceDist = 10.0f;
    // 공격 사정거리
    public float attackDist = 2.0f;
    // 몬스터의 사망 여부
    public bool isDie = false;

    // 컴포넌트의 캐시를 처리할 변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    // Animator 파라미터의 해시값 추출
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");

    // 혈흔 효과 프리팹
    private GameObject bloodEffect;

    void Start (){
        // 몬스터의 Transform 할당
        monsterTr = GetComponent<Transform>();

        // 추적 대상인 Player의 Transform 할당
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        // NavMeshAgent 컴포넌트 할당
        agent = GetComponent<NavMeshAgent>();

        // Animator 컴포넌트 할당
        anim = GetComponent<Animator>();        

        // BloodSprayEffect 프리팹 로드
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

        // 몬스터의 상태를 체크하는 코루틴 함수 호출
        StartCoroutine(CheckMonsterState());
        // 상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출
        StartCoroutine(MonsterAction());
    }

    // 일정한 간격으로 몬스터의 행동 상태를 체크
    IEnumerator CheckMonsterState(){
        while(!isDie){
            // 0.3초동안 중지(대기)하는 동안 제어권을 메시지 루프에 양보
            yield return new WaitForSeconds(0.3f);

            // 몬스터와 주인공 캐릭터 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            // 공격 사정거리 범위로 들어왔는지 확인
            if (distance <= attackDist){
                state = State.ATTCK;
            }
            // 추적 사정거리 범위로 들어왔는지 확인
            else if (distance <= traceDist){
                state = State.TRACE;
            }
            else{
                state = State.IDLE;
            }
        }
    }

    // 몬스터의 상태에 따라 몬스터의 동작을 수행
    IEnumerator MonsterAction(){
        while (!isDie){
            switch (state){
                // IDLE 상태
                case State.IDLE:
                    // 추적 중지
                    agent.isStopped = true;

                    // Animator의 IsTrace 변수를 false로 설정
                    anim.SetBool(hashTrace, false);
                    break;

                // 추적 상태
                case State.TRACE:
                    // 추적 대상의 좌표로 이동 시작
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;

                    // Animator의 IsTrace 변수를 true로 설정
                    anim.SetBool(hashTrace, true);

                    // Animator의 IsAttack 변수를 false로 설정
                    anim.SetBool(hashAttack, false);
                    break;

                // 공격 상태
                case State.ATTCK:
                    // Animator의 IsAttack 변수를 true로 설정
                    anim.SetBool(hashAttack, true);
                    break;

                // 사망
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision coll){
        if (coll.collider.CompareTag("BULLET")){
            // 충돌된 총알을 삭제
            Destroy(coll.gameObject);
            // 피격 리액션 애니메이션 실행
            anim.SetTrigger(hashHit);

            // 총알의 충돌 지점
            Vector3 pos = coll.GetContact(0).point;
            // 총알의 충돌 지점의 법선 벡터
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            // 혈흔 효과를 생성하는 함수 호출
            ShowBloodEffect(pos, rot);
        }
    }

    void ShowBloodEffect(Vector3 pos, Quaternion rot){
        // 혈흔 효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
    }

    void OnDrawGizmos(){
        // 추적 사정거리 표시
        if (state == State.TRACE){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        // 공격 사정거리 표시
        if (state == State.ATTCK){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}