using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Board
{
    public enum CellType
    {
        EMPTY = 0,  //�����, ���� ��ġ�Ҽ� ���� 
        BASIC =1, //����ִ� �⺻ ��
        FIXTURE=2, //������ ��ֹ�, ��ȭ����
        JELLY=3,    //����: ���̵� OK, ��� CLEAR�Ǹ� BASIC ���
    }

    static class CelltypeMethod
    {
        public static bool IsBlockAllocatableType(this CellType cellType)
        {
            return !(cellType == CellType.EMPTY);
        }

        public static bool IsBlockMovableType(this CellType cellType)
        {
            return !(cellType == CellType.EMPTY);
        }
    }
}