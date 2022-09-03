using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//타겟(목표) 지점을 향해 이동하도록 만들고 싶다
//- 목표지점
//- nav Agent AI도구 필요
//Agent Component
public class Agent : MonoBehaviour
{
    public Transform target;     //-목표지점
    public NavMeshAgent agent;   //-Agent Component

    bool isMove;                 // 지금 목적지로 이동하는 중!
    public float stopDistance = 0.1f; // 도착한셈 치는 거리

    public AudioSource bgmmove;




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // 지금 이동하라고 명령이 들어왔으면
        if (isMove)
        {
            // 타깃(=목적지)과 나의 거리 검사..
            float distance = Vector3.Distance(target.transform.position, transform.position);
            // 만일 목적지와 나의 거리 사이가 0.01 보다 작으면 도착한 셈 치자
            if (distance <= stopDistance)
            {

                // 만약에 도착했다면..isMove는 다시 false
                isMove = false;

                // 만약에 도착했다면..Idle 애니메이션 실행
                GetComponentInChildren<Animator>().SetInteger("AnimationPar", 0);
                bgmmove.Stop();
            }
        }

        //마우스 오른쪽 버튼을 클릭하면
        if (Input.GetMouseButtonDown(1))
        {
            // 게임뷰테서 선택한 위치로 agent를 이동시키려한다
            //3. 게임뷰에서 마우스 위치를 기준으로 목표지점 설정
            // - 카메라에서 마우스 위치로 ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 충돌한 곳의 Layer가 "Path"인경우에만 클릭 가능
            //int layerMask = LayerMask.NameToLayer("Path");
            // - 충돌한 곳이 있는 경우
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //2.목표 지점을 구한다
                target.position = hitInfo.point;
                //1.목표지점으로 설정된 곳으로 agent를 이동하게 하고싶다
                agent.SetDestination(target.position);

                // Move 애니메이션 실행
                GetComponentInChildren<Animator>().SetInteger("AnimationPar", 1);
                // 지금 이동해!

                isMove = true;
            }
            bgmmove.Play();
        }
       
    }
}
