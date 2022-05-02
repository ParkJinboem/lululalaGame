using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Util;

namespace Lulu.Stage 
{
    public class StageController : MonoBehaviour
    {
        bool m_bInit;   //�ʱ�ȭ ���λ��� �÷���
        Stage m_Stage;  //Stage�� ������ ���� ����
        InputManager m_InputManager;
        ActionManager m_ActionManager;

        //Members for Event
        bool m_bTouchDown;          //�Է»��� ó�� �÷���, ��ȿ�� ���� Ŭ���� ��� true
        BlockPos m_BlockDownPos;    //�� ��ġ (���忡 ����� ��ġ)
        Vector3 m_ClickPos;         //DOWN ��ġ(���� ���� local ��ǥ)

        [SerializeField] Transform m_Container;
        [SerializeField] GameObject m_CellPrefab;
        [SerializeField] GameObject m_BlockPrefab;

        void Start()    
        {
            InitStage();    //���� ���۵Ǹ� �ʱ�ȭ �Լ��� ȣ��
        }
        private void Update()
        {
            if (!m_bInit)
                return;

            OnInputHandler();
        }


        void InitStage()    //��Ʈ�ѷ� �ʱ�ȭ �Լ��� �ʱ�ȭ ���� �÷��׸� ����
        {
            if (m_bInit)
                return;

            m_bInit = true;
            m_InputManager = new InputManager(m_Container);

            BuildStage();   //�ʱ�ȭ �������� BuildStage() �޼ҵ带 ȣ��

            //m_Stage.PrintAll(); //�������� Ȯ��
        }

        void BuildStage()
        {
            //1. ���������� �����Ѵ�
            m_Stage = StageBuilder.BuildStage(nStage: 1);
            m_ActionManager = new ActionManager(m_Container, m_Stage);  //�����̳ʿ� �������� ��ü������ ����

            //2. ������ stage������ �̿��Ͽ� ���� �����Ѵ�.
            m_Stage.ComposeStage(m_CellPrefab, m_BlockPrefab, m_Container);
        }


        void OnInputHandler()
        {
            //Touch Down
            if(!m_bTouchDown && m_InputManager.isTouchDown)
            {
                //1.1 ���� ���� Local ��ǥ�� ���Ѵ�.
                Vector2 point = m_InputManager.touch2BoardPosition;
                //Debug.Log($"Input Down= {point}, local = {m_InputManager.touch2BoardPosition}");

                //1.2 Play ����(����)���� Ŭ������ �ʴ� ���� ����
                if (!m_Stage.IsInsideBoard(point))
                    return;
                //1.3 Ŭ���� ��ġ�� ���� ���Ѵ�.
                BlockPos blockPos;
                if(m_Stage.IsOnValideBlock(point, out blockPos))
                {
                    //1.3.1 ��ȿ��(�������� ������) ������ Ŭ���� ���
                    m_bTouchDown = true;    //Ŭ�� ���� �÷��� ON
                    m_BlockDownPos = blockPos;  //Ŭ���� ���� ��ġ(row, col)����
                    m_ClickPos = point;         //Ŭ���� Local ��ǥ ����
                   // Debug.Log($"Mouse Down In Board : {blockPos})");
                }
            }
            //2. Touch Up: ��ȿ�� �� ������ Down �Ŀ��� Up �̺�Ʈó��
            else if(m_bTouchDown && m_InputManager.isTouchUp)
            {
                //2.1 ���� ���� local ��ǥ�� ���Ѵ�
                Vector2 point = m_InputManager.touch2BoardPosition;
      
                //2.2 �������� ������ ���Ѵ�.
                Swipe swipeDir = m_InputManager.EvalSwiperDir(m_ClickPos, point);

                Debug.Log($"Swipe : {swipeDir}, Block = {m_BlockDownPos}");

                if (swipeDir != Swipe.NA)
                    m_ActionManager.DoSwipeAction(m_BlockDownPos.row, m_BlockDownPos.col, swipeDir);

                m_bTouchDown = false;   //Ŭ�� ���� �÷��� OFF
            }
        }
    }
}
