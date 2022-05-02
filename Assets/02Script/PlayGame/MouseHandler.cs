using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{

    public class MouseHandler : IInputHandlerBase   //IInputHandlerBase 인터페이스를 구현할 클래스를 선언
    {
        bool IInputHandlerBase.isInputDown => Input.GetButtonDown("Fire1"); //마우스 왼쪽버튼이 DOWN상태이면 true
        bool IInputHandlerBase.isInputUp => Input.GetButtonUp("Fire1");     //마우스 왼쪽버튼이 UP상태이면 true

        Vector2 IInputHandlerBase.inputPosition => Input.mousePosition;     //마우스 위치(Screen 좌표)를 리턴한다.

    }
}