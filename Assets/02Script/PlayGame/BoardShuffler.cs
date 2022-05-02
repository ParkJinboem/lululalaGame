using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Core;

namespace Lulu.Board
{
    using BlockVectorKV = KeyValuePair<Block, Vector2Int>;

    //using���� ����ؼ� type�� �������Ѵ�(Generic TypeAlias)

    public class BoardShuffler
    {
        Board m_Board;  //�۾� ����� �Ǵ� ���� ��ü�� �����ϴ� ����� ����
        bool m_bLoadingMode;    //���� ���۵ǰ� ���� �����ÿ� ȣ��Ǹ� true, �÷��� ���߿��� false

        SortedList<int, BlockVectorKV> m_OrgBlocks = new SortedList<int, BlockVectorKV>();  //���� ���� ���� ����ϴ� SortedList
        IEnumerator<KeyValuePair<int, BlockVectorKV>> m_it;                 //SortedList���� ���� �ϳ��� �����µ� ���
        Queue<BlockVectorKV> m_UnusedBlocks = new Queue<BlockVectorKV>();   //�� ��ġ �����߿� 3��ġ �߻��� ���� �ӽ÷� �����ϴ� ť
        bool m_bListComplete;                                               //sortedList���� ��ȸ�� ���� ���������� true, ��ȸ�� ��� ��ġ�� ť�� �����ִ� ���� ó���ϴ� �����̸� false

        public BoardShuffler(Board board, bool bLoadingMode)    //������. ����� ȣ���带 �����Ѵ�.
        {
            m_Board = board;
            m_bLoadingMode = bLoadingMode;
        }

        //Shuffle() �޼ҵ� �ۼ�
        public void Shuffle(bool bAnimation = false)
        {
            //1. ���� �������� �� ���� ��Ī ������ ������Ʈ�Ѵ�
            PrepareDuplicationDates();

            //2. ���� ��� ���� ���� ����Ʈ�� �����Ѵ�.
            PrepareShuffleBlocks();

            //3. 1),2)���� �غ��� �����͸� �̿��Ͽ� ������ ����
            RunShuffle(bAnimation);
        }
     
        BlockVectorKV NextBlock(bool bUseQueue)
        {
            if (bUseQueue && m_UnusedBlocks.Count > 0)
                return m_UnusedBlocks.Dequeue();

            if (!m_bListComplete && m_it.MoveNext())
                return m_it.Current.Value;

            m_bListComplete = true;

            return new BlockVectorKV(null, Vector2Int.zero);
        }

        //�� ��Ī ���� ������Ʈ
        void PrepareDuplicationDates()
        {
            for (int nRow = 0; nRow < m_Board.maxRow; nRow++)
                for (int nCol = 0; nCol < m_Board.maxCol; nCol++)
                {
                    Block block = m_Board.blocks[nRow, nCol];

                    if (block == null)
                        continue;

                    if (m_Board.CanShuffle(nRow, nCol, m_bLoadingMode))
                        block.ResetDuplicationInfo();
          
                    //�������� ���ϴ� ��(���ٿ� ���� ���)�Ǹ�Ī ������ ���
                    else
                    {
                        block.horzDuplicate = 1;
                        block.vertDuplicate = 1;

                        //���� ��ġ�� ���� �̴��(��, ������ ���ϴ� ��)�� ���� ��ġ ���¸� �ݿ�
                        //(3���̻� ��ġ�Ǵ� ���� �߻����� �ʱ� ������ ������ ���� �˻��ϸ� �ȴ�)
                        //Note : ���ϸ� ����ص� ��ü ���� ��� �˻��� �� �ִ�.
                        if (nCol > 0 && !m_Board.CanShuffle(nRow, nCol - 1, m_bLoadingMode) && m_Board.blocks[nRow, nCol - 1].IsSafeEqual(block))
                        {
                            block.horzDuplicate = 2;
                            m_Board.blocks[nRow, nCol - 1].horzDuplicate = 2;
                        }
                        if (nRow > 0 && !m_Board.CanShuffle(nRow-1, nCol, m_bLoadingMode) && m_Board.blocks[nRow-1, nCol].IsSafeEqual(block))
                        {
                            block.vertDuplicate = 2;
                            m_Board.blocks[nRow-1, nCol].vertDuplicate = 2;
                        }

                    }
                }
        }

        //�� ������ �غ�
        void PrepareShuffleBlocks()
        {
            for (int nRow = 0; nRow < m_Board.maxRow; nRow++)
                for (int nCol = 0; nCol < m_Board.maxCol; nCol++)
                {
                    if (!m_Board.CanShuffle(nRow, nCol, m_bLoadingMode))    //���� ����� �ƴѰ�� ����Ʈ�� �������� ����
                        continue;

                    //Sorted List�� ������ ���ϱ� ���ؼ� �ߺ����� ������ ���� ���� �������� Ű������ �����Ѵ�.
                    while (true)
                    {
                        int nRandom = Random.Range(0, 10000);
                        if (m_OrgBlocks.ContainsKey(nRandom))
                            continue;

                        m_OrgBlocks.Add(nRandom, new BlockVectorKV(m_Board.blocks[nRow, nCol], new Vector2Int(nCol, nRow)));
                        break;
                    }
                }

            m_it = m_OrgBlocks.GetEnumerator();
        }

