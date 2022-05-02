using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lulu.Board;

namespace Lulu.Board
{
    public class CellBehaviour : MonoBehaviour
    {
        Cell m_Cell;
        SpriteRenderer m_SpriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            UpdateView(false);
        }

        public void SetCell(Cell cell)
        {
            m_Cell = cell;
        }

        public void UpdateView(bool bValueChanged)
        {
            if (m_Cell.type == CellType.EMPTY)
            {
                m_SpriteRenderer.sprite = null; //cell 종류가 EMPTY인경우 Sprite가 보이지 않도록 한다.
            }
        }
    }
}