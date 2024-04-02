using System;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public abstract void Move();
    public abstract void SubscribeToList();
    public abstract void RemoveFromList();
}
