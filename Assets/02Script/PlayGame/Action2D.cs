using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class Action2D
    {
        //지정된 시간동안 지정된 위치로 이동
        //@param target 애니메이션을 적용할 타겟 GameObject
        //@para to 이동할 목표 위치
        //@param duration 이동시간
        //parm bSelfRemove 애니메이션 종료 후 타겟 GameObject 삭제 여부 플래그

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


        //param toScale 커지는(줄어지는) 크기, 예를 들어, 0.5인 경우 현재 크기에서 절반으로 줄어든다.
        //param speed 초당 커지는 속도.예를 들어, 2인 경우 초당 2배 만큼 커지거나 줄어든다. 
        public static IEnumerator Scale(Transform target, float toScale, float speed)
        {

            //1. 방향 결정 : 커지는 방향이면 +, 줄어드는 방향이면 -
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

        //        //target.localPosition = new Vector3(currentPosition, currentPosition, 0);    //센터에서 대각선으로 흔들림
        //        //target.localPosition = new Vector3(target.position.x, target.position.y+7, 0);    //위치가 아예바뀜
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