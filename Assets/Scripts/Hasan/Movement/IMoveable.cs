using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable 
{
    public void ReadInputs();
    public void Move(float moveSpeed);
    
}
