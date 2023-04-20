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

    public Equipment(int id, string name, int price)
    {
        this.id = id;
        this.name = name;
        this.price = price;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Price { get => price; set => price = value; }
}
