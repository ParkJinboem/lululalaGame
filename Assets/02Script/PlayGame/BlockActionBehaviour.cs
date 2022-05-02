using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Scriptable;
using Lulu.Util;

namespace Lulu.Board
{
    public class BlockActionBehaviour : MonoBehaviour
    {
        [SerializeField] BlockConfig m_BlockConfig;
        public bool isMoving { get; set; }  //�̵����� �Ӽ��� ����

        //�̵��� ��ġ�� �����ϴ� ť������
        //�ϳ��� ���� ���ؼ� �̵� ��û�� ������ �� �� �ִ�. 
        //ť�� �̵��� ��ġ�� ���̰� ť�� ���̴� ������� �� �̵��� ����ȴ�.
        Queue<Vector3> m_MovementQueue = new Queue<Vector3>();  //x=col, y=row, z = acceleration

        //�Ʒ������� �־��� �Ÿ���ŭ �̵��Ѵ�.
        //�̵��� ��� �������� �ʰ� ť�� �̵��� ��ġ�� �����Ѵ�. �̵��� �����ϴ� �ڷ�ƾ�� ���� ��� �ڷ�ƾ�� �����Ѵ�.
        //���� �����ӿ��� ť���� �̵� ������ ������ �̵� �ִϸ��̼��� ������ ���̴�.
        //fDropDistance : �̵��� ���� �� ��, �Ÿ� (unit)   
        public void MoveDrop(Vector2 vtDropDistance)
        {
            m_MovementQueue.Enqueue(new Vector3(vtDropDistance.x, vtDropDistance.y, 1));
            
            if(!isMoving)
            {
                StartCoroutine(DoActionMoveDrop());
            }
        }

        //��� �̵� �ִϸ��̼��� �����ϴ� Enumerator�� �����Ѵ�.
        //ť�� �̵������� ������ ������ �̵��� �����Ѵ�. ť�� �ִ� ��� ������ ó���� ������ �����Ѵ�.
        IEnumerator DoActionMoveDrop(float acc = 1.0f)
        {
            isMoving = true;

            while(m_MovementQueue.Count>0)
            {
                Vector2 vtDestination = m_MovementQueue.Dequeue();

                //�������� �Ÿ��� �ش�Ǵ� �ε����� ���Ѵ�.(1~9�� -> �ε��� 0~8�ǰ��� ���´�)
                int dropuIndex = System.Math.Min(9, System.Math.Max(1, (int)Mathf.Abs(vtDestination.y)));
                float duration = m_BlockConfig.dropSpeed[dropuIndex - 1];

                yield return CoStartDropSmooth(vtDestination, duration * acc);
            }

            isMoving = false;
            yield break;
        }

        //��� �ִϸ��̼��� �����ϴ� Enumerator�������Ѵ�.
        //�̵� ��ġ�� ����Ͽ� MoveTo �׼��� �����Ѵ�.
        IEnumerator CoStartDropSmooth(Vector2 vtDropDistance, float duration)
        {
            Vector3 to = new Vector3(transform.position.x + vtDropDistance.x, transform.position.y - vtDropDistance.y, transform.position.z);
            yield return Action2D.MoveTo(transform, to, duration);
        }
    }
}