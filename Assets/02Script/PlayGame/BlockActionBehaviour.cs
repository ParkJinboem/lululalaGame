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
        public bool isMoving { get; set; }  //이동상태 속성을 정의

        //이동할 위치를 저장하는 큐를생성
        //하나의 블럭에 대해서 이동 요청이 여러번 올 수 있다. 
        //큐에 이동할 위치가 쌓이고 큐에 쌓이는 순서대로 블럭 이동이 적용된다.
        Queue<Vector3> m_MovementQueue = new Queue<Vector3>();  //x=col, y=row, z = acceleration

        //아래쪽으로 주어진 거리만큼 이동한다.
        //이동을 즉시 실행하지 않고 큐에 이동할 위치를 보관한다. 이동을 수행하는 코루틴이 없는 경우 코루틴을 시작한다.
        //다음 프레임에서 큐에서 이동 정보를 꺼내어 이동 애니메이션을 수행할 것이다.
        //fDropDistance : 이동할 스텝 수 즉, 거리 (unit)   
        public void MoveDrop(Vector2 vtDropDistance)
        {
            m_MovementQueue.Enqueue(new Vector3(vtDropDistance.x, vtDropDistance.y, 1));
            
            if(!isMoving)
            {
                StartCoroutine(DoActionMoveDrop());
            }
        }

        //드롭 이동 애니메이션을 수행하는 Enumerator를 정의한다.
        //큐에 이동정보가 있으면 꺼내서 이동을 적용한다. 큐에 있는 모든 정보를 처리할 때까지 수행한다.
        IEnumerator DoActionMoveDrop(float acc = 1.0f)
        {
            isMoving = true;

            while(m_MovementQueue.Count>0)
            {
                Vector2 vtDestination = m_MovementQueue.Dequeue();

                //떨어지는 거리에 해당되는 인덱스를 구한다.(1~9행 -> 인덱스 0~8의값을 갖는다)
                int dropuIndex = System.Math.Min(9, System.Math.Max(1, (int)Mathf.Abs(vtDestination.y)));
                float duration = m_BlockConfig.dropSpeed[dropuIndex - 1];

                yield return CoStartDropSmooth(vtDestination, duration * acc);
            }

            isMoving = false;
            yield break;
        }

        //드롭 애니메이션을 수행하는 Enumerator를정의한다.
        //이동 위치를 계산하여 MoveTo 액션을 수행한다.
        IEnumerator CoStartDropSmooth(Vector2 vtDropDistance, float duration)
        {
            Vector3 to = new Vector3(transform.position.x + vtDropDistance.x, transform.position.y - vtDropDistance.y, transform.position.z);
            yield return Action2D.MoveTo(transform, to, duration);
        }
    }
}