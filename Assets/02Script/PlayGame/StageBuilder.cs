using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lulu.Board;

namespace Lulu.Stage
{
    public class StageBuilder
    {
        int m_nStage;
        StageInfo m_StageInfo;  //Json���� ���� �ε��� �������� ������ ����ִ� StageInfo ���
        
        public StageBuilder(int nStage)
        {
            m_nStage = nStage;
        }

        public static Stage BuildStage(int nStage)    //���� �������� ����
        {
            StageBuilder stageBuilder = new StageBuilder(nStage);
            Stage stage = stageBuilder.ComposeStage();

            return stage;
        }

        public Stage ComposeStage()
        {
            Debug.Assert(m_nStage > 0, $"Invalide Stage : {m_nStage}"); //�������� ��ȣ�� ��ȿ���� �˻�

            //0. �������� ������ �ε��Ѵ�(���� ũ��, Cell/Block ���� ��)
            m_StageInfo = LoadStage(m_nStage);
            //1. stage ��ü�� ����
            Stage stage = new Stage(this, m_StageInfo.row, m_StageInfo.col);    //StageInfo�� ����� ���� ����ϵ��� ����

            //2. Cell,Block �ʱ� ���� �����Ѵ�.
            for (int nRow = 0; nRow< m_StageInfo.row; nRow++)
            {
                for(int nCol = 0; nCol< m_StageInfo.col; nCol++)
                {
                    stage.blocks[nRow, nCol] = SpawnBlockForStage(nRow, nCol);
                    stage.cells[nRow, nCol] = SpawnCellForStage(nRow, nCol);
                }
            }
            return stage;
        }

        //������ �߰��� StageReader���� �������� ������ �ε� �ؼ� StageInfo�� ���Ѵ�.
        public StageInfo LoadStage(int nStage)
        {
            StageInfo stageInfo = StageReader.LoadStage(nStage);
            if(stageInfo != null)
            {
                Debug.Log(stageInfo.ToString());
            }

            return stageInfo;
        }

        Block SpawnBlockForStage(int nRow, int nCol)    //��� ���� �������� ������ ����
        {
            if (m_StageInfo.GetCellType(nRow, nCol) == CellType.EMPTY)
                return SpawnEmptyBlock();

            return SpawnBlock();
        }
        Cell SpawnCellForStage(int nRow, int nCol)
        {
            //Debug.Assert() ==> ������ Ȯ����, ������ false�̸� �޽����� ���
            Debug.Assert(m_StageInfo != null);
            Debug.Assert(nRow < m_StageInfo.row && nCol < m_StageInfo.col);

            return CellFactory.SpawnCell(m_StageInfo, nRow, nCol);
        }
        public Block SpawnBlock()
        {
            return BlockFactory.SpawnBlock(BlockType.BASIC);
        }
        public Block SpawnEmptyBlock()
        {
            Block newBlock = BlockFactory.SpawnBlock(BlockType.EMPTY);

            return newBlock;
        }

       
    }
}
