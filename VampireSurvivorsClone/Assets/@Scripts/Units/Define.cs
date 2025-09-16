using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Define
{
    public enum Scene
    {
        Unknown,
        DevScene,
        GameScene,
    }

    public enum  Sound
    {
        BGM,
        Effect,
    }

    public enum  ObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }
}
