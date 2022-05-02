using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class TouchHandler : IInputHandlerBase
    {
        bool IInputHandlerBase.isInputDown => Input.GetTouch(0).phase == TouchPhase.Began;  //첫번째 터치 포인트가 눌리면 true
        bool IInputHandlerBase.isInputUp => Input.GetTouch(0).phase == TouchPhase.Ended;    //첫번째 터치 포인트가 릴리즈 상태가 되면 true

        Vector2 IInputHandlerBase.inputPosition => Input.GetTouch(0).position;      //터치위치(Screen 좌표)를 리턴한다.
    }
}