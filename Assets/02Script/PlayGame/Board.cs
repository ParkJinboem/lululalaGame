using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Util;
using Lulu.Quest;


namespace Lulu.Board
{
    using IntIntKV = KeyValuePair<int, int>;
    public class Board
    {
        //������ ũ�� ������ �����ϴ� ��� �� �Ӽ��� �����ϰ� ����
        int m_nRow;
        int m_nCol;

        public int maxRow { get { return m_nRow; } }
        public int maxCol { get { return m_nCol; } }

        //���带 �����ϴ� Cell�� �����ϴ� 2���� �迭�� ����
        Cell[,] m_Cells;
        public Cell[,] cells { get { return m_Cells; } }

        //���带 �����ϴ� Block�� �����ϴ� 2���� �迭�� ����
        Block[,] m_Blocks;
        public Block[,] blocks { get { return m_Blocks; } }

        //GameObject�� �����ϱ� ���� ����� ����
        Transform m_Container;
        GameObject m_CellPrefab;
        GameObject m_BlockPrefab;

        BoardEnumerator m_Enumerator;
        //������, ����ũ�� ������ �����ϰ� ���� ũ�⸸ŭ ������ �� �ִ� Cell�� Block �迭�� ����
        public Board(int nRow, int nCol)
        {
            m_nRow = nRow;
            m_nCol = nCol;

            m_Cells = new Cell[nRow, nCol];
            m_Blocks = new Block[nRow, nCol];

            m_Enumerator = new BoardEnumerator(this);
        }



        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            //1. �������� ������ �ʿ��� Cell, Block, Container(Board) ������ �����Ѵ�.
            m_CellPrefab = cellPrefab;
            m_BlockPrefab = blockPrefab;
            m_Container = container;

            //2. 3��ġ�� ����� ������ ���´�.
            BoardShuffler shuffler = new BoardShuffler(this, true); //BoardShuffler ��ü�� ����, ����� ��������ʱ� ������ Shuffle�� �����ϰ� ������
            shuffler.Shuffle();


            //3. Cell, Block Prefab�� �̿��ؼ� Board�� Cell/Block GameObject�� �߰��Ѵ�.
            float initX = CalcInitX(0.5f);
            float initY = CalcInitY(0.5f);
            for (int nRow = 0; nRow < m_nRow; nRow++)
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    //3.1 Cell GameObject ������ ��û�Ѵ�. GameObject�� �������� �ʴ� ��쿡�� null�� ����
                    Cell cell = m_Cells[nRow, nCol]?.InstantiateCellObj(cellPrefab, container);
                    cell?.Move(initX + nCol, initY + nRow);

