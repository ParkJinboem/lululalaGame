using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Board
{
    public class Cell
    {
        protected CellType m_CellType;
        public CellType type
        {
            get { return m_CellType; }
            set { m_CellType = value; }
        }
        public Cell(CellType cellType)
        {
            m_CellType = cellType;
        }

        protected CellBehaviour m_CellBehaviour;
        public CellBehaviour cellBehaviour
        {
            get { return m_CellBehaviour; }
            set
            {
                m_CellBehaviour = value;
                m_CellBehaviour.SetCell(this);
            }
        }
        public Cell InstantiateCellObj(GameObject cellPrefab, Transform containerObj)
        {
            //1. Cell ������Ʈ�� ����
            GameObject newObj = Object.Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //2. �����̳�(Board)�� ���ϵ��Cell�� ���Խ�Ų��.
            newObj.transform.parent = containerObj;

            //3. Cell ������Ʈ�� ����� CellBehaviour ������Ʈ�� �����Ѵ�.
            this.cellBehaviour = newObj.transform.GetComponent<CellBehaviour>();

            return this;
        }

        public void Move(float x, float y)
        {
            cellBehaviour.transform.position = new Vector3(x, y);
        }

        public bool IsObstracle()
        {
            return type == CellType.EMPTY;
        }
    }

 
}
