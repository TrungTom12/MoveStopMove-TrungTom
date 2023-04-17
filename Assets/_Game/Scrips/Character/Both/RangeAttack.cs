using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private Character characterOwner;
    Character character;

    private void OnTriggerEnter(Collider other)
    {
        //Cache getcomponent
        if (other.TryGetComponent<Character>(out character))
        {
            if (characterOwner is Bot)
            {
                characterOwner.GetComponent<Bot>().targetFollow = character.transform;
            }
            characterOwner.L_AttackTarget.Add(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out character))
        {
            characterOwner.L_AttackTarget.Remove(character);
        }
    }
}
