using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Quest;

namespace Lulu.Board
{
    public class Block
    {
        public BlockStatus status;                  //블럭 상태(NORMAL, MATCH, CLEAR)
        public BlockQuestType questType;            //블럭 퀘스트 타입

        public MatchType match = MatchType.NONE;    //MatchType, Evaluation동안 블럭 상태 계산에 사용

        public short matchCount;                    //연속된 블럭 개수, Evaluation동안 블럭상태 계산에 사용된다.

        BlockType m_BlockType;
        public BlockType type
        {
            get { return m_BlockType; }
            set { m_BlockType = value; }
        }


        protected BlockBreed m_Breed;
        public BlockBreed breed
        {
            get { return m_Breed; }
            set
            {
                m_Breed = value;
                m_BlockBehaviour?.UpdateView(true);
            }
        }

        protected BlockBehaviour m_BlockBehaviour;
        public BlockBehaviour blockBehaviour
        {
            get { return m_BlockBehaviour; }
            set
            {
                m_BlockBehaviour = value;
                m_BlockBehaviour.SetBlock(this);
            }
        }

        //증복블럭 셔플
        public Transform blockObj { get { return m_BlockBehaviour?.transform; } }   //Block에 연결된 GameObject의 Transform을 구함

        Vector2Int m_vtDuplicate;   //블럭 중복 개수, Shuffle시에 중복검사에 사용
        public int horzDuplicate    //가로방향 중복 검사시 사용
        {
            get { return m_vtDuplicate.x; }
            set { m_vtDuplicate.x = value; }
        }
        public int vertDuplicate    //세로방향 중복 검사시 사용
        {
            get { return m_vtDuplicate.y; }
            set { m_vtDuplicate.y = value; }
        }

        int m_nDurability;  //내구도가 0이되면 제거(블럭 내구도)    
        //3매치 마다 내구도가 1씩 감소하며 내구도가 '0'이되면 블럭이 제거
        public virtual int durability
        {
            get { return m_nDurability; }
            set { m_nDurability = value; }
        }


        protected BlockActionBehaviour m_BlockActionBehaviour;

        public bool isMoving    //블럭이 애니메이션 중인지 검사하는 속성
        {
            get
            {
                return blockObj != null && m_BlockActionBehaviour.isMoving;
            }
        }
        public Vector2 dropDistance //Block GameObject가 주어진 위치로 이동하도록 요청하는 속성
        {
            set
            {
                m_BlockActionBehaviour?.MoveDrop(value);
            }
        }

        public Block(BlockType blokcType)
        {
            m_BlockType = blokcType;

            status = BlockStatus.NORMAL;
            questType = BlockQuestType.CLEAR_SIMPLE;
            match = MatchType.NONE;
            m_Breed = BlockBreed.NA;

            m_nDurability = 1;
        }

        internal Block InstantiateBlockObj(GameObject blockPrefab, Transform containerObj)
        {
            //유효하지 않은 블럭인 경우 Block GameObject를 생성하지 않는다.
            if (IsValidate() == false)
                return null;

            //1. Block 오브젝트를 생성한다.
            GameObject newObj = Object.Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //2. 컨테이너(Board)의 차일드로 Block를 포함시킨다.
            newObj.transform.parent = containerObj;

            //3. Block 오브젝트에 적용된 BlockBehaviour 컴포넌트를 보관한다.
            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();

            //Block Game Object 생성 시에 BlockActionBehaviour 컴포넌트에 대한 참조를 설정한다.
            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();
            m_BlockActionBehaviour = newObj.transform.GetComponent<BlockActionBehaviour>();

            return this;
        }

