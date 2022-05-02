using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{

    public class MouseHandler : IInputHandlerBase   //IInputHandlerBase �������̽��� ������ Ŭ������ ����
    {
        bool IInputHandlerBase.isInputDown => Input.GetButtonDown("Fire1"); //���콺 ���ʹ�ư�� DOWN�����̸� true
        bool IInputHandlerBase.isInputUp => Input.GetButtonUp("Fire1");     //���콺 ���ʹ�ư�� UP�����̸� true

        Vector2 IInputHandlerBase.inputPosition => Input.mousePosition;     //���콺 ��ġ(Screen ��ǥ)�� �����Ѵ�.

    }
}