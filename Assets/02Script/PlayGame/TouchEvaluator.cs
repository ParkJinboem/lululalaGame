using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    //스와이프 바향을 나타내는 Swipe enum 타입을 선언
    public enum Swipe
    {
        NA = -1,
        RIGHT = 0,
        UP = 1,
        LEFT = 2,
        DOWN = 3
    }

    public static class SwipeDirMethod
    {
        public static int GetTargetRow(this Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.DOWN: return -1; ;
                case Swipe.UP: return 1;
                default:
                    return 0;
            }
        }

        public static int GetTargetCol(this Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.LEFT: return -1; ;
                case Swipe.RIGHT: return 1;
                default:
                    return 0;
            }
        }
    }

    public static class TouchEvaluator
    {
        //두지점을 사용하여 Swipe 방향을 구한다.
        //UP: 45~135, LEFT: 135~225, DOWN: 225~315, RIGHT: 0~45, 0~315

        public static Swipe EvalSwipeDir(Vector2 vtStart, Vector2 vtEnd)
        {
            float angle = EvalDragAngle(vtStart, vtEnd);
            if (angle < 0)
                return Swipe.NA;

            int swipe = (((int)angle + 45) % 360) / 90;

            switch (swipe)
            {
                case 0: return Swipe.RIGHT;
                case 1: return Swipe.UP;
                case 2: return Swipe.LEFT;
                case 3: return Swipe.DOWN;
            }

            return Swipe.NA;    //Swipe enum 타입을 리턴
        }

        //두 포인트 사이의 각도를 구한다.
        //Input(마우스, 터치) 장치 드래그한 각도를 구하는데 활용한다.
        //return: 두 포인트 사이의 각도

        static float EvalDragAngle(Vector2 vtStart, Vector2 vtEnd)
        {
            Vector2 dragDirection = vtEnd - vtStart;
            if (dragDirection.magnitude <= 0.2f)
                return -1f;

            float aimAngle = Mathf.Atan2(dragDirection.y, dragDirection.x);
            if(aimAngle<0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            return aimAngle * Mathf.Rad2Deg;    //0~360 사이의 값을 리턴한다.

        }
    }
}