using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Board;

namespace Lulu.Scriptable
{
    [CreateAssetMenu(menuName = "Bingle/Block Config", fileName ="BlockConfig.asset")]
    public class BlockConfig : ScriptableObject
    {
        public float[] dropSpeed;
        public Sprite[] basicBlockSprites;
        public GameObject explosion;    //���� ���ŵ� �� ����� ��ƼŬ Prefab�� �����ϴ� GameObject
        

        public Color[] blockColors;

   

        public GameObject GetExplosionObject(BlockQuestType questType)
        {
            switch(questType)
            {
                case BlockQuestType.CLEAR_SIMPLE:
                    return Instantiate(explosion) as GameObject;
                default:
                    return Instantiate(explosion) as GameObject;
            }
        }

        public Color GetBlockColor(BlockBreed breed)
        {
            return blockColors[(int)breed];    
        }
    }
}