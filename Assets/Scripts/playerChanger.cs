using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChanger : MonoBehaviour
{
    public List<GameObject> character;
    private int characterId;
    public Vector3 position;

    void Awake()
    {
        characterId = PlayerPrefs.GetInt("character");
        Instantiate(character[characterId], position, Quaternion.identity);
    }
}
