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

        //블럭이 폭발한 후, GameObject를 삭제
        IEnumerator CoStartSimpleExplosion(bool bDestroy = true)
        {
            //0. 크기가 줄어드는 액션 실행한다 : 폭파되면서 자연스럽게 소멸되는 모양 연출, 1 -> 0.3으로 줄어듬
            yield return Util.Action2D.Scale(transform, Core.Constants.BLOCK_DESTROY_SCALE, 4f);
            //1. 폭발시키는 효과 연출 : 블럭 자체의 Clear 효과를 연출한다. (모든블럭 동일)
            GameObject explosionObj = m_BlockConfig.GetExplosionObject(BlockQuestType.CLEAR_SIMPLE);
            ParticleSystem.MainModule newModuele = explosionObj.GetComponent<ParticleSystem>().main;
            newModuele.startColor = m_BlockConfig.GetBlockColor(m_Block.breed);

            explosionObj.SetActive(true);
            explosionObj.transform.position = this.transform.position;

            yield return new WaitForSeconds(0.1f);

            //2. 블럭 GameObject 객체 삭제
            if (bDestroy)
                Destroy(gameObject);
            else
            {
                Debug.Assert(false, "Unknown Action : GameObject No Destory After Particle");
            }

        }
    }
}