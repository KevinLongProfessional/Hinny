using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "TileOutfit", menuName = "Hinny/TileOutfit")]
public class TileOutfit : ScriptableObject
{
    [SerializeField] public Material material;
    [SerializeField] public OutfitType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
