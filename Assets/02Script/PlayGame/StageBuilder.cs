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
        StageInfo m_StageInfo;  //Json으로 부터 로딩된 스테이지 정보를 담고있는 StageInfo 멤버
        
        public StageBuilder(int nStage)
        {
            m_nStage = nStage;
        }

        public static Stage BuildStage(int nStage)    //빌드 스테이지 설정
        {
            StageBuilder stageBuilder = new StageBuilder(nStage);
            Stage stage = stageBuilder.ComposeStage();

            return stage;
        }

        public Stage ComposeStage()
        {
            Debug.Assert(m_nStage > 0, $"Invalide Stage : {m_nStage}"); //스테이지 번호가 유효한지 검사

            //0. 스테이지 정보를 로드한다(보드 크기, Cell/Block 정보 등)
            m_StageInfo = LoadStage(m_nStage);
            //1. stage 객체를 생성
            Stage stage = new Stage(this, m_StageInfo.row, m_StageInfo.col);    //StageInfo에 저장된 값을 사용하도록 변경

            //2. Cell,Block 초기 값을 생성한다.
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

        //위에서 추가한 StageReader에게 스테이지 파일을 로드 해서 StageInfo를 구한다.
        public StageInfo LoadStage(int nStage)
        {
            StageInfo stageInfo = StageReader.LoadStage(nStage);
            if(stageInfo != null)
            {
                Debug.Log(stageInfo.ToString());
            }

            return stageInfo;
        }

        Block SpawnBlockForStage(int nRow, int nCol)    //행과 열이 같은곳은 빈블록을 생성
        {
            if (m_StageInfo.GetCellType(nRow, nCol) == CellType.EMPTY)
                return SpawnEmptyBlock();

            return SpawnBlock();
        }
        Cell SpawnCellForStage(int nRow, int nCol)
        {
            //Debug.Assert() ==> 조건을 확인함, 조건이 false이면 메시지를 출력
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
