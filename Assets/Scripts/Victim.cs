using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    [SerializeField] bool Dissapearing;
    public bool HasRandomAppearance;
    private void Start()
    {
        Dissapearing = false;
        if(HasRandomAppearance)
        {
            GenerateRandomApearance();
        }
    }
    private void Update()
    {
        if (Dissapearing)
        {
            foreach (var sprite in sprites)
            {
                Color color = sprite.color;
                color.a -= 0.3f * Time.deltaTime;
                if(color.a < 0f) color.a = 0f;
                if(color.a < 0.6f) if (!Dissapearing) AudioManager.PlayCall("Obliviate");
                sprite.color = color;
            }
        }
    }

    public void GenerateRandomApearance()
    {
        foreach(var sprite in sprites)
        {
            GiveSpriteRandomColor(sprite);
        }
    }

    public void GiveSpriteRandomColor(SpriteRenderer sprite)
    {
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void Dissapear()
    {
        Dissapearing = true;
    }
}
