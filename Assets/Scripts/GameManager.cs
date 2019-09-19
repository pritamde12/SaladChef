using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    [Header("MIN/MAX")]
    public Vector2 heightBounds;
    public Vector2 sideBounds;
}
