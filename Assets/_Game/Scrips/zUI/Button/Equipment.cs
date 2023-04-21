using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Equipment
{
    private int id;
    private string name;
    private int price;
    private string wingName;
    private string headName;
    private string tailName;
    private string shieldName;
    private int idPant;
    private int idColor;
    public Equipment(int id, string name, int price)
    {
        this.id = id;
        this.name = name;
        this.price = price;
    }

    public Equipment(int id, string name, int price, string wingName, string headName, string tailName, string shieldName, int idPant, int idColor) : this(id, name, price)
    {
        this.wingName = wingName;
        this.headName = headName;
        this.tailName = tailName;
        this.shieldName = shieldName;
        this.idPant = idPant;
        this.idColor = idColor;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Price { get => price; set => price = value; }
    public string WingName { get => wingName; set => wingName = value; }
    public string HeadName { get => headName; set => headName = value; }
    public string TailName { get => tailName; set => tailName = value; }
    public string ShieldName { get => shieldName; set => shieldName = value; }
    public int IdPant { get => idPant; set => idPant = value; }
    public int IdColor { get => idColor; set => idColor = value; }
}
