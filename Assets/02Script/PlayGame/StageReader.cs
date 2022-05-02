using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Stage
{
    public static class StageReader //스태틱 Class 선언
    {
        //StageInfo 객체를 리턴하는 LoadStage() 함수를 구현
        public static StageInfo LoadStage(int nStage)
        {
            Debug.Log($"Load Stage : Stage/{GetFileName(nStage)}");

            //1. 리소스파일에서 텍스트를 읽어온다
            TextAsset textAsset = Resources.Load<TextAsset>($"Stage/{GetFileName(nStage)}");

            if (textAsset != null)
            {
                //2. Json 문자열을 객체(StageInfo)로 변환
                StageInfo stageInfo = JsonUtility.FromJson<StageInfo>(textAsset.text);

                //3. 변환된 객체가 유효한지 체크한다(only Debuggin)
                Debug.Assert(stageInfo.DoValidation());

                return stageInfo;
            }

            return null;
        }

        //'stage_숫자네자리'로 구성된 파일이름을 리턴
        static string GetFileName(int nStage)
        {
            return string.Format("Stage_{0:D4}", nStage);
        }
    }
}