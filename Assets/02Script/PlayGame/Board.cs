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
        //보드의 크기 정보를 저장하는 멤버 및 속성을 선언하고 정의
        int m_nRow;
        int m_nCol;

        public int maxRow { get { return m_nRow; } }
        public int maxCol { get { return m_nCol; } }

        //보드를 구성하는 Cell을 저장하는 2차원 배열을 선언
        Cell[,] m_Cells;
        public Cell[,] cells { get { return m_Cells; } }

        //보드를 구성하는 Block를 저장하는 2차원 배열을 선언
        Block[,] m_Blocks;
        public Block[,] blocks { get { return m_Blocks; } }

        //GameObject를 참조하기 위한 멤버를 선언
        Transform m_Container;
        GameObject m_CellPrefab;
        GameObject m_BlockPrefab;

        BoardEnumerator m_Enumerator;
        //생성자, 보드크기 정보를 저장하고 보드 크기만큼 저장할 수 있는 Cell과 Block 배열을 생성
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
            //1. 스테이지 구성에 필요한 Cell, Block, Container(Board) 정보를 저장한다.
            m_CellPrefab = cellPrefab;
            m_BlockPrefab = blockPrefab;
            m_Container = container;

            //2. 3매치된 블록이 없도록 섞는다.
            BoardShuffler shuffler = new BoardShuffler(this, true); //BoardShuffler 객체를 생성, 멤버로 저장되지않기 때문에 Shuffle을 수행하고 삭제됨
            shuffler.Shuffle();


            //3. Cell, Block Prefab을 이용해서 Board에 Cell/Block GameObject를 추가한다.
            float initX = CalcInitX(0.5f);
            float initY = CalcInitY(0.5f);
            for (int nRow = 0; nRow < m_nRow; nRow++)
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    //3.1 Cell GameObject 생성을 요청한다. GameObject가 생성되지 않는 경우에는 null을 리턴
                    Cell cell = m_Cells[nRow, nCol]?.InstantiateCellObj(cellPrefab, container);
                    cell?.Move(initX + nCol, initY + nRow);

                    //3.2 Block GameObject 생성을 요청한다.
                    Block block = m_Blocks[nRow, nCol]?.InstantateBlockObj(blockPrefab, container);
                    block?.Move(initX + nCol, initY + nRow);
                    //엘비스연산자 '?' : ?의 왼쪽 객체가 null이면 null을 리턴
                }
        }

        public IEnumerator Evaluate(Returnable<bool> matchResult)   //매칭된 블럭이 제거된다.
        {
            //1. 모든 블럭의 매칭 정보(개수, 상태, 내구도)를 계산한 후, 3매치 블럭이 있으면 true 리턴
            bool bbMatchBlockFound = UpdateAllBlockMatchedStatus();

            //2. 3매칭 블럭이 없는 경우 실행을 종료
            if (bbMatchBlockFound == false)
            {
                matchResult.value = false;
                yield break;
            }

            //3. 3매칭 블럭이 있는 경우

            //3.1 첫번째 phase
            // 매치된 블럭에 지정된 액션을 수행함
            // ex) 가로줄의 블럭 전체가 클리어 되는 블럭인 경우에 처리 등
            for (int nRow = 0; nRow < m_nRow; nRow++)
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    Block block = m_Blocks[nRow, nCol];

                    block?.DoEvaluation(m_Enumerator, nRow, nCol);
                }

            //3.2 두번째 phase
            // 첫 번째 phse에서 반영된 블럭의 상태값에 따라서 블럭의 최종 상태를 반영함

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

            //3.3 매칭된 블럭을 제거한다.
            clearBlocks.ForEach((block) => block.Destroy());

            //3.3.1 블럭이 제거되는 동안 잠시 Delay, 블럭 제거가 순식간에 일어나느 것에 약간 지연을 시킴
            yield return new WaitForSeconds(0.15f);

            //3.4 3매칭 블럭이 있는 경우 true 설정
            matchResult.value = true;

            yield break;
        }

        //전체 블럭 매칭 상태 업데이트
        public bool UpdateAllBlockMatchedStatus()
        {
            List<Block> matchedBlockList = new List<Block>();  // 블럭의 개수만큼 생성되는 것을 줄이기 위해 Caller에서 생선한 후에
                                                        // EvalBlocksIfMatched()의 인자로 전달한다(GC 최소화)
            int nCount = 0;
            for (int nRow = 0; nRow < m_nRow; nRow++)
            {
                for (int nCol = 0; nCol < m_nCol; nCol++)
                {
                    if (EvalBlocksIfMatched(nRow, nCol, matchedBlockList))   //전체 블럭에 대해서 블럭의 매치 정보 계산 함수를 호출한다.
                                                                             //3매치가 있으면 true 리턴된다.
                    {
                        nCount++;   //3매치가 발생한 개수를 카운트한다.
                    }
                }
            }
            return nCount > 0;  //매치가 있으면 true를 리턴한다.
        }

        //개별 블럭 매칭 상태 계산하기 - 지정된 row, col의 블럭이 Match 블럭인지 판단한다.
        public bool EvalBlocksIfMatched(int nRow, int nCol, List<Block> matchedBlockList)
        {
            bool bFound = false;

            Block baseBlock = m_Blocks[nRow, nCol];
            if (baseBlock == null)
                return false;

            if (baseBlock.match != Lulu.Quest.MatchType.NONE || !baseBlock.IsValidate() || m_Cells[nRow, nCol].IsObstracle())
                return false;
        

            //검사하는 자신을 매칭 리스트에 우선 보관한다.
            matchedBlockList.Add(baseBlock);

            //1. 가로 블럭 검색
            Block block;

            //1.1 오른쪽 방향
            for (int i = nCol + 1; i < m_nCol; i++)
            {
                block = m_Blocks[nRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }

            //1.2 왼쪽 방향
            for (int i = nCol - 1; i >= 0; i--)
            {
                block = m_Blocks[nRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }

            //1.3 매치된 상태인지 판단한다
            // 기준 블럭(baseBlock)을 제외하고 좌우에 2개이상이면 기준블럭 포함해서 3개이상 매치되는 경우로 판단할 수 있다.
            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, true);
                bFound = true;
            }

            matchedBlockList.Clear();

            //2. 세로 블럭 검색
            matchedBlockList.Add(baseBlock);

            //2.1 위쪽 검색
            for (int i = nRow + 1; i < m_nRow; i++)
            {
                block = m_Blocks[i, nCol];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }
            //2.2 아래쪽 검색
            for (int i = nRow - 1; i >= 0; i--)
            {
                block = m_Blocks[i, nCol];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }
            //2.3 매치된 상태인지 판단한다
            //  기준 블럭(baseBlock)을 제외하고 상하에 2개이상이면 기준블럭 포함해서 3개이상 매치되는 경우로 판단할 수 있다.
            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, false);
                bFound = true;
            }
            //계산위해 리스트에 저장한 블럭 제거
            matchedBlockList.Clear();

            return bFound;  //3매치 블럭이 있으면 true, 없으면 false 리턴
        }

        //리스트에 포함된 전체 블럭의 상태를 MATCH로 변경한다.
        void SetBlockStatusMatched(List<Block> blockList, bool bHorz)   //bHorz: 리스트에 보관된 블럭의 배치방향
                                                                        //가로방향이면 true, 세로바향이면 false
        {
            int nMatchCount = blockList.Count;
            blockList.ForEach(block => block.UpdateBlockStatusMatched((MatchType)nMatchCount)); //리스트에 보관에 전체 블럭에 대해서 상태 업데이트를 요청
        }

        //전체불럭의 구성을 재배치
        //비어있는 블럭을 위에 있는 블럭으로 채운다.
        // -MATCH 블럭이 제거된 후에 호출된다.

        public IEnumerator ArrangeBlocksAfterClean(List<IntIntKV> unfilledBlocks, List<Block> movingBlocks)
        {
            SortedList<int, int> emptyBlocks = new SortedList<int, int>();
            List<IntIntKV> emptyRemaingBlocks = new List<IntIntKV>();

            for (int nCol = 0; nCol < m_nCol; nCol++)
            {
                emptyBlocks.Clear();

                //1. 같은 열(col)에 빈 블럭을 수집한다.
                //현재 col의 다른 row의 비어있는 블럭 인덱스를 수집한다. sortedList이므로 첫번째 노드가 가장 아래쪽 블럭에 위치
                for (int nRow = 0; nRow < m_nRow; nRow++)
                {
                    if (CanBlockBeAllocatable(nRow, nCol))
                        emptyBlocks.Add(nRow, nRow);
                }

                //아래쪽에 비었는 블럭이 없는 경우
                if (emptyBlocks.Count == 0)
                    continue;

                //2. 이동이 가능한 블럭을 비어있는 하단 위치로 이동

                //2.1 가장 아래쪽부터 비어있는 블럭을 처리한다.
                IntIntKV first = emptyBlocks.First();

                //2.2 비어있는 블럭 위쪽 방향으로 이동 가능한 블럭을 탐색하면서 빈 블럭을 채워나감
                for (int nRow = first.Value + 1; nRow < m_nRow; nRow++)
                {
                    Block block = m_Blocks[nRow, nCol];

                    //2.2.1 이동 가능한 아이템이 아닌 경우 pass
                    if (block == null || m_Cells[nRow, nCol].type == CellType.EMPTY)//TODO EMPTY를 직접체크하지 않고 이러한 부류를 함수로 체크
                        continue;
                    //2.2.3 이동이 필요한 블럭 발견
                    block.dropDistance = new Vector2(0, nRow - first.Value);
                    movingBlocks.Add(block);

                    //2.2.4 빈공간으로 이동
                    Debug.Assert(m_Cells[first.Value, nCol].IsObstracle() == false, $"{m_Cells[first.Value, nCol]}");
                    m_Blocks[first.Value, nCol] = block;    //이동될 위치로 Board에서 저장된 위치 이동

                    //2.2.5 다른곳으로 이동했으므로 현재 위치는 비워둔다.
                    m_Blocks[nRow, nCol] = null;

                    //2.2.6 비어있는 블럭 리스트에서 사용된 첫번째 노드(first)를 삭제한다.
                    emptyBlocks.RemoveAt(0);

                    //2.2.7 현재 위치의 블럭이 다른 위치로 이동했으므로 현재 위치가 비어있게 된다.
                    //그러므로 비어잇는 블럭을 보관하는 emptyBlocks에 추가한다.
                    emptyBlocks.Add(nRow, nRow);

                    //2.2.8 다음(Next) 비어있는 블럭을 처리하도록 기준을 변경한다
                    first = emptyBlocks.First();
                    nRow = first.Value; //Note : 빈곳 바로 위부터 처리하도록 위치 조정, for문에서 nRow++ 하기 때문에 +1을 하지 않는다
                }
            }
            yield return null;

            //드롭으로 채워지지 않는 블럭이 있는 경우(왼쪽 아래 순으로 들어있음)
            if(emptyRemaingBlocks.Count>0)
            {
                unfilledBlocks.AddRange(emptyRemaingBlocks);
            }

            yield break;
        }

        //지정된 위치에 블럭이 새로 할당될수 있는지 체크하는 함수
        bool CanBlockBeAllocatable(int nRow, int nCol)
        {
            if (!m_Cells[nRow, nCol].type.IsBlockAllocatableType())
                return false;

            return m_Blocks[nRow, nCol] == null;
        }

        //퍼즐의 시작 X 위치를 구한다. left - top 좌표
        public float CalcInitX(float offset = 0)
        {
            return -m_nCol / 2.0f + offset;
        }
        //퍼즐의 시작 Y 위치, left - bottom 좌표
        //하단이 (0,0) 이므로
        public float CalcInitY(float offset = 0)
        {
            return -m_nRow / 2.0f + offset;
        }

        //주어진 위치의 블럭이 셔플 가능한지 즉, 셔플 대상인지 검사하는 메소드
        public bool CanShuffle(int nRow, int nCol, bool bLoading)   //bLoading: 메소드가 호출되는 단계를 나타낸다.
                                                                    //플레이 시작할때 호출되면 true가 전달됨
        {
            //지정된 위치에 블럭이 이동가능한지 CellType으로 판단
            if (!m_Cells[nRow, nCol].type.IsBlockMovableType())
            {
                return false;
            }

            return true;
        }

        public void ChangeBlock(Block block, BlockBreed notAllowedBreed)
        {
            BlockBreed genBreed;

            while (true) //notAllowedBreed와 중복되지 않도록 Breed를 랜덤하게 설정
            {
                genBreed = (BlockBreed)UnityEngine.Random.Range(0, 3);

                if (notAllowedBreed == genBreed)
                    continue;

                break;
            }

            block.breed = genBreed; //새로 생성된 Breed를 블럭에 설정
        }

        public bool IsSwipeable(int nRow, int nCol)
        {
            return m_Cells[nRow, nCol].type.IsBlockMovableType();
        }
    }
}