using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
    public RenderTexture[] lightCheckTextures;
    float lightLevel;
    public int realLightLevel;
    float sum;
    float threshold = 200000;

    // Update is called once per frame
    void Update()
    {
        sum = 0;
        foreach (RenderTexture lightCheck in lightCheckTextures)
        {
            sum += CheckLightLevel(lightCheck);
        }

        realLightLevel = Mathf.RoundToInt(sum);
        if (realLightLevel <= 16)
        {
            realLightLevel = 0;
        }
    }


    float CheckLightLevel(RenderTexture lightCheckTexture)
    {
        RenderTexture tmpTexture = RenderTexture.GetTemporary(lightCheckTexture.width, lightCheckTexture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightCheckTexture, tmpTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmpTexture;

        Texture2D tmp2DTexture = new Texture2D(lightCheckTexture.width, lightCheckTexture.height);
        tmp2DTexture.ReadPixels(new Rect(0, 0, tmpTexture.width, tmpTexture.height), 0, 0);
        tmp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmpTexture);

        Color32[] colours = tmp2DTexture.GetPixels32();
        Destroy(tmp2DTexture);

        lightLevel = 0;
        for (int i = 0; i < colours.Length; i++)
        {
            lightLevel += ((0.2126f * colours[i].r) + (0.7152f * colours[i].g) + (0.0722f * colours[i].b));
        }

        lightLevel -= threshold;
        lightLevel = lightLevel / colours.Length;

        return lightLevel;
    }
}
