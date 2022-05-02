using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Util;

namespace Lulu.Stage
{
    public class ActionManager
    {
        Transform m_Container;          //�����̳�(Board GameObject)
        Stage m_Stage;                  //Stage ��ü�� ����
        MonoBehaviour m_MonoBehaviour;  //�ڷ�ƾ ȣ��� �ʿ��� MonoBehaviour


        bool m_bRunning;                //�׼� ���� ���� : �������� ��� true

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
            if (!m_bRunning)     //�ٸ� �׼��� ���� ���̸� PASS
            {
                m_bRunning = true;

                SoundManager.instance.PlayOneShot(Clip.Chomp);

                //1. swipe action ����
                Returnable<bool> bSwipedBlock = new Returnable<bool>(false);    //EvaluateBoard() Enumerator ȣ�� ����� ���Ź��� Returnable<bool> ��ü�� �����Ѵ�.
                                                                                // ������� bool���� ���Ϲ����� 3��Ī ���� �߰ߵǴ� ��� true, ���°�� false���� ������    
                yield return m_Stage.CoDoSwipeAction(nRow, nCol, swipeDir, bSwipedBlock);

                //2. �������� ������ ��� ���带 ��(��ġ������, ��� ���, ���� Spawn��)�Ѵ�.
                if (bSwipedBlock.value)
                {
                    Returnable<bool> bMatchBlock = new Returnable<bool>(false);
                    yield return EvaluateBoard(bMatchBlock);    //���忡 ���ӱ�Ģ�� �����ϴ� Enumerator�� ����

                    //���������� ���� ��ġ���� ���� ��쿡 ������ ����
                    if (!bMatchBlock.value)
                    {
                        yield return m_Stage.CoDoSwipeAction(nRow, nCol, swipeDir, bSwipedBlock);
                    }
                }

                m_bRunning = false;     //�׼� ���� ���� OFF
            }
            yield break;
        }

        /*
         * �����¿��� ���带 ���Ѵ�. �� ���带 �����ϴ� ���� ���ӱ�Ģ�� �����Ų��.
         * ��ġ�� ���� �����ϰ� ���ڸ����� ���ο� ���� �����Ѵ�.    
         * matchResult : ���� ����� ���Ϲ��� Ŭ���� 
         *               true : ��ġ�� �� �ִ� ���, false : ���� ���
         */
        IEnumerator EvaluateBoard(Returnable<bool> matchResult)
        {
            while (true)    //��ġ�� ���� �ִ� ��� �ݺ� �����Ѵ�.
            {
                //1. ��ġ �� ����
                Returnable<bool> bBlockMatched = new Returnable<bool>(false);
                yield return StartCoroutine(m_Stage.Evaluate(bBlockMatched));

                //2. 3��ġ ���� �ִ� ��� ��ó�� ����(�� ��� ��)
                if (bBlockMatched.value)
                {
                    matchResult.value = true;

                    SoundManager.instance.PlayOneShot(Clip.BlcokClear);

                    //��Ī �� ���� �� ��� �巴 �� �� �� ����
                    yield return StartCoroutine(m_Stage.PostprocessAfterEvaluate());
                }
                //3. 3��ġ ���� ���� ��� while �� ����
                else
                    break;
            }

            yield break;
        }
    }
}