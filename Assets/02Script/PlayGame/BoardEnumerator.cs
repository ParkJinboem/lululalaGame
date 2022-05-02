using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Board
{
    public class BoardEnumerator
    {
        Lulu.Board.Board m_Board;

        public BoardEnumerator(Lulu.Board.Board board)
        {
            this.m_Board = board;
        }
        public bool IsCageTypeCell(int nRow, int nCol)  //철창 타입의 Cell인지 검사
        {
            return false;
        }
    }
}
