using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitBuilder
{
    private static readonly string[] UNIT_ARR = {"", "K", "M", "B", "T"};

    public static string GetUnitStr(double value)
    {
        string valueStr = value.ToString("N0");
        string[] splited = valueStr.Split(',');

        string result = "";

        if(splited.Length > 1)
        {
            char[] underPoint = splited[1].ToCharArray();
            result = string.Format("{0}.{1}{2} {3}", splited[0], underPoint[0], underPoint[1], UNIT_ARR[splited.Length - 1]);
        }
        else
        {
            result = splited[0];
        }

        return result;
    }
}
