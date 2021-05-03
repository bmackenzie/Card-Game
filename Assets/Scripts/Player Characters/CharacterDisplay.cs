using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{

    public PlayerCharacter character;
    public Text namePlate;
    public Image portrait;
    // Start is called before the first frame update
    void Start()
    {
        portrait.sprite = character.art;
        namePlate.text = character.characterName;
    }
}
