using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class Action2D
    {
        //������ �ð����� ������ ��ġ�� �̵�
        //@param target �ִϸ��̼��� ������ Ÿ�� GameObject
        //@para to �̵��� ��ǥ ��ġ
        //@param duration �̵��ð�
        //parm bSelfRemove �ִϸ��̼� ���� �� Ÿ�� GameObject ���� ���� �÷���

        public static IEnumerator MoveTo(Transform target, Vector3 to, float duration, bool bSelfRemove = false)
        {
            Vector2 startPos = target.transform.position;

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                elapsed += Time.smoothDeltaTime;
                target.transform.position = Vector2.Lerp(startPos, to, elapsed / duration);

                yield return null;
            }

            target.transform.position = to;

            if (bSelfRemove)
                Object.Destroy(target.gameObject, 0.1f);

            yield break;
        }


        //param toScale Ŀ����(�پ�����) ũ��, ���� ���, 0.5�� ��� ���� ũ�⿡�� �������� �پ���.
        //param speed �ʴ� Ŀ���� �ӵ�.���� ���, 2�� ��� �ʴ� 2�� ��ŭ Ŀ���ų� �پ���. 
        public static IEnumerator Scale(Transform target, float toScale, float speed)
        {

            //1. ���� ���� : Ŀ���� �����̸� +, �پ��� �����̸� -
            bool blnc = target.localScale.x < toScale;
            float fDir = blnc ? 1 : -1;
            float factor;

            while (true)
            {
                factor = Time.deltaTime * speed * fDir;
                target.localScale = new Vector3(target.localScale.x + factor, target.localScale.y + factor, target.localScale.z);

                if ((!blnc && target.localScale.x <= toScale) || (blnc && target.localScale.x >= toScale))
                    break;

                yield return null;
            }
            yield break;
        }

        //public static IEnumerator Shake(Transform target, float delta, float speed, int count)
        //{

        //    float currentPosition = target.position.x;

        //    while (true)
        //    {
        //        currentPosition += Time.deltaTime * speed;

        //        if (currentPosition >= delta)
        //        {
        //            speed *= -1;
        //            currentPosition = delta;
        //            count++;
        //        }
        //        else if (currentPosition <= -delta)
        //        {
        //            speed *= -1;
        //            currentPosition = -delta;
        //            count++;
        //        }

        //        target.Translate(currentPosition, 0, 0);
        //        //target.position = new Vector3(target.position.x, target.position.y, target.position.z);

        //        //target.localPosition = new Vector3(currentPosition, currentPosition, 0);    //���Ϳ��� �밢������ ��鸲
        //        //target.localPosition = new Vector3(target.position.x, target.position.y+7, 0);    //��ġ�� �ƿ��ٲ�
        //        //target.scale = new Vector3(target.position.x, currentPosition, 0);
        //        //Debug.Log("CurrentPosition " + currentPosition);


        //        if (count > 10)
        //        {
        //            break;
        //        }

        //        yield return null;
        //    }
        //}

    }

}