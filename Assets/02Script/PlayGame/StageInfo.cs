using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Board;

namespace Lulu.Stage
{
    [System.Serializable]   //JSON to Object ��ȯ�� �ǵ��� Serializable�� ��ü�� ����
    public class StageInfo
    {
        public int row;
        public int col;

        public int[] cells;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        //��û�� ��ġ(row,col)�� �ش�Ǵ� CellType�� �����ϴ� �޼ҵ�
        public CellType GetCellType(int nRow, int nCol)
        {
            Debug.Assert(cells != null && cells.Length > nRow * col + nCol);    //��û�� ��ġ�� ��ȿ���� üũ

            int revisedRow = (row - 1) - nRow;  //�� ���Ϲ���
            if (cells.Length > revisedRow * col + nCol)
                return (CellType)cells[revisedRow * col + nCol];


            Debug.Assert(false);

            return CellType.EMPTY;
        }

        //Json ������ ��ȿ�� �˻縦 �����ϴ� �޼ҵ�
        public bool DoValidation()
        {
            Debug.Assert(cells.Length == row * col);
            Debug.Log($"cell length : {cells.Length}, row, col = ({row}, {col})");

            if (cells.Length != row * col)  //��ũ��� �迭ũ�Ⱑ �ٸ� ��� false ����
                return false;

            return true;
        }
    }
}