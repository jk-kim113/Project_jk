﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValue
{
    public delegate void VoidCallBack();
    public delegate void OneIntParameter(int a);

    public const int PLAYER_LEVEL_LENGTH = 6;
    public const int MONSTER_LENGTH = 4;
    public const int CARD_ID_LENGTH = 6;
    public const int ITEM_NUM_LENGTH = 3;
    public const int EQUIP_NUM_LENGTH = 3;
}
