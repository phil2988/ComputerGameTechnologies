using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public enum TileTypes
    {
        PathFull,
        PathPartial,
        Void,
        Grass,
        Poi,
        Test
    }

    public Dictionary<TileTypes, Color> TileColors = new() {
        { TileTypes.PathFull, Color.black },
        { TileTypes.PathPartial, Color.blue },
        { TileTypes.Void , Color.white },
        { TileTypes.Grass , Color.green },
        { TileTypes.Test , Color.yellow },
        { TileTypes.Poi , Color.magenta },
    };

    public List<Tile> grassTiles = new();
    public List<Tile> fullPathTiles = new();
    public List<Tile> partialPathTiles = new();

    public Tilemap terrainTileMap;
    public Tilemap voidTileMap;
    public List<Tilemap> poiTileMaps;
    public Tile voidTile;
    public Tile testTile;

    public List<GameObject> mobPool = new();

    // Width and height of the texture in pixels.
    readonly int pixWidth = 500;
    readonly int pixHeight = 500;

    // Texture for holding the generation data
    Texture2D terrainTexture;
    readonly Vector3Int startPos = new(-250, -250, 0); 

    void Start()
    {
        terrainTexture = new(pixWidth, pixHeight);

        // Fill the texture with white
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if(IsTileInTilemapsEmpty(x + startPos.x, y + startPos.y))
                {
                    terrainTexture.SetPixel(x, y, TileColors.Where((tile) => tile.Key == TileTypes.Void).Single().Value);
                }
                else
                {
                    terrainTexture.SetPixel(x, y, TileColors.Where((tile) => tile.Key == TileTypes.Poi).Single().Value);
                }
            }
        }

        Debug.Log("Loading tiles from resouces...");
        LoadTiles();
        Debug.Log("Done!");

        Debug.Log("Generating path section of texture...");
        GeneratePathArea(repetitions: 5);
        terrainTexture.Apply();
        Debug.Log("Done!");

        Debug.Log("Generating path padding section of texture...");
        GeneratePathPadding();
        terrainTexture.Apply();
        Debug.Log("Done!");

        Debug.Log("Generating path padding section of texture...");
        GeneratePointsOfInterest();
        terrainTexture.Apply();
        Debug.Log("Done!");

        Debug.Log("Generating grass padding of paths...");
        GenerateGrassPadding();
        terrainTexture.Apply();
        Debug.Log("Done!");

        Debug.Log("Generating grass padding of paths...");
        GenerateGrassPadding();
        terrainTexture.Apply();
        Debug.Log("Done!");

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = terrainTexture;

        Debug.Log("Applying terrain data to tilemap...");
        PaintTerrain();
        Debug.Log("Done!");

        Debug.Log("Done Generating Texture!");
    }

    void LoadTiles()
    {
        // Load all the tp grass tiles 
        var tilesFolder = Resources.LoadAll<Tile>("TP Grass/");

        for (int i = 0; i < tilesFolder.Length; i++)
        {
            // Split them into path tiles and grass tiles
            if (i <= 31)
            {
                grassTiles.Add(tilesFolder[i]);
            }
            if (i >= 32 && i <= 38)
            {
                fullPathTiles.Add(tilesFolder[i]);
            }
            if (i >= 39 && i <= tilesFolder.Length - 3)
            {
                partialPathTiles.Add(tilesFolder[i]);
            }
            if(i == tilesFolder.Length -2)
            {
                voidTile = tilesFolder[i];
            }
            if (i == tilesFolder.Length -1)
            {
                testTile = tilesFolder[i];
            }
        }
    }

    void GeneratePathArea(TileTypes tileType = TileTypes.PathFull, int repetitions = 1)
    {
        Texture2D perlinData = new(pixWidth, pixHeight);

        if(repetitions > 1)
        {
            var thinPerlinTextures = new List<Texture2D>();
            // Generate all the perlins needed
            for (int i = 0; i < repetitions; i++)
            {
                var basic = GeneratePerlinTexture();
                var thin = ThinLines(basic);
                thinPerlinTextures.Add(thin);
            }

            // Combine all the perlins into one texture
            foreach (var item in thinPerlinTextures)
            {
                for (int x = 0; x < pixWidth; x++)
                {
                    for (int y = 0; y < pixHeight; y++)
                    {
                        if (item.GetPixel(x, y) == Color.black)
                        {
                            perlinData.SetPixel(x, y, Color.black);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if (perlinData.GetPixel(x, y) == Color.black)
                {
                    terrainTexture.SetPixel(
                        x,
                        y,
                        TileColors.Where((tile) => tile.Key == tileType).Single().Value
                    );
                }
            }
        }
    }

    void GeneratePathPadding(TileTypes tileType = TileTypes.PathPartial)
    {
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if(terrainTexture.GetPixel(x, y) == TileColors.Where((tile) => tile.Key == TileTypes.Void).Single().Value)
                {
                    int adjecentTiles = 0;

                    foreach (var position in paddingNeighbourPositions)
                    {
                        if (
                            terrainTexture.GetPixel(
                                x + position.x, 
                                y + position.y
                            ) == TileColors.Where((tile) => tile.Key == TileTypes.PathFull).Single().Value
                        )
                        {
                            adjecentTiles++;
                        }
                    }

                    if (adjecentTiles > 0)
                    {
                        terrainTexture.SetPixel(
                            x,
                            y,
                            TileColors.Where((tile) => tile.Key == tileType).Single().Value
                        );
                    }
                }
            }
        }
    }

    void GenerateGrassPadding(TileTypes tileType = TileTypes.Grass)
    {
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if (terrainTexture.GetPixel(x, y) == TileColors.Where((tile) => tile.Key == TileTypes.Void).Single().Value)
                {
                    int adjecentPathTiles = 0;
                    int adjecentGrassTiles = 0;

                    foreach (var position in paddingNeighbourPositions)
                    {
                        if (
                            terrainTexture.GetPixel(
                                x + position.x,
                                y + position.y
                            ) == TileColors.Where((tile) => tile.Key == TileTypes.PathPartial).Single().Value
                        )
                        {
                            adjecentPathTiles++;
                        }

                        if (
                            terrainTexture.GetPixel(
                                x + position.x,
                                y + position.y
                            ) == TileColors.Where((tile) => tile.Key == TileTypes.Grass).Single().Value
                        )
                        {
                            adjecentGrassTiles++;
                        }
                    }

                    if (adjecentPathTiles > 0 || adjecentGrassTiles + adjecentPathTiles >= 3)
                    {
                        terrainTexture.SetPixel(
                            x,
                            y,
                            TileColors.Where((tile) => tile.Key == tileType).Single().Value
                        );
                    }
                }
            }
        }

    }

    void GeneratePointsOfInterest(int likelyhood = 10 /*int radius = 15, int differentRadius = 4*/)
    {
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if(terrainTexture.GetPixel(x, y) == TileColors.Where((tile) => tile.Key == TileTypes.PathFull).Single().Value)
                {
                    var rand = UnityEngine.Random.Range(0, likelyhood);

                    if (rand == 1)
                    {
                        // Choose a random mob to spawn
                        var mob = mobPool[UnityEngine.Random.Range(0, mobPool.Count)];

                        // Spawn the mob at the position
                        Instantiate(mob, new Vector3(x + startPos.x, y + startPos.y, 0), Quaternion.identity);
                    }
                }
            }
        }
    }

    void PaintTerrain()
    {
        // Iterate through all the pixels in the data texture
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                // Check which type of pixel x, y is
                var pixel = TileColors.Where((tile) => tile.Value == terrainTexture.GetPixel(x, y)).FirstOrDefault().Key;

                // Color the tile according to the selected pixel
                switch (pixel)
                {
                    case TileTypes.Void:
                        voidTileMap.SetTile(
                            startPos + new Vector3Int(x, y, 0),
                            GetRandomTile(TileTypes.Void)
                        );
                        break;

                    case TileTypes.PathFull:
                        terrainTileMap.SetTile(
                            startPos + new Vector3Int(x, y, 0),
                            GetRandomTile(TileTypes.PathFull)
                        );
                        break;

                    case TileTypes.PathPartial:
                        terrainTileMap.SetTile(
                            startPos + new Vector3Int(x, y, 0),
                            GetRandomTile(TileTypes.PathPartial)
                        );
                        break;

                    case TileTypes.Grass:
                        terrainTileMap.SetTile(
                            startPos + new Vector3Int(x, y, 0),
                            GetRandomTile(TileTypes.Grass)
                        );
                        break;

                    case TileTypes.Test:
                        terrainTileMap.SetTile(
                            startPos + new Vector3Int(x, y, 0),
                            GetRandomTile(TileTypes.Test)
                        );
                        break;

                    default:

                        break;   
                }
            }
        }
    }

    Tile GetRandomTile(TileTypes tileType)
    {
        switch (tileType)
        {
            case TileTypes.PathFull:
                return fullPathTiles[UnityEngine.Random.Range(0, fullPathTiles.Count)];

            case TileTypes.PathPartial:
                return partialPathTiles[UnityEngine.Random.Range(0, partialPathTiles.Count)];
                
            case TileTypes.Void:
                return voidTile;

            case TileTypes.Grass:
                return grassTiles[UnityEngine.Random.Range(0, grassTiles.Count)];

            case TileTypes.Test:
                return testTile;

            default:
                throw new Exception("Invalid tile type input!");
        }
    }

    Texture2D GeneratePerlinTexture()
    {
        Texture2D noiseTex = new (pixWidth, pixHeight);

        // Randomize the offset value for the perlin noise
        var perlinOffset = new Vector2(
            UnityEngine.Random.Range(0, 100000),
            UnityEngine.Random.Range(0, 100000)
        );

        // For each pixel in the texture...
        for (int x = 0; x < pixWidth; x++)
        {
                for (int y = 0; y < pixHeight; y++) {
                    Color color = PaintPerlin(x, y, 0.4f, 8f, perlinOffset);
                if (color == Color.black) {
                    noiseTex.SetPixel(x, y, color);
                }
            }
        }
        // Apply the generated perlin to the texture
        noiseTex.Apply();
        return noiseTex;
    }

    Color PaintPerlin(int x, int y, float perlinThreshold, float perlinScale, Vector2? offset = null)
    {
        float xCoord =  (float)x / pixWidth * perlinScale + (offset == null ? 0 : offset.Value.x);
        float yCoord =  (float)y / pixHeight * perlinScale + (offset == null ? 0 : offset.Value.y);

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        if (sample > perlinThreshold)
        {
            return Color.black;
        }
        return Color.white;
    }

    Texture2D ThinLines(Texture2D texture)
    {
        Texture2D updatedTexture = texture;

        for (int width = 0; width < texture.width; width++)
        {
            for (int height = 0; height < texture.height; height++)
            {
                if (updatedTexture.GetPixel(width, height) == Color.black)
                {
                    var allAdjecent = FindAllBlackNeighbors(new Vector2Int(width, height), texture, NeighbourType.Thinning);

                    if (allAdjecent >= 2)
                    {
                        updatedTexture.SetPixel(width, height, Color.white);
                    }
                }
            }
        }
        updatedTexture.Apply();
        return updatedTexture;
    }

    private readonly Vector3Int[] thinningNeighbourPositions =
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,
        //Vector3Int.up + Vector3Int.right,
        //Vector3Int.up + Vector3Int.left,
        //Vector3Int.down + Vector3Int.right,
        //Vector3Int.down + Vector3Int.left
    };
  
    private readonly Vector3Int[] paddingNeighbourPositions =
{
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,
        //Vector3Int.up + Vector3Int.right,
        //Vector3Int.up + Vector3Int.left,
        //Vector3Int.down + Vector3Int.right,
        //Vector3Int.down + Vector3Int.left
    };

    public enum NeighbourType
    {
        Padding,
        Thinning
    }

    public int FindAllBlackNeighbors(Vector2Int gameOjectPosition, Texture2D texture, NeighbourType neighbourType)
    {
        int adjecentTiles = 0;

        foreach (var position in neighbourType == NeighbourType.Thinning ? thinningNeighbourPositions : paddingNeighbourPositions)
        {
            if(texture.GetPixel(gameOjectPosition.x + position.x, gameOjectPosition.y + position.y) == Color.black)
            {
                adjecentTiles++;
            }
        }
        return adjecentTiles;
    }

    public bool IsTileInTilemapsEmpty(int x, int y)
    {
        // Check the poi tilemaps to see if they have the coordinate set
        for (int i = 0; i < poiTileMaps.Count; i++)
        {
            if (poiTileMaps[i].HasTile(new Vector3Int(x, y, 0)))
            {
                return false;
            }
        }

        if(terrainTileMap.HasTile(new Vector3Int(x, y, 0)))
        {
            return false;
        }

        return true;
    }
}
