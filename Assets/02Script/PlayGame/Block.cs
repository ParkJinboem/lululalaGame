using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Quest;

namespace Lulu.Board
{
    public class Block
    {
        public BlockStatus status;                  //�� ����(NORMAL, MATCH, CLEAR)
        public BlockQuestType questType;            //�� ����Ʈ Ÿ��

        public MatchType match = MatchType.NONE;    //MatchType, Evaluation���� �� ���� ��꿡 ���

        public short matchCount;                    //���ӵ� �� ����, Evaluation���� ������ ��꿡 ���ȴ�.

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

        //������ ����
        public Transform blockObj { get { return m_BlockBehaviour?.transform; } }   //Block�� ����� GameObject�� Transform�� ����

        Vector2Int m_vtDuplicate;   //�� �ߺ� ����, Shuffle�ÿ� �ߺ��˻翡 ���
        public int horzDuplicate    //���ι��� �ߺ� �˻�� ���
        {
            get { return m_vtDuplicate.x; }
            set { m_vtDuplicate.x = value; }
        }
        public int vertDuplicate    //���ι��� �ߺ� �˻�� ���
        {
            get { return m_vtDuplicate.y; }
            set { m_vtDuplicate.y = value; }
        }

        int m_nDurability;  //�������� 0�̵Ǹ� ����(�� ������)    
        //3��ġ ���� �������� 1�� �����ϸ� �������� '0'�̵Ǹ� ���� ����
        public virtual int durability
        {
            get { return m_nDurability; }
            set { m_nDurability = value; }
        }


        protected BlockActionBehaviour m_BlockActionBehaviour;

        public bool isMoving    //���� �ִϸ��̼� ������ �˻��ϴ� �Ӽ�
        {
            get
            {
                return blockObj != null && m_BlockActionBehaviour.isMoving;
            }
        }
        public Vector2 dropDistance //Block GameObject�� �־��� ��ġ�� �̵��ϵ��� ��û�ϴ� �Ӽ�
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
            //��ȿ���� ���� ���� ��� Block GameObject�� �������� �ʴ´�.
            if (IsValidate() == false)
                return null;

            //1. Block ������Ʈ�� �����Ѵ�.
            GameObject newObj = Object.Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //2. �����̳�(Board)�� ���ϵ�� Block�� ���Խ�Ų��.
            newObj.transform.parent = containerObj;

            //3. Block ������Ʈ�� ����� BlockBehaviour ������Ʈ�� �����Ѵ�.
            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();

            //Block Game Object ���� �ÿ� BlockActionBehaviour ������Ʈ�� ���� ������ �����Ѵ�.
            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();
            m_BlockActionBehaviour = newObj.transform.GetComponent<BlockActionBehaviour>();

            return this;
        }

        //���� ���ؽ�Ʈ(����, ����, ī��Ʈ, ����Ʈ���� ��)�� �ش��ϴ� ������ �����ϵ��� �Ѵ�.
        // return Ư�����̸� true, �׷��� ������ fasle
        public bool DoEvaluation(BoardEnumerator boardEnumerator, int nRow, int nCol)
        {
            //��Ī ���� �ӽ� ����, Quest ó�� ���� Ŭ���� �и� �ʿ�
            Debug.Assert(boardEnumerator != null, $"({nRow},{nCol})");

            if (!IsEvaluatable())
                return false;

            //1. ��ġ ����(Ŭ���� ���� ����)�� ���
            if (status == BlockStatus.MATCH)
            {
                if (questType == BlockQuestType.CLEAR_SIMPLE || boardEnumerator.IsCageTypeCell(nRow, nCol))  //TODO cagetype cell ������ �ʿ��Ѱ�?
                {
                    Debug.Assert(m_nDurability > 0, $"durability is zero : {m_nDurability}");

                    //���忡 �� Ŭ���� �̺�Ʈ�� �����Ѵ�.
                    //�� Ŭ���� �Ŀ� ���忡 ��ġ�� ������ �ݿ��Ѵ�.
                    //if (boardEnumerator.SendMessageToBoard(BlockStatus.CLEAR, nRow, nCol))
                    durability--;
                }
                else  //Ư������ ��� true ����
                {
                    return true;
                }

                if (m_nDurability == 0)  //�������� '0'�� �Ǹ� �� ���¸� CLEAR�� �����Ѵ�.
                {
                    status = BlockStatus.CLEAR;
                    return false;
                }
            }

            //2. Ŭ���� ���ǿ� ���� �������� �ʴ� ��� NORMAL ���·� ����
            status = BlockStatus.NORMAL;
            match = MatchType.NONE;
            matchCount = 0;

            return false;
        }
        public void UpdateBlockStatusMatched(MatchType matchType, bool bAccumulate = true)      //�� ��Ī ���� ������Ʈ
        {
            this.status = BlockStatus.MATCH;

            if (match == MatchType.NONE) //��ġ���°� �ƴ� ���� ���, 
            {
                this.match = matchType;
            }
            else                        //������ ��ġ ������ ��� ����(Accumulate)����̸� Add�ϰ� �׷��� ������ ���ο� ��ġŸ������ ��ġ
            {
                this.match = bAccumulate ? match.Add(matchType) : matchType;    //match + matchType
            }

            matchCount = (short)matchType;  //��ġ ������ ������Ʈ �Ѵ�.
        }

