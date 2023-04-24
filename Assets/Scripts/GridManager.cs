using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height; //to do: rename height to length or something...
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private List<Material> _tileMaterials;
    [SerializeField] public Transform _cam;
    public List<List<Tile>> tiles;

    public static GridManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<List<Tile>>();
        Instance = this;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            tiles.Add(new List<Tile>());
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                int index = Random.Range(0, _tileMaterials.Count);

                spawnedTile.Init(_tileMaterials[index]);
                tiles[x].Add(spawnedTile);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, 10, (float)_height / 2 - 0.5f);
    }
}
