using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Board;
using Lulu.Util;
using Lulu.Core;
using System;

namespace Lulu.Stage
{
    public class Stage
    {

        //���������� �÷����� �������� ������ ����� ����
        Lulu.Board.Board m_Board;
        //�÷����� �������� ũ�⸦ ������ ����� �Ӽ��� ����
        public int maxRow { get { return m_Board.maxRow; } }
        public int maxCol { get { return m_Board.maxCol; } }

        
        public Lulu.Board.Board board { get { return m_Board; } }

        StageBuilder m_StageBuilder;

        public Block[,] blocks { get { return m_Board.blocks; } }
        public Cell[,] cells { get { return m_Board.cells; } }

        public Stage(StageBuilder stageBuilder, int nRow, int nCol)
        {
            m_StageBuilder = stageBuilder;
            m_Board = new Lulu.Board.Board(nRow, nCol);
        }

        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            m_Board.ComposeStage(cellPrefab, blockPrefab, container, m_StageBuilder);
        }
      
        public IEnumerator CoDoSwipeAction(int nRow, int nCol, Swipe swipeDir, Returnable<bool> actionResult)
        {
            actionResult.value = false; //�ڷ�ƾ ���ϰ� RESET

            //1. ���������Ǵ� ��� �� ��ġ�� ���Ѵ�.(using SwipeDir ExtensionMethod)
            int nSwipeRow = nRow, nSwipeCol = nCol;
            nSwipeRow += swipeDir.GetTargetRow();   //Right : +1, LEFT :-1
            nSwipeCol += swipeDir.GetTargetCol();   //UP : +1, DOWN : -1

            Debug.Assert(nRow != nSwipeRow || nCol != nSwipeCol, "Invali Swipe : ({nSwipeRow}, {nSwipeCol})");
            Debug.Assert(nSwipeRow >= 0 && nSwipeRow < maxRow && nSwipeCol >= 0 && nSwipeCol < maxCol, $"Swipe Ÿ�� �� �ε��� ���� = ({nSwipeRow}, {nSwipeCol}) ");

            //2. �������� ������ ������ üũ�Ѵ�. (�ε��� Validation�� ȣ�� ���� ������)
            if(m_Board.IsSwipeable(nSwipeRow, nSwipeCol))
            {
                //2.1 �������� ��� ��(�ҽ�, Ÿ��)�� �� ���� �̵��� ��ġ�� �����Ѵ�.
                Block targetBlock = blocks[nSwipeRow, nSwipeCol];
                Block baseBlock = blocks[nRow, nCol];
                Debug.Assert(baseBlock != null && targetBlock != null);

                Vector3 basePos = baseBlock.blockObj.transform.position;
                Vector3 targetPos = targetBlock.blockObj.transform.position;

                //2.2 �������� �׼��� �����Ѵ�.
                if(targetBlock.IsSwipeable(baseBlock))
                {
                    //2.2.1 ���� �� ��ġ�� �̵��ϴ� �ִϸ��̼��� �����Ѵ�.
                    baseBlock.MoveTo(targetPos, Constants.SWIPE_DURATION);
                    targetBlock.MoveTo(basePos, Constants.SWIPE_DURATION);

                    yield return new WaitForSeconds(Constants.SWIPE_DURATION);

                    //2.2.2 Board�� ����� ���� ��ġ�� ��ȯ�Ѵ�.
                    blocks[nRow, nCol] = targetBlock;
                    blocks[nSwipeRow, nSwipeCol] = baseBlock;

                    actionResult.value = true;
                }
            }

            yield break;
        }

        public IEnumerator Evaluate(Returnable<bool> matchResult)
        {
            yield return m_Board.Evaluate(matchResult);
        }

        //��Ī�� ���� ���������� ��ó�������� ���
        //�� �귰�� ���� ���� Drop�ؼ� ä�� �Ŀ� ���ο� ������ ���ڸ��� ä���.
        public IEnumerator PostprocessAfterEvaluate()   //��ó�� �Լ� ����
        {
            List<KeyValuePair<int, int>> unfilledBlocks = new List<KeyValuePair<int, int>>();
            //�� ����� �Ϸ� �Ǿ����� �ٸ� ������ ä������ �ʰ� ������ �� ��ġ�� �����ϱ� ���� List�� ����
            //��ֹ��� �ִ� ��� ������ ä������ �ʴ� �� ����� �߻��� �� �ִ�.
            List<Block> movingBlocks = new List<Block>();   //��ӵǴ� ���� �����ϴ� List�� ����(��� ����͸��� �ʿ�)

            //1. ���ŵ� ���� ���� �� ���ġ(���� -> ���� �̵�/�ִϸ��̼�)
            yield return m_Board.ArrangeBlocksAfterClean(unfilledBlocks, movingBlocks); //Board��ü�� �� ���� ä�쵵�� ��û

            //2. ���ġ �Ϸ�(�̵� �ִϸ��̼� �Ϸ�)��, ����ִ� �� �ٽ� ����
            yield return m_Board.SpawnBlocksAfterClean(movingBlocks);

            //3. �� ����� ��, ��ġ�� �����ϱ� ���� ������ ����
            //   �������� ������ ���� ��õ��� ���̵��� �ٸ� ���� ��ӵǴ� ���� ����Ѵ�.
            yield return WaitForDropping(movingBlocks);
        }

