using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Scriptable;

namespace Lulu.Board
{
    public class BlockBehaviour : MonoBehaviour
    {
        Block m_Block;
        SpriteRenderer m_spriteRenderer;
        [SerializeField] BlockConfig m_BlockConfig;
                
        // Start is called before the first frame update
        void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateView(false);
        }

        internal void SetBlock(Block block)
        {
            m_Block = block;
        }

        public void UpdateView(bool bValueChanged)
        {
            if(m_Block.type == BlockType.EMPTY)
            {
                m_spriteRenderer.sprite = null;
            }
            else if(m_Block.type == BlockType.BASIC)
            {
                m_spriteRenderer.sprite = m_BlockConfig.basicBlockSprites[(int)m_Block.breed];
            }
        }

        public void DoActionClear()
        {
            StartCoroutine(CoStartSimpleExplosion(true));
        }

        //���� ������ ��, GameObject�� ����
        IEnumerator CoStartSimpleExplosion(bool bDestroy = true)
        {
            //0. ũ�Ⱑ �پ��� �׼� �����Ѵ� : ���ĵǸ鼭 �ڿ������� �Ҹ�Ǵ� ��� ����, 1 -> 0.3���� �پ��
            yield return Util.Action2D.Scale(transform, Core.Constants.BLOCK_DESTROY_SCALE, 4f);
            //1. ���߽�Ű�� ȿ�� ���� : �� ��ü�� Clear ȿ���� �����Ѵ�. (���� ����)
            GameObject explosionObj = m_BlockConfig.GetExplosionObject(BlockQuestType.CLEAR_SIMPLE);
            ParticleSystem.MainModule newModuele = explosionObj.GetComponent<ParticleSystem>().main;
            newModuele.startColor = m_BlockConfig.GetBlockColor(m_Block.breed);

            explosionObj.SetActive(true);
            explosionObj.transform.position = this.transform.position;

            yield return new WaitForSeconds(0.1f);

            //2. �� GameObject ��ü ����
            if (bDestroy)
                Destroy(gameObject);
            else
            {
                Debug.Assert(false, "Unknown Action : GameObject No Destory After Particle");
            }

        }
    }
}