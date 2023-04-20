using System;
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
    public static readonly Dictionary<WeaponType, int> PriceWeapon = new Dictionary<WeaponType, int>{
            {WeaponType.Knife,100},
            {WeaponType.Boomerang,200 },
            {WeaponType.Axe,400 },
            {WeaponType.Candy,50 }
        };
    public static readonly Dictionary<String, WeaponType> WeaponEnum = new Dictionary<String, WeaponType>{
            {"Knife",WeaponType.Knife},
            {"Boomerang",WeaponType.Boomerang },
            {"Axe",WeaponType.Axe },
            {"Candy",WeaponType.Candy }
        };
    public static readonly Dictionary<String, Head> HeadEnum = new Dictionary<String, Head>{
            {"Head1",Head.Head1},
            {"Head2",Head.Head2},

        };
    public static readonly List<Equipment> pantEquipments = new List<Equipment>
    {
            new Equipment(1,"Pant1",100),new Equipment(2,"Pant2",200)
    };
    public static readonly List<Equipment> headEquipments = new List<Equipment>
    {
            new Equipment(1,"Head1",100),new Equipment(2,"Head2",200)
    };
    public static readonly List<Equipment> shieldEquipments = new List<Equipment>
    {
            new Equipment(1,"Shield1",300),new Equipment(2,"Shield2",400)
    };
    public static readonly List<Equipment> setEquipments = new List<Equipment>
    {
            new Equipment(1,"Set1",1000),new Equipment(2,"Set2",2000)
    };
}