        /// <summary>
        ///  ������ ��ġ�� block GameObject�� ��ġ(position)�� �����Ѵ�.
        /// </summary>
        /// <param name="x"> X��ǥ : ������</param>
        /// <param name="y"> Y��ǥ : ������</param>

        internal void Move(float x, float y)
        {
            blockBehaviour.transform.position = new Vector3(x, y);
        }

        public void MoveTo(Vector3 to, float duration)
        {
            m_BlockBehaviour.StartCoroutine(Util.Action2D.MoveTo(blockObj, to, duration));
        }

        public virtual void Destroy()       //��Ī�� �� ����
        {
            Debug.Assert(blockObj != null, $"{match}");
            blockBehaviour.DoActionClear();
        }

        /// <summary>
        /// ��ȿ�� �귰���� üũ�Ѵ�.
        /// EMPTY Ÿ���� �����ϰ� ��� ���� ��ȿ�� ������ ����
        /// Block GameObject ���� ���� �Ǵܿ� ���ȴ�.
        /// </summary>
        /// <returns></returns>

        public bool IsValidate()
        {
            return type != BlockType.EMPTY;
        }

        public void ResetDuplicationInfo()   //���������� 0���� ����
        {
            m_vtDuplicate.x = 0;
            m_vtDuplicate.y = 0;
        }

        /// <summary>
        /// target Block�� ���� breed�� ������ �ִ��� �˻��Ѵ�.
        /// </summary>
        /// <param name="target">���� ��� Block</param>
        /// <returns>breed�� ������ true, �ٸ��� false</returns>
        public bool IsEqual(Block target)   //���� ������ ������ ��
        {
            if (IsMatchableBlock() && this.breed == target.breed)
                return true;

            return false;
        }
        // �ٸ� ��ϰ� ��Ī�� ������ ������ �˻�
        // 3��ġ ����� �Ǵ� ������ �˻�
        // ��� ���� 3��ġ ����� �Ǵ°��� �ƴ�, ��ֹ� ���� ���� ���ŵ��� �ʴ� ���� ���� �� �ִ�.
        public bool IsMatchableBlock()
        {
            return !(type == BlockType.EMPTY);
        }

        /*
      * swipe ������ ������ üũ�Ѵ�
      * @param baseBlock �������� ���� ��, ���غ� ������ ���� ���� ���ΰ� �޶�����    
      */
        public bool IsSwipeable(Block baseBlock)
        {
            return true;
        }


        /*
         * Evaluation ��� �������� üũ�Ѵ�
         */
        public bool IsEvaluatable()     //Board�� ���� ��ȸ�Լ� �߰�
        {
            //�̹� ó���Ϸ�(CLEAR) �Ǿ��ų�, ���� ó������ ���� ���
            if (status == BlockStatus.CLEAR || !IsMatchableBlock())
                return false;

            return true;
        }
    }
}

