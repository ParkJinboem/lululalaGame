using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public class TouchHandler : IInputHandlerBase
    {
        bool IInputHandlerBase.isInputDown => Input.GetTouch(0).phase == TouchPhase.Began;  //ù��° ��ġ ����Ʈ�� ������ true
        bool IInputHandlerBase.isInputUp => Input.GetTouch(0).phase == TouchPhase.Ended;    //ù��° ��ġ ����Ʈ�� ������ ���°� �Ǹ� true

        Vector2 IInputHandlerBase.inputPosition => Input.GetTouch(0).position;      //��ġ��ġ(Screen ��ǥ)�� �����Ѵ�.
    }
}