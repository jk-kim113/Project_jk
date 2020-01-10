using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveViewPoint : MonoBehaviour
{
    public void Move(int xCord)
    {
        transform.position = new Vector3(xCord, transform.position.y, transform.position.z);
    }
}
