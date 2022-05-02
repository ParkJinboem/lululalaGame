using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Board
{
    public enum CellType
    {
        EMPTY = 0,  //빈공간, 블럭이 위치할수 없음 
        BASIC =1, //배경있는 기본 형
        FIXTURE=2, //고정된 장애물, 변화없음
        JELLY=3,    //젤리: 블럭이동 OK, 블록 CLEAR되면 BASIC 출력
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