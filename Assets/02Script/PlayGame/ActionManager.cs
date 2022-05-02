using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Util;

namespace Lulu.Stage
{
    public class ActionManager
    {
        Transform m_Container;          //컨테이너(Board GameObject)
        Stage m_Stage;                  //Stage 객체를 참조
        MonoBehaviour m_MonoBehaviour;  //코루틴 호출시 필요한 MonoBehaviour


        bool m_bRunning;                //액션 실행 상태 : 실행중인 경우 true

        public ActionManager(Transform container, Stage stage)
        {
            m_Container = container;
            m_Stage = stage;

            m_MonoBehaviour = container.gameObject.GetComponent<MonoBehaviour>();
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return m_MonoBehaviour.StartCoroutine(routine);
        }

        public void DoSwipeAction(int nRow, int nCol, Swipe swipeDir)
        {
            Debug.Assert(nRow >= 0 && nRow < m_Stage.maxRow && nCol >= 0 && nCol < m_Stage.maxCol);

            if (m_Stage.IsValideSwipe(nRow, nCol, swipeDir))
            {
                StartCoroutine(CoDoSwipeAction(nRow, nCol, swipeDir));
            }
        }

        IEnumerator CoDoSwipeAction(int nRow, int nCol, Swipe swipeDir)
        {
            if (!m_bRunning)     //다른 액션이 수행 중이면 PASS
            {
                m_bRunning = true;

                SoundManager.instance.PlayOneShot(Clip.Chomp);

                //1. swipe action 수행
                Returnable<bool> bSwipedBlock = new Returnable<bool>(false);    //EvaluateBoard() Enumerator 호출 결과를 수신받을 Returnable<bool> 객체를 생성한다.
                                                                                // 실형결과 bool값을 리턴받으며 3매칭 블럭이 발견되는 경우 true, 없는경우 false값을 가진다    
                yield return m_Stage.CoDoSwipeAction(nRow, nCol, swipeDir, bSwipedBlock);

                //2. 스와이프 성공한 경우 보드를 평가(매치블럭삭제, 빈블럭 드롭, 새블럭 Spawn등)한다.
                if (bSwipedBlock.value)
                {
                    Returnable<bool> bMatchBlock = new Returnable<bool>(false);
                    yield return EvaluateBoard(bMatchBlock);    //보드에 게임규칙을 적용하는 Enumerator을 실행

                    //스와이프한 블럭이 매치되지 않은 경우에 원상태 복귀
                    if (!bMatchBlock.value)
                    {
                        yield return m_Stage.CoDoSwipeAction(nRow, nCol, swipeDir, bSwipedBlock);
                    }
                }

                m_bRunning = false;     //액션 실행 상태 OFF
            }
            yield break;
        }

        /*
         * 현상태에서 보드를 평가한다. 즉 보드를 구성하는 블럭에 게임규칙을 적용시킨다.
         * 매치된 블럭은 제거하고 빈자리에는 새로운 블럭을 생성한다.    
         * matchResult : 실행 결과를 리턴받은 클래스 
         *               true : 매치된 블럭 있는 경우, false : 없는 경우
         */
        IEnumerator EvaluateBoard(Returnable<bool> matchResult)
        {
            while (true)    //매치된 블럭이 있는 경우 반복 수행한다.
            {
                //1. 매치 블럭 제거
                Returnable<bool> bBlockMatched = new Returnable<bool>(false);
                yield return StartCoroutine(m_Stage.Evaluate(bBlockMatched));

                //2. 3매치 블럭이 있는 경우 후처리 실행(블럭 드롭 등)
                if (bBlockMatched.value)
                {
                    matchResult.value = true;

                    SoundManager.instance.PlayOneShot(Clip.BlcokClear);

                    //매칭 블럭 제거 후 빈블럭 드럽 후 새 블럭 생성
                    yield return StartCoroutine(m_Stage.PostprocessAfterEvaluate());
                }
                //3. 3매치 블럭이 없는 경우 while 문 종료
                else
                    break;
            }

            yield break;
        }
    }
}