        //블럭의 컨텍스트(종류, 상태, 카운트, 퀘스트조건 등)에 해당하는 동작을 수행하도록 한다.
        // return 특수블럭이면 true, 그렇지 않으면 fasle
        public bool DoEvaluation(BoardEnumerator boardEnumerator, int nRow, int nCol)
        {
            //매칭 로직 임시 적용, Quest 처리 별도 클래스 분리 필요
            Debug.Assert(boardEnumerator != null, $"({nRow},{nCol})");

            if (!IsEvaluatable())
                return false;

            //1. 매치 상태(클리어 조건 충족)인 경우
            if (status == BlockStatus.MATCH)
            {
                if (questType == BlockQuestType.CLEAR_SIMPLE || boardEnumerator.IsCageTypeCell(nRow, nCol))  //TODO cagetype cell 조건이 필요한가?
                {
                    Debug.Assert(m_nDurability > 0, $"durability is zero : {m_nDurability}");

                    //보드에 블럭 클리어 이벤트를 전달한다.
                    //블럭 클리어 후에 보드에 미치는 영향을 반영한다.
                    //if (boardEnumerator.SendMessageToBoard(BlockStatus.CLEAR, nRow, nCol))
                    durability--;
                }
                else  //특수블럭인 경우 true 리턴
                {
                    return true;
                }

                if (m_nDurability == 0)  //내구도가 '0'이 되면 블럭 상태를 CLEAR로 설정한다.
                {
                    status = BlockStatus.CLEAR;
                    return false;
                }
            }

            //2. 클리어 조건에 아직 도달하지 않는 경우 NORMAL 상태로 복귀
            status = BlockStatus.NORMAL;
            match = MatchType.NONE;
            matchCount = 0;

            return false;
        }
        public void UpdateBlockStatusMatched(MatchType matchType, bool bAccumulate = true)      //블럭 매칭 상태 업데이트
        {
            this.status = BlockStatus.MATCH;

            if (match == MatchType.NONE) //매치상태가 아닌 블럭인 경우, 
            {
                this.match = matchType;
            }
            else                        //기존에 매치 상태인 경우 누적(Accumulate)모드이면 Add하고 그렇지 않으면 새로운 매치타입으로 대치
            {
                this.match = bAccumulate ? match.Add(matchType) : matchType;    //match + matchType
            }

            matchCount = (short)matchType;  //매치 개수를 업데이트 한다.
        }

        /// <summary>
        ///  지정된 위치로 block GameObject위 위치(position)을 변경한다.
        /// </summary>
        /// <param name="x"> X좌표 : 씬기준</param>
        /// <param name="y"> Y좌표 : 씬기준</param>

        internal void Move(float x, float y)
        {
            blockBehaviour.transform.position = new Vector3(x, y);
        }

        public void MoveTo(Vector3 to, float duration)
        {
            m_BlockBehaviour.StartCoroutine(Util.Action2D.MoveTo(blockObj, to, duration));
        }

        public virtual void Destroy()       //매칭된 블럭 제거
        {
            Debug.Assert(blockObj != null, $"{match}");
            blockBehaviour.DoActionClear();
        }

        /// <summary>
        /// 유효한 브럭인지 체크한다.
        /// EMPTY 타입을 제외하고 모든 블럭이 유효한 것으로 간주
        /// Block GameObject 생성 등의 판단에 사용된다.
        /// </summary>
        /// <returns></returns>

        public bool IsValidate()
        {
            return type != BlockType.EMPTY;
        }

        public void ResetDuplicationInfo()   //증복개수를 0으로 리셋
        {
            m_vtDuplicate.x = 0;
            m_vtDuplicate.y = 0;
        }

        /// <summary>
        /// target Block과 같은 breed를 가지고 있는지 검사한다.
        /// </summary>
        /// <param name="target">비교할 대상 Block</param>
        /// <returns>breed가 같으면 true, 다르면 false</returns>
        public bool IsEqual(Block target)   //같은 종류의 블럭인지 비교
        {
            if (IsMatchableBlock() && this.breed == target.breed)
                return true;

            return false;
        }
        // 다른 블록과 매칭이 가능한 블럭인지 검사
        // 3매치 대상이 되는 블럭인지 검사
        // 모든 블럭이 3매치 대상이 되는것은 아님, 장애물 블럭과 같이 제거되지 않는 블럭이 있을 수 있다.
        public bool IsMatchableBlock()
        {
            return !(type == BlockType.EMPTY);
        }

        /*
      * swipe 가능한 블럭인지 체크한다
      * @param baseBlock 스와이프 기준 블럭, 기준블럭 종류에 따라서 가능 여부가 달라진다    
      */
        public bool IsSwipeable(Block baseBlock)
        {
            return true;
        }


        /*
         * Evaluation 대상에 적합한지 체크한다
         */
        public bool IsEvaluatable()     //Board에 상태 조회함수 추가
        {
            //이미 처리완료(CLEAR) 되었거나, 현재 처리중인 블럭인 경우
            if (status == BlockStatus.CLEAR || !IsMatchableBlock())
                return false;

            return true;
        }
    }
}

