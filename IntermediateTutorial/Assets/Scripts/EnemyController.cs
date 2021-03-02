using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    //Static variables are shared across all instances of a class.
    public static int enemyCount = 0;

    public EnemyController()
    {
        //Increment the static variable to know how many objects of this class have been created.
        enemyCount++;
    }
}
