using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Stage
{
    public static class StageReader //����ƽ Class ����
    {
        //StageInfo ��ü�� �����ϴ� LoadStage() �Լ��� ����
        public static StageInfo LoadStage(int nStage)
        {
            Debug.Log($"Load Stage : Stage/{GetFileName(nStage)}");

            //1. ���ҽ����Ͽ��� �ؽ�Ʈ�� �о�´�
            TextAsset textAsset = Resources.Load<TextAsset>($"Stage/{GetFileName(nStage)}");

            if (textAsset != null)
            {
                //2. Json ���ڿ��� ��ü(StageInfo)�� ��ȯ
                StageInfo stageInfo = JsonUtility.FromJson<StageInfo>(textAsset.text);

                //3. ��ȯ�� ��ü�� ��ȿ���� üũ�Ѵ�(only Debuggin)
                Debug.Assert(stageInfo.DoValidation());

                return stageInfo;
            }

            return null;
        }

        //'stage_���ڳ��ڸ�'�� ������ �����̸��� ����
        static string GetFileName(int nStage)
        {
            return string.Format("Stage_{0:D4}", nStage);
        }
    }
}