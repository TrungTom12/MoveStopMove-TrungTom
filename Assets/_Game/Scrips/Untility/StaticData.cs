using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static readonly Dictionary<WeaponType, float> RangeWeapon = new Dictionary<WeaponType, float>{
            {WeaponType.Knife,4},
            {WeaponType.Boomerang,6 },
            {WeaponType.Axe,4 },
            {WeaponType.Candy,4 }
        };
}
