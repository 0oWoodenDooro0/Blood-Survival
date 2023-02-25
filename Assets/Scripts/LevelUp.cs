using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public Image[] images;
    public Sprite[] sprites;
    public Button[] buttons;
    private List<int> _selections;
    private List<int> _imageSprites;

    private void Awake()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => ChoiceOnClick(_imageSprites[index]));
        }
    }

    public void RandomSelection()
    {
        Time.timeScale = 0f;
        _imageSprites = new List<int> { Random.Range(0, sprites.Length) };
        var imageSprite2 = Random.Range(0, sprites.Length);
        while (_imageSprites[0] == imageSprite2)
        {
            imageSprite2 = Random.Range(0, sprites.Length);
        }

        _imageSprites.Add(imageSprite2);
        var imageSprites3 = Random.Range(0, sprites.Length);
        while (_imageSprites[0] == imageSprites3 || _imageSprites[1] == imageSprites3)
        {
            imageSprites3 = Random.Range(0, sprites.Length);
        }

        _imageSprites.Add(imageSprites3);
        for (var i = 0; i < images.Length; i++)
        {
            images[i].sprite = sprites[_imageSprites[i]];
        }
    }

    private void ChoiceOnClick(int index)
    {
        Time.timeScale = 1f;
        switch (index)
        {
            case 0:
                Game.Instance.shovel += 1;
                break;
            case 1:
                Game.Instance.fork += 1;
                break;
            case 2:
                Game.Instance.hook += 1;
                break;
            case 3:
                Game.Instance.pistol += 1;
                break;
            case 4:
                Game.Instance.rifle += 1;
                break;
            case 5:
                Game.Instance.shotgun += 1;
                break;
            case 6:
                Game.Instance.armor += 1;
                break;
            case 7:
                Game.Instance.shoe += 1;
                break;
            case 8:
                Game.Instance.maxHp += 1;
                break;
            case 9:
                Game.Instance.damage += 1;
                break;
        }

        Game.Instance.pause = false;
    }
}