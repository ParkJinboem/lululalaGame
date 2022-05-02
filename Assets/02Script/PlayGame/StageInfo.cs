using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Board;

namespace Lulu.Stage
{
    [System.Serializable]   //JSON to Object 변환이 되도록 Serializable한 객체로 선언
    public class StageInfo
    {
        public int row;
        public int col;

        public int[] cells;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        //요청한 위치(row,col)에 해당되는 CellType을 리턴하는 메소드
        public CellType GetCellType(int nRow, int nCol)
        {
            Debug.Assert(cells != null && cells.Length > nRow * col + nCol);    //요청한 위치가 유효한지 체크

            int revisedRow = (row - 1) - nRow;  //맵 상하반전
            if (cells.Length > revisedRow * col + nCol)
                return (CellType)cells[revisedRow * col + nCol];


            Debug.Assert(false);

            return CellType.EMPTY;
        }

        //Json 데이터 유효성 검사를 수행하는 메소드
        public bool DoValidation()
        {
            Debug.Assert(cells.Length == row * col);
            Debug.Log($"cell length : {cells.Length}, row, col = ({row}, {col})");

            if (cells.Length != row * col)  //블럭크기와 배열크기가 다른 경우 false 리턴
                return false;

            return true;
        }
    }
}