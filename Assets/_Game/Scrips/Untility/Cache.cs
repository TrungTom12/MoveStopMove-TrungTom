using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, CharacterController> characters = new Dictionary<Collider, CharacterController>();

    public static CharacterController GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<CharacterController>());
        }
        return characters[collider];
    }
}
