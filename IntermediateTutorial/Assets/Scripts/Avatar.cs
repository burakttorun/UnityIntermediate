using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is a basic interface with a single required
//method.
public interface IKillable
{
    void Kill();
}

//This is a generic interface where T is a placeholder
//for a data type that will be provided by the 
//implementing class.
public interface IDamageable<T>
{
    void Damage(T damageTaken);
}
public class Avatar : MonoBehaviour
{
    //The required method of the IKillable interface
    public void Kill()
    {
        //Do something fun
    }

    //The required method of the IDamageable interface
    public void Damage(float damageTaken)
    {
        //Do something fun
    }
}