        //�غ�� �����ͷ� ���� ����
        void RunShuffle(bool bAnimation)
        {
            for (int nRow = 0; nRow < m_Board.maxRow; nRow++)
            {
                for (int nCol = 0; nCol < m_Board.maxCol; nCol++)
                {
                    //1. ���� �̴�� ���� PASS
                    if (!m_Board.CanShuffle(nRow, nCol, m_bLoadingMode))
                        continue;

                    //2. ���� ��� ���� ���� ��ġ�� ���� ���Ϲ޾Ƽ� �����Ѵ�.
                    m_Board.blocks[nRow, nCol] = GetShuffledBlock(nRow, nCol);
                }
            }
        }

        //������ ��ġ�� ���� �� ���ϱ�
        Block GetShuffledBlock(int nRow, int nCol)
        {
            BlockBreed prevBreed = BlockBreed.NA;   //ó�� �񱳽ÿ� ����������
            Block firstBlock = null;                //����Ʈ������ ó���ϰ� ť�� ���� ��쿡 �ߺ� üũ���� ���(ť���� ���� ù��° ���)

            bool bUseQueue = true; //true: ť���� ����, false: ����Ʈ���� ����
            while(true)
            {
                //1. Queue���� ���� �ϳ� ������. ù��° �ĺ��̴�.
                BlockVectorKV blockInfo = NextBlock(bUseQueue);
                Block block = blockInfo.Key;

                //2. ����Ʈ���� ���� ���� ó���� ��� : ��ü ����(for �� ����)���� 1ȸ�� �߻�
                if(block ==null)
                {
                    blockInfo = NextBlock(true);
                    block = blockInfo.Key;
                }

                Debug.Assert(block != null, $"block can't be null : queue count -> {m_UnusedBlocks.Count}");

                if (prevBreed == BlockBreed.NA)  //ù�񱳽� ���� ����
                    prevBreed = block.breed;

                //3. ����Ʈ�� ���ó���� ���
                if(m_bListComplete)
                {
                    if(firstBlock == null)
                    {
                        //3.1 ��ü ����Ʈ�� ó���ϰ�, ó������ ť���� ���� ���
                        firstBlock = block;
                    }
                    else if(System.Object.ReferenceEquals(firstBlock, block))
                    {
                        //3.2 ó�� ���Ҵ� ���� �ٽ� ó���ϴ� ���,
                        //��, ť�� ����ִ� ��� ���� ���ǿ� ���� �ʴ� ���(���� �� �߿� ���ǿ� �´°� ���� ���)
                        m_Board.ChangeBlock(block, prevBreed);
                    }
                }

                //4. �����¿����� ���� ��ġ�� ������ ���
                Vector2Int vtDup = CalcDuplications(nRow, nCol, block);

                //5. 2�� �̻� ��ġ�Ǵ� ���, ���� ��ġ�� �ش� ���� �� �� �����Ƿ� ť�� �����ϰ� ���� �� ó���ϵ��� Continue�Ѵ�.
                if(vtDup.x>2 || vtDup.y>2)
                {
                    m_UnusedBlocks.Enqueue(blockInfo);
                    bUseQueue = m_bListComplete || !bUseQueue;

                    continue;
                }

                //6. ���� ��ġ�� ���ִ� ���, ã�� ��ġ�� Block GameObject�� �̵���Ų��.
                block.vertDuplicate = vtDup.y;
                block.horzDuplicate = vtDup.x;
                if(block.blockObj != null)
                {
                    float initX = m_Board.CalcInitX(Constants.BLOCK_ORG);
                    float initY = m_Board.CalcInitX(Constants.BLOCK_ORG);
                    block.Move(initX + nCol, initY + nRow);
                }

                //7. ã�� ���� ����
                return block;
                    
            }
        }

        //�����¿� ���� ���� �ܺ�� ������ ���

        Vector2Int CalcDuplications(int nRow, int nCol, Block block)
        {
            int colDup = 1, rowDup = 1;

            if (nCol > 0 && m_Board.blocks[nRow, nCol - 1].IsSafeEqual(block))
                colDup += m_Board.blocks[nRow, nCol - 1].horzDuplicate;

            if (nRow > 0 && m_Board.blocks[nRow-1, nCol].IsSafeEqual(block))
                rowDup += m_Board.blocks[nRow-1, nCol].vertDuplicate;

            if(nCol<m_Board.maxCol -1 && m_Board.blocks[nRow, nCol+1].IsSafeEqual(block))
            {
                Block rightBlock = m_Board.blocks[nRow, nCol + 1];
                colDup += rightBlock.horzDuplicate;

                //���� �̴����� ���� ���� �ߺ��Ǵ� ���, ���ù̴�� ���� �ߺ� ������ �Բ� ������Ʈ �Ѵ�
                if (rightBlock.horzDuplicate == 1)
                    rightBlock.horzDuplicate = 2;
            }

            if(nRow<m_Board.maxRow-1&&m_Board.blocks[nRow+1, nCol].IsSafeEqual(block))
            {
                Block upperBlock = m_Board.blocks[nRow + 1, nCol];
                rowDup += upperBlock.vertDuplicate;

                //���� �̴����� ���� ���� �ߺ��Ǵ� ���, ���ù̴�� ���� �ߺ� ������ �Բ� ������Ʈ �Ѵ�
                if (upperBlock.horzDuplicate == 1)
                    upperBlock.horzDuplicate = 2;
            }

            return new Vector2Int(colDup, rowDup);
        }
    }
}