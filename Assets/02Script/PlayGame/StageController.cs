using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Util;

namespace Lulu.Stage 
{
    public class StageController : MonoBehaviour
    {
        bool m_bInit;   //초기화 여부상태 플래그
        Stage m_Stage;  //Stage를 참조한 변수 선언
        InputManager m_InputManager;
        ActionManager m_ActionManager;

        //Members for Event
        bool m_bTouchDown;          //입력상태 처리 플래그, 유효한 블럭을 클릭한 경우 true
        BlockPos m_BlockDownPos;    //블럭 위치 (보드에 저장된 위치)
        Vector3 m_ClickPos;         //DOWN 위치(보드 기준 local 좌표)

        [SerializeField] Transform m_Container;
        [SerializeField] GameObject m_CellPrefab;
        [SerializeField] GameObject m_BlockPrefab;

        void Start()    
        {
            InitStage();    //씬이 시작되면 초기화 함수를 호출
        }
        private void Update()
        {
            if (!m_bInit)
                return;

            OnInputHandler();
        }


        void InitStage()    //컨트롤러 초기화 함수로 초기화 상태 플래그를 설정
        {
            if (m_bInit)
                return;

            m_bInit = true;
            m_InputManager = new InputManager(m_Container);

            BuildStage();   //초기화 과정에서 BuildStage() 메소드를 호출

            //m_Stage.PrintAll(); //구성정보 확인
        }

        void BuildStage()
        {
            //1. 스테이지를 구성한다
            m_Stage = StageBuilder.BuildStage(nStage: 1);
            m_ActionManager = new ActionManager(m_Container, m_Stage);  //컨테이너와 스테이지 객체정보를 전달

            //2. 생성한 stage정보를 이용하여 씬을 구성한다.
            m_Stage.ComposeStage(m_CellPrefab, m_BlockPrefab, m_Container);
        }


        void OnInputHandler()
        {
            //Touch Down
            if(!m_bTouchDown && m_InputManager.isTouchDown)
            {
                //1.1 보드 기준 Local 좌표를 구한다.
                Vector2 point = m_InputManager.touch2BoardPosition;
                //Debug.Log($"Input Down= {point}, local = {m_InputManager.touch2BoardPosition}");

                //1.2 Play 영역(보드)에서 클릭하지 않는 경우는 무시
                if (!m_Stage.IsInsideBoard(point))
                    return;
                //1.3 클릭한 위치의 블럭을 구한다.
                BlockPos blockPos;
                if(m_Stage.IsOnValideBlock(point, out blockPos))
                {
                    //1.3.1 유효한(스와이프 가능한) 블럭에서 클릭한 경우
                    m_bTouchDown = true;    //클릭 상태 플래그 ON
                    m_BlockDownPos = blockPos;  //클릭한 블럭의 위치(row, col)저장
                    m_ClickPos = point;         //클릭한 Local 좌표 저장
                   // Debug.Log($"Mouse Down In Board : {blockPos})");
                }
            }
            //2. Touch Up: 유효한 블럭 위에서 Down 후에만 Up 이벤트처리
            else if(m_bTouchDown && m_InputManager.isTouchUp)
            {
                //2.1 보드 기준 local 좌표를 구한다
                Vector2 point = m_InputManager.touch2BoardPosition;
      
                //2.2 스와이프 방향을 구한다.
                Swipe swipeDir = m_InputManager.EvalSwiperDir(m_ClickPos, point);

                Debug.Log($"Swipe : {swipeDir}, Block = {m_BlockDownPos}");

                if (swipeDir != Swipe.NA)
                    m_ActionManager.DoSwipeAction(m_BlockDownPos.row, m_BlockDownPos.col, swipeDir);

                m_bTouchDown = false;   //클릭 상태 플래그 OFF
            }
        }
    }
}
