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

    public static readonly Dictionary<String, HeadType> HeadEnum = new Dictionary<String, HeadType>{
            {"Head1",HeadType.Head1},
            {"Head2",HeadType.Head2},
            {"ArrowHead",HeadType.ArrowHead },
        {"Cowboy",HeadType.Cowboy },
        {"Crown",HeadType.Crown },
        {"HatCap",HeadType.HatCap },
        {"HatYellow",HeadType.HatYellow },
        {"HeadPhone",HeadType.HeadPhone },
            {"Horn",HeadType.Horn },
            {"head_angel",HeadType.head_angel},
        {"HatWitch",HeadType.HatWitch},
        {"Hat_Thor",HeadType.Hat_Thor }

        };

    public static readonly Dictionary<String, ShieldType> ShieldEnum = new Dictionary<String, ShieldType>{
            {"Shield1",ShieldType.Shield1},
            {"Shield2",ShieldType.Shield2},
            {"bow_angel",ShieldType.bow_angel },
        {"Book",ShieldType.Book },
        {"Blade_Death",ShieldType.Blade_Death}
        };

    public static readonly Dictionary<String, SetType> SetEnum = new Dictionary<String, SetType>{
            {"Set1",SetType.Set1 },{"Set2",SetType.Set2 },
        {"Set3",SetType.Set3},{"Set4",SetType.Set4},{"Set5",SetType.Set5}
        };

    public static readonly Dictionary<String, WingType> WingEnum = new Dictionary<String, WingType>{
            {"wing_devil",WingType.wing_devil},
            {"wing_angel",WingType.wing_angel}
        };

    public static readonly Dictionary<String, TailType> TailEnum = new Dictionary<String, TailType>{
            {"tail_devil",TailType.tail_devil}
        };

    public static readonly List<Equipment> pantEquipments = new List<Equipment>
    {
            new Equipment(1,"Pant1",100),new Equipment(2,"Pant2",200),
            new Equipment(5,"chambi",200),new Equipment(6,"comy",400),
            new Equipment(7,"dabao",300), new Equipment(8,"onion",500),
            new Equipment(9,"rainbow",700), new Equipment(10,"Skull",400),
            new Equipment(11,"vantim",200),
    };

    public static readonly List<Equipment> headEquipments = new List<Equipment>
    {
            new Equipment(1,"Head1",100),
        new Equipment(2,"Head2",200),
        new Equipment(3,"ArrowHead",300),
        new Equipment(4,"Cowboy",400),
        new Equipment(5,"Crown",500),
        new Equipment(6,"HatCap",500),
        new Equipment(7,"HatYellow",600),
        new Equipment(8,"HeadPhone",500)
    };

    public static readonly List<Equipment> shieldEquipments = new List<Equipment>
    {
            new Equipment(1,"Shield1",300),new Equipment(2,"Shield2",400)
    };
    public static readonly List<Equipment> setEquipments = new List<Equipment>
    {
        new Equipment(1,"Set1",1000,"wing_devil","Horn","tail_devil",null,3,1),
        new Equipment(2,"Set2",2000,"wing_angel","head_angel",null,"bow_angel",4,3),
        new Equipment(3,"Set3",1000,null,"HatWitch",null,"Book",1,4),
        new Equipment(4,"Set4",3000,null,null,null,"Blade_Death",0,5),
        new Equipment(5,"Set5",4000,null,"Hat_Thor",null,null,0,6)
    };
}
