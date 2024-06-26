using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using TMPro;

public class Row : MonoBehaviour
{
    public Tile[] tiles { get; private set; }

    public string Word
    {
        get
        {
            string word = "";

            for (int i = 0; i < tiles.Length; i++) {
                word += tiles[i].letter;
            }

            return word;
        }
    }

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }
}
