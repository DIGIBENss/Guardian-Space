using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX
{
    public void GetTransform(Vector3 position)
    {
        GameManager.Instance.CreateFX(position);
    }
}
