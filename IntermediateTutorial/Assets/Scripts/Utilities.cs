using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //A static method can be invoked without an object
    //of a class. Note that static methods cannot access
    //non-static member variables.
    public static int Add(int num1, int num2)
    {
        return num1 + num2;
    }

    public static string Add(string str1, string str2)
    {
        return str1 + str2;
    }

}

   