        //����Ʈ�� ���Ե� ���� �ִϸ��̼��� ������ ���� ��ٸ���.
        public IEnumerator WaitForDropping(List<Block> movingBlocks)
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(0.05f);   //50ms ���� �˻��Ѵ�.

            while(true) //�ִϸ��̼� ���� ���� ���� ������ ���ѷ���
            {
                bool bContinue = false;

                //�̵����� ���� �ִ��� �˻��Ѵ�.
                for (int i = 0; i < movingBlocks.Count; i++)  //�̵����� ���� ������ for������
                {
                    if (movingBlocks[i].isMoving)
                    {
                        bContinue = true;
                        break;
                    }
                }

                if (!bContinue) //�̵� ���� ���� ������ ��, ��� �� �ִϸ��̼��� ����� ��� while���� ����
                    break;

                yield return waitForSecond;
            }

            movingBlocks.Clear();   //�̵����� �� ����Ʈ�� �ʱ�ȭ
            yield break;
        }

        #region Simple Methods
        //----------------------------------------------------------------------
        // ��ȸ(get/set/is) �޼ҵ�
        //----------------------------------------------------------------------

        /*
       * ����ȿ��� �߻��� �̺�Ʈ���� üũ�Ѵ�       
       */
        public bool IsInsideBoard(Vector2 ptOrg)
        {
            // ����� ���Ǹ� ���ؼ� (0, 0)�� �������� ��ǥ�� �̵��Ѵ�. 
            // 8 x 8 ������ ���: x(-4 ~ +4), y(-4 ~ +4) -> x(0 ~ +8), y(0 ~ +8) 
            Vector2 point = new Vector2(ptOrg.x + (maxCol / 2.0f), ptOrg.y + (maxRow / 2.0f));

            if (point.y < 0 || point.x < 0 || point.y > maxRow || point.x > maxCol)
                return false;

            return true;
        }

        /*
      * ��ȿ�� ��(�̵������� ��) ������ �ִ��� üũ�Ѵ�.
      * @param point Wordl ��ǥ, �����̳� ����
      * @param blockPos out �Ķ����, ���忡 ����� ���� �ε���
      * 
      * @return �������� �����ϸ� true
      */
        public bool IsOnValideBlock(Vector2 point, out BlockPos blockPos)
        {
            //1. World ��ǥ -> ������ �� �ε����� ��ȯ�Ѵ�.
            Vector2 pos = new Vector2(point.x + (maxCol / 2.0f), point.y + (maxRow / 2.0f));
            int nRow = (int)pos.y;
            int nCol = (int)pos.x;

            //������ �� �ε��� ����
            blockPos = new BlockPos(nRow, nCol);

            //2. �������� �������� üũ�Ѵ�.
            return board.IsSwipeable(nRow, nCol);
        }

        public bool IsValideSwipe(int nRow, int nCol, Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.DOWN: return nRow > 0; ;
                case Swipe.UP: return nRow < maxRow - 1;
                case Swipe.LEFT: return nCol > 0;
                case Swipe.RIGHT: return nCol < maxCol - 1;
                default: return false;
            }
        }

        #endregion

        public void PrintAll()
        {
            System.Text.StringBuilder strCells = new System.Text.StringBuilder();
            System.Text.StringBuilder strBlocks = new System.Text.StringBuilder();

            for(int nRow = maxRow-1; nRow>=0;nRow--)
            {
                for(int nCol = 0; nCol < maxCol; nCol++)
                {
                    strCells.Append($"{cells[nRow, nCol].type} ");
                    strBlocks.Append($"{blocks[nRow, nCol].type}, ");
                }

                strCells.Append("\n");
                strBlocks.Append("\n");
            }
            Debug.Log(strCells.ToString());
            Debug.Log(strBlocks.ToString());


        }
    }

    
}
