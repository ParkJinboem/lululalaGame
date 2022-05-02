using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Board
{
    public enum BlockType
    {
        EMPTY = 0,
        BASIC = 1
    }
    public enum BlockBreed
    {
        NA =-1,
        BREED_0 =0,
        BREED_1 = 1,
        BREED_2 = 2,
    }
    public enum BlockStatus
    {
        NORMAL,     //�⺻����
        MATCH,      //��Ī �� �ִ� ����
        CLEAR       //Ŭ���� ���� ����
    }

    public enum BlockQuestType      //�� Ŭ���� �ߵ�ȿ��
    {
        NONE=-1,
        CLEAR_SIMPLE=0,             //���� �� ����
        CLEAR_HORZ=1,               //������ �� ���� ( ������ -1 ) -> 4 match ������
        CLEAR_VERT=2,               //������ �� ���� -> 4 match ������
        CLEAR_CIRCLE=3,             //������ �ֺ����� �� ���� -> T L ��ġ(3x3, 4x3)
        CLEAR_LAZER=4,              //������ ���� ������ �� ��ü ���� --> 5match
        CLEAR_HORZ_BUFF=5,          //HORZ + CIRCLE ����
        CLEAR_VERT_BUFF=6,          //VERT + CIRCLE ����
        CLEAR_CIRCLE_BUFF=7,        //CIRCLE + CIRCLE ����
        CLEAR_LAZER_BUFF=8          //LAZER + LAZER ����
    }

    static class BlockMethod
    {
        public static bool IsSafeEqual(this Block block, Block targetBlock)
        {
            if (block == null)
                return false;

            return block.IsEqual(targetBlock);
        }
    }
}
