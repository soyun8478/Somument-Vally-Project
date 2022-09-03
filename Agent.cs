using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Ÿ��(��ǥ) ������ ���� �̵��ϵ��� ����� �ʹ�
//- ��ǥ����
//- nav Agent AI���� �ʿ�
//Agent Component
public class Agent : MonoBehaviour
{
    public Transform target;     //-��ǥ����
    public NavMeshAgent agent;   //-Agent Component

    bool isMove;                 // ���� �������� �̵��ϴ� ��!
    public float stopDistance = 0.1f; // �����Ѽ� ġ�� �Ÿ�

    public AudioSource bgmmove;




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �̵��϶�� ����� ��������
        if (isMove)
        {
            // Ÿ��(=������)�� ���� �Ÿ� �˻�..
            float distance = Vector3.Distance(target.transform.position, transform.position);
            // ���� �������� ���� �Ÿ� ���̰� 0.01 ���� ������ ������ �� ġ��
            if (distance <= stopDistance)
            {

                // ���࿡ �����ߴٸ�..isMove�� �ٽ� false
                isMove = false;

                // ���࿡ �����ߴٸ�..Idle �ִϸ��̼� ����
                GetComponentInChildren<Animator>().SetInteger("AnimationPar", 0);
                bgmmove.Stop();
            }
        }

        //���콺 ������ ��ư�� Ŭ���ϸ�
        if (Input.GetMouseButtonDown(1))
        {
            // ���Ӻ��׼� ������ ��ġ�� agent�� �̵���Ű���Ѵ�
            //3. ���Ӻ信�� ���콺 ��ġ�� �������� ��ǥ���� ����
            // - ī�޶󿡼� ���콺 ��ġ�� ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // �浹�� ���� Layer�� "Path"�ΰ�쿡�� Ŭ�� ����
            //int layerMask = LayerMask.NameToLayer("Path");
            // - �浹�� ���� �ִ� ���
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //2.��ǥ ������ ���Ѵ�
                target.position = hitInfo.point;
                //1.��ǥ�������� ������ ������ agent�� �̵��ϰ� �ϰ�ʹ�
                agent.SetDestination(target.position);

                // Move �ִϸ��̼� ����
                GetComponentInChildren<Animator>().SetInteger("AnimationPar", 1);
                // ���� �̵���!

                isMove = true;
            }
            bgmmove.Play();
        }
       
    }
}
