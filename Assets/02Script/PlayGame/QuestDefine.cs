using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lulu.Quest
{
    public enum MatchType
    {
        NONE        =0,
        THREE       =3,     //3Match
        FOUR        =4,     //4Match        -> CLEAR_HORZ �Ǵ� VERT ����Ʈ
        FIVE        =5,     //5Match        -> CLEAR_LAZER ����Ʈ
        THREE_THREE =6,     //3 + 3 Match   -> CLEAR_CIRCLE ����Ʈ
        THREE_FOUR  =7,     //3 + 4 Match   -> CLEAR_CIRCLE ����Ʈ
        THREE_FIVE  =8,     //3 + 5 Match   -> CLEAR_LAZER ����Ʈ
        FOUR_FIRE   =9,     //4 + 5 Match   -> CLEAR_LAZER ����Ʈ
        FOUR_FOUR   =10,    //4 + 4 Match   -> CLEAR_CIRCLE ����Ʈ
    }

    static class MatchTypeMethod
    {
        public static short ToValue(this MatchType matchType)
        {
            return (short)matchType;
        }

        //���� ��Ī ����� �����Ѵ�
        public static MatchType Add(this MatchType matchTypeSrc, MatchType matchTypeTarget)
        {
            if (matchTypeSrc == MatchType.FOUR && matchTypeTarget == MatchType.FOUR)
                return MatchType.FOUR_FOUR;

            return (MatchType)((int)matchTypeSrc + (int)matchTypeTarget);
        }
    }
}