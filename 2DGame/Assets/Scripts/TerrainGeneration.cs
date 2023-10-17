using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    public List<Tile> grassTiles;
    public List<Tile> mainPathTiles;
    public List<Tile> pathTiles;
    public Tilemap grassTilemap;
    public Tile voidTile;

    // Width and height of the texture in pixels.
    readonly int pixWidth = 1000;
    readonly int pixHeight = 1000;


    // Start is called before the first frame update
    void Start()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //renderer.material.mainTexture = ThinLines(GeneratePerlinTexture());

        grassTiles = new List<Tile>();
        mainPathTiles = new List<Tile>();
        //voidTile = new();
        //Texture2D voidTileTexture = new(16, 16);

        //for (int x = 0; x < 16; x++)
        //{
                        
        //}
        //voidTileTexture.SetPixel()

        //Sprite voidTileSprite = new Sprite(new Rect(0, 0, 16, 16), );

        // Load all the tp grass tiles 
        var tilesFolder = Resources.LoadAll<Tile>("TP Grass/");

        for (int i = 0; i < tilesFolder.Length; i++)
        {
            // Split them into path tiles and grass tiles
            if(i <= 31)
            {
                grassTiles.Add(tilesFolder[i]);
            }
            if(i >= 32 && i <= 38)
            {
                mainPathTiles.Add(tilesFolder[i]);
            }
            if(i >= 39 && i <= 45)
            {
                pathTiles.Add(tilesFolder[i]);
            }
        }

        GenerateGrassArea(new Vector2Int(-(pixWidth/2), -(pixHeight/2)), new Vector2Int(pixWidth / 2, pixHeight / 2));
        GeneratePathArea(new Vector2Int(-(pixWidth / 2), -(pixHeight / 2)));
    }

    void GenerateGrassArea(Vector2Int areaStart, Vector2Int areaEnd)
    {
        for (int x = areaStart.x; x < areaEnd.x; x++)
        {
            for (int y = areaStart.y; y < areaEnd.y; y++)
            {
                grassTilemap.SetTile(
                    new Vector3Int(x, y, 0), 
                    grassTiles[UnityEngine.Random.Range(0, grassTiles.Count - 1)]);
            }
        }
    }

    void GeneratePathArea(Vector2Int areaStart)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = ThinLines(GeneratePerlinTexture());

        var thinnedPerlin = ThinLines(GeneratePerlinTexture());

        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                if (thinnedPerlin.GetPixel(areaStart.x + x, areaStart.y + y) == Color.black)
                {
                    grassTilemap.SetTile(
                        new Vector3Int(areaStart.x + x, areaStart.y + y, 0),
                        mainPathTiles[UnityEngine.Random.Range(0, mainPathTiles.Count - 1)]);

                    
                }

                int adjecent = 0;

                if (x + 1 <= pixWidth)
                {
                    if (thinnedPerlin.GetPixel(areaStart.x + x + 1, areaStart.y + y) == Color.black)
                    {
                        adjecent++;
                    }
                }
                if (x - 1 > 0)
                {
                    if (thinnedPerlin.GetPixel(areaStart.x + x - 1, areaStart.y + y) == Color.black)
                    {
                        adjecent++;
                    }
                }
                if (y - 1 > 0)
                {
                    if (thinnedPerlin.GetPixel(areaStart.x + x, areaStart.y + y - 1) == Color.black)
                    {
                        adjecent++;
                    }
                }
                if (y + 1 <= pixHeight)
                {
                    if (thinnedPerlin.GetPixel(areaStart.x + x, areaStart.y + y + 1) == Color.black)
                    {
                        adjecent++;
                    }
                }
                if (adjecent >= 1)
                {
                    grassTilemap.SetTile(
                        new Vector3Int(areaStart.x + x, areaStart.y + y, 0),
                        pathTiles[UnityEngine.Random.Range(0, pathTiles.Count - 1)]);
                }
            }
        }
    }

    Texture2D GeneratePerlinTexture()
    {
        Texture2D noiseTex = new (pixWidth, pixHeight);

        // For each pixel in the texture...
        for (int x = 0; x < pixWidth; x++)
        {
            for (int y = 0; y < pixHeight; y++)
            {
                Color color = PaintPerlin(x, y);
                noiseTex.SetPixel(x, y, color);
            }
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.Apply();

        return noiseTex;
    }

    Color PaintPerlin(int x, int y, float perlinThreshold = 0.4f, float perlinScale = 10f)
    {

        //float offsetX = UnityEngine.Random.Range(0f, 1000f);
        //float offsetY = UnityEngine.Random.Range(0f, 1000f);

        float xCoord =  (float)x / pixWidth * perlinScale + 10200f;
        float yCoord =  (float)y / pixHeight * perlinScale + 10200f;

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
                    int adjecent = 0;
                    if (width + 1 <= texture.width)
                    {
                        if (updatedTexture.GetPixel(width + 1, height) == Color.black)
                        {
                            adjecent++;
                        }
                    }
                    if (height - 1 > 0)
                    {
                        if (updatedTexture.GetPixel(width, height - 1) == Color.black)
                        {
                            adjecent++;
                        }
                    }
                    if (height + 1 <= texture.height)
                    {
                        if (updatedTexture.GetPixel(width, height + 1) == Color.black)
                        {
                            adjecent++;
                        }
                    }
                    if (adjecent >= 2)
                    {
                        updatedTexture.SetPixel(width, height, Color.white);
                    }
                }
            }
        }
        updatedTexture.Apply();
        return updatedTexture;
    }
    

    void Update()
    {
        
    }
}
