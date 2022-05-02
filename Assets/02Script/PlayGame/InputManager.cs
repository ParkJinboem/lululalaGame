using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class InputManager
    {
        Transform m_Container;

#if UNITY_ANDROID && !UNITY_EDITOR
        IInputHandlerBase m_InputHandler = new TouchHandler();
#else
        IInputHandlerBase m_InputHandler = new MouseHandler();
#endif
        public InputManager(Transform container)
        {
            m_Container = container;
        }

        public bool isTouchDown => m_InputHandler.isInputDown;
        public bool isTouchUp => m_InputHandler.isInputUp;
        public Vector2 touchPosition => m_InputHandler.inputPosition;
        public Vector2 touch2BoardPosition => TouchToPosition(m_InputHandler.inputPosition);    //Board의 원점을 기준으로 변환된 Local

        //Screen 좌표의 씬의 Board 기준 Local 좌표를 리턴한다.
        Vector2 TouchToPosition(Vector3 vtInput) //vtInput: Screen 좌표 즉, 픽셀 좌표
                                                //return: 기준 Local 좌표
        {
            //1. 스크린 좌표 -> 월드 좌표
            Vector3 vtMousePosW = Camera.main.ScreenToWorldPoint(vtInput);
            
            //2. 컨테이너 local 좌표계로 변환(컨테이너 위치 이동시에도 컨테이너 기준의 로컬 좌표계이므로 화면 구성이 유언하다)
            Vector3 vtContainerLocal = m_Container.transform.InverseTransformPoint(vtMousePosW);

            return vtContainerLocal;
        }

        public Swipe EvalSwiperDir(Vector2 vtStart, Vector2 vtEnd)
        {
            return TouchEvaluator.EvalSwipeDir(vtStart, vtEnd);
        }
    }
}