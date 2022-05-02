using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Util
{
    public static class SortedListMethods
    {
        //SortedList Ȯ�� �޼ҵ�
        //ù��° ����� key-value�� ���Ѵ�.
        //(����) ����ִ� ��� T1, T2 Ÿ���� �������� ���޵ǹǷ� ȣ������ ����ִ��� üũ�ϴ� ���� �����ϴ�.
        // ��� ��) KeyValuePair<int, Vector2) kv = sortedList.First();

        public static KeyValuePair<T1, T2> First<T1, T2>(this SortedList<T1, T2> sortedList)
        {
            if (sortedList.Count == 0)
                return new KeyValuePair<T1, T2>();

            return new KeyValuePair<T1, T2>(sortedList.Keys[0], sortedList.Values[0]);
        }
    }
}