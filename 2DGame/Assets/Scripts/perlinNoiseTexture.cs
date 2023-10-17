using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class perlinNoiseTexture : MonoBehaviour
{
    // Width and height of the texture in pixels.
    int pixWidth = 100;
    int pixHeight = 100;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = ThinLines(GeneratePerlinTexture());

    }

    Texture2D GeneratePerlinTexture()
    {
        Texture2D noiseTex = new(pixWidth, pixHeight);

        // For each pixel in the texture...
        for (int width = 0; width < pixWidth; width++)
        {
            for (int height = 0; height < pixHeight; height++)
            {
                Color color = PaintPerlin(width, height);
                noiseTex.SetPixel(width, height, color);
            }
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.Apply();

        return noiseTex;
    }

    Color PaintPerlin(int x, int y, float perlinThreshold = 0.4f, float perlinScale = 10f)
    {
        float xCoord = (float) x / pixWidth * perlinScale;
        float yCoord = (float) y / pixHeight * perlinScale;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if(sample > perlinThreshold)
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
                if(updatedTexture.GetPixel(width, height) == Color.black)
                {
                    int adjecent = 0;
                    if(width + 1 <= texture.width)
                    {
                        if(updatedTexture.GetPixel(width + 1, height) == Color.black)
                        {
                            adjecent++;
                        }
                    }
                    if(height - 1 > 0)
                    {
                        if(updatedTexture.GetPixel(width, height-1) == Color.black)
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
                    if(adjecent >= 2)
                    {
                        updatedTexture.SetPixel(width, height, Color.white);
                    }
                }
            }
        }
        updatedTexture.Apply();
        return updatedTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
