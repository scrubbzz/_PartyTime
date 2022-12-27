using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IfallingCollectable  
{
    public void OnSpawn();



    public void OnFall();



    public void OnTouch(Collider collider);
   
}