                    //3.2 Block GameObject ������ ��û�Ѵ�.
                    Block block = m_Blocks[nRow, nCol]?.InstantateBlockObj(blockPrefab, container);
                    block?.Move(initX + nCol, initY + nRow);
                    //���񽺿����� '?' : ?�� ���� ��ü�� null�̸� null�� ����
                }
        }

        public IEnumerator Evaluate(Returnable<bool> matchResult)   //��Ī�� ���� ���ŵȴ�.
        {
            //1. ��� ���� ��Ī ����(����, ����, ������)�� ����� ��, 3��ġ ���� ������ true ����
            bool bbMatchBlockFound = UpdateAllBlockMatchedStatus();

            //2. 3��Ī ���� ���� ��� ������ ����
            if (bbMatchBlockFound == false)
            {
                matchResult.value = false;
                yield break;
            }

            //3. 3��Ī ���� �ִ� ���

            //3.1 ù��° phase
            // ��ġ�� ���� ������ �׼��� ������
            // ex) �������� �� ��ü�� Ŭ���� �Ǵ� ���� ��쿡 ó�� ��
            for (int nRow = 0; nRow < m_nRow; nRow++)
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    Block block = m_Blocks[nRow, nCol];

                    block?.DoEvaluation(m_Enumerator, nRow, nCol);
                }

            //3.2 �ι�° phase
            // ù ��° phse���� �ݿ��� ���� ���°��� ���� ���� ���� ���¸� �ݿ���

            List<Block> clearBlocks = new List<Block>();

            for (int nRow = 0; nRow < m_nRow; nRow++)
            {
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    Block block = m_Blocks[nRow, nCol];

                    if (block != null)
                    {
                        if (block.status == BlockStatus.CLEAR)
                        {
                            clearBlocks.Add(block);

                            m_Blocks[nRow, nCol] = null;
                        }
                    }
                }
            }

            //3.3 ��Ī�� ���� �����Ѵ�.
            clearBlocks.ForEach((block) => block.Destroy());

            //3.3.1 ���� ���ŵǴ� ���� ��� Delay, �� ���Ű� ���İ��� �Ͼ�� �Ϳ� �ణ ������ ��Ŵ
            yield return new WaitForSeconds(0.15f);

            //3.4 3��Ī ���� �ִ� ��� true ����
            matchResult.value = true;

            yield break;
        }

        //��ü �� ��Ī ���� ������Ʈ
        public bool UpdateAllBlockMatchedStatus()
        {
            List<Block> matchedBlockList = new List<Block>();  // ���� ������ŭ �����Ǵ� ���� ���̱� ���� Caller���� ������ �Ŀ�
                                                        // EvalBlocksIfMatched()�� ���ڷ� �����Ѵ�(GC �ּ�ȭ)
            int nCount = 0;
            for (int nRow = 0; nRow < m_nRow; nRow++)
            {
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    if (EvalBlocksIfMatched(nRow, nCol, matchedBlockList))   //��ü ���� ���ؼ� ���� ��ġ ���� ��� �Լ��� ȣ���Ѵ�.
                                                                             //3��ġ�� ������ true ���ϵȴ�.
                    {
                        nCount++;   //3��ġ�� �߻��� ������ ī��Ʈ�Ѵ�.
                    }
                }
            }
            return nCount > 0;  //��ġ�� ������ true�� �����Ѵ�.
        }

        //���� �� ��Ī ���� ����ϱ� - ������ row, col�� ���� Match ������ �Ǵ��Ѵ�.
        public bool EvalBlocksIfMatched(int nRow, int nCol, List<Block> matchedBlockList)
        {
            bool bFound = false;

            Block baseBlock = m_Blocks[nRow, nCol];
            if (baseBlock == null)
                return false;

            if (baseBlock.match != Lulu.Quest.MatchType.NONE || !baseBlock.IsValidate() || m_Cells[nRow, nCol].IsObstracle())
                return false;
        

            //�˻��ϴ� �ڽ��� ��Ī ����Ʈ�� �켱 �����Ѵ�.
            matchedBlockList.Add(baseBlock);

            //1. ���� �� �˻�
            Block block;

            //1.1 ������ ����
            for (int i = nCol + 1; i < m_nCol; i++)
            {
                block = m_Blocks[nRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }

            //1.2 ���� ����
            for (int i = nCol - 1; i >= 0; i--)
            {
                block = m_Blocks[nRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }

            //1.3 ��ġ�� �������� �Ǵ��Ѵ�
            // ���� ��(baseBlock)�� �����ϰ� �¿쿡 2���̻��̸� ���غ� �����ؼ� 3���̻� ��ġ�Ǵ� ���� �Ǵ��� �� �ִ�.
            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, true);
                bFound = true;
            }

            matchedBlockList.Clear();

            //2. ���� �� �˻�
            matchedBlockList.Add(baseBlock);

            //2.1 ���� �˻�
            for (int i = nRow + 1; i < m_nRow; i++)
            {
                block = m_Blocks[i, nCol];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }
            //2.2 �Ʒ��� �˻�
            for (int i = nRow - 1; i >= 0; i--)
            {
                block = m_Blocks[i, nCol];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }
            //2.3 ��ġ�� �������� �Ǵ��Ѵ�
            //  ���� ��(baseBlock)�� �����ϰ� ���Ͽ� 2���̻��̸� ���غ� �����ؼ� 3���̻� ��ġ�Ǵ� ���� �Ǵ��� �� �ִ�.
            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, false);
                bFound = true;
            }
            //������� ����Ʈ�� ������ �� ����
            matchedBlockList.Clear();

            return bFound;  //3��ġ ���� ������ true, ������ false ����
        }

        //����Ʈ�� ���Ե� ��ü ���� ���¸� MATCH�� �����Ѵ�.
        void SetBlockStatusMatched(List<Block> blockList, bool bHorz)   //bHorz: ����Ʈ�� ������ ���� ��ġ����
                                                                        //���ι����̸� true, ���ι����̸� false
        {
            int nMatchCount = blockList.Count;
            blockList.ForEach(block => block.UpdateBlockStatusMatched((MatchType)nMatchCount)); //����Ʈ�� ������ ��ü ���� ���ؼ� ���� ������Ʈ�� ��û
        }

        //��ü�ҷ��� ������ ���ġ
        //����ִ� ���� ���� �ִ� ������ ä���.
        // -MATCH ���� ���ŵ� �Ŀ� ȣ��ȴ�.

        public IEnumerator ArrangeBlocksAfterClean(List<IntIntKV> unfilledBlocks, List<Block> movingBlocks)
        {
            SortedList<int, int> emptyBlocks = new SortedList<int, int>();
            List<IntIntKV> emptyRemaingBlocks = new List<IntIntKV>();

            for (int nCol = 0; nCol < m_nCol; nCol++)
            {
                emptyBlocks.Clear();

                //1. ���� ��(col)�� �� ���� �����Ѵ�.
                //���� col�� �ٸ� row�� ����ִ� �� �ε����� �����Ѵ�. sortedList�̹Ƿ� ù��° ��尡 ���� �Ʒ��� ���� ��ġ
                for (int nRow = 0; nRow < m_nRow; nRow++)
                {
                    if (CanBlockBeAllocatable(nRow, nCol))
                        emptyBlocks.Add(nRow, nRow);
                }

                //�Ʒ��ʿ� ����� ���� ���� ���
                if (emptyBlocks.Count == 0)
                    continue;

                //2. �̵��� ������ ���� ����ִ� �ϴ� ��ġ�� �̵�

                //2.1 ���� �Ʒ��ʺ��� ����ִ� ���� ó���Ѵ�.
                IntIntKV first = emptyBlocks.First();

                //2.2 ����ִ� �� ���� �������� �̵� ������ ���� Ž���ϸ鼭 �� ���� ä������
                for (int nRow = first.Value + 1; nRow < m_nRow; nRow++)
                {
                    Block block = m_Blocks[nRow, nCol];

                    //2.2.1 �̵� ������ �������� �ƴ� ��� pass
                    if (block == null || m_Cells[nRow, nCol].type == CellType.EMPTY)//TODO EMPTY�� ����üũ���� �ʰ� �̷��� �η��� �Լ��� üũ
                        continue;
                    //2.2.3 �̵��� �ʿ��� �� �߰�
                    block.dropDistance = new Vector2(0, nRow - first.Value);
                    movingBlocks.Add(block);

                    //2.2.4 ��������� �̵�
                    Debug.Assert(m_Cells[first.Value, nCol].IsObstracle() == false, $"{m_Cells[first.Value, nCol]}");
                    m_Blocks[first.Value, nCol] = block;    //�̵��� ��ġ�� Board���� ����� ��ġ �̵�

                    //2.2.5 �ٸ������� �̵������Ƿ� ���� ��ġ�� ����д�.
                    m_Blocks[nRow, nCol] = null;

                    //2.2.6 ����ִ� �� ����Ʈ���� ���� ù��° ���(first)�� �����Ѵ�.
                    emptyBlocks.RemoveAt(0);

                    //2.2.7 ���� ��ġ�� ���� �ٸ� ��ġ�� �̵������Ƿ� ���� ��ġ�� ����ְ� �ȴ�.
                    //�׷��Ƿ� ����մ� ���� �����ϴ� emptyBlocks�� �߰��Ѵ�.
                    emptyBlocks.Add(nRow, nRow);

                    //2.2.8 ����(Next) ����ִ� ���� ó���ϵ��� ������ �����Ѵ�
                    first = emptyBlocks.First();
                    nRow = first.Value; //Note : ��� �ٷ� ������ ó���ϵ��� ��ġ ����, for������ nRow++ �ϱ� ������ +1�� ���� �ʴ´�
                }
            }
            yield return null;

            //������� ä������ �ʴ� ���� �ִ� ���(���� �Ʒ� ������ �������)
            if(emptyRemaingBlocks.Count>0)
            {
                unfilledBlocks.AddRange(emptyRemaingBlocks);
            }

            yield break;
        }

        //������ ��ġ�� ���� ���� �Ҵ�ɼ� �ִ��� üũ�ϴ� �Լ�
        bool CanBlockBeAllocatable(int nRow, int nCol)
        {
            if (!m_Cells[nRow, nCol].type.IsBlockAllocatableType())
                return false;

            return m_Blocks[nRow, nCol] == null;
        }

        //������ ���� X ��ġ�� ���Ѵ�. left - top ��ǥ
        public float CalcInitX(float offset = 0)
        {
            return -m_nCol / 2.0f + offset;
        }
        //������ ���� Y ��ġ, left - bottom ��ǥ
        //�ϴ��� (0,0) �̹Ƿ�
        public float CalcInitY(float offset = 0)
        {
            return -m_nRow / 2.0f + offset;
        }

        //�־��� ��ġ�� ���� ���� �������� ��, ���� ������� �˻��ϴ� �޼ҵ�
        public bool CanShuffle(int nRow, int nCol, bool bLoading)   //bLoading: �޼ҵ尡 ȣ��Ǵ� �ܰ踦 ��Ÿ����.
                                                                    //�÷��� �����Ҷ� ȣ��Ǹ� true�� ���޵�
        {
            //������ ��ġ�� ���� �̵��������� CellType���� �Ǵ�
            if (!m_Cells[nRow, nCol].type.IsBlockMovableType())
            {
                return false;
            }

            return true;
        }

        public void ChangeBlock(Block block, BlockBreed notAllowedBreed)
        {
            BlockBreed genBreed;

            while (true) //notAllowedBreed�� �ߺ����� �ʵ��� Breed�� �����ϰ� ����
            {
                genBreed = (BlockBreed)UnityEngine.Random.Range(0, 3);

                if (notAllowedBreed == genBreed)
                    continue;

                break;
            }

            block.breed = genBreed; //���� ������ Breed�� ���� ����
        }

        public bool IsSwipeable(int nRow, int nCol)
        {
            return m_Cells[nRow, nCol].type.IsBlockMovableType();
        }
    }
}