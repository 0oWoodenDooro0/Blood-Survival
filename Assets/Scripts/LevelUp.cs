using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    [Header("Game")] public PlayerAttribute playerAttribute;
    [Header("Skill")] public SkillAttribute skillAttribute;
    public Image[] images;
    public Sprite[] sprites;
    public GameObject[] buttons;
    private List<int> _selections;
    private List<int> _imageSprites;

    private void Awake()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => ChoiceOnClick(_imageSprites[index]));
        }
    }

    private void Update()
    {
        if (Input.anyKey && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttons[0]);
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

    private void ChoiceOnClick(int index)
    {
        Time.timeScale = 1f;
        switch (index)
        {
            case 0:
                skillAttribute.shovel += 1;
                break;
            case 1:
                skillAttribute.fork += 1;
                break;
            case 2:
                skillAttribute.hook += 1;
                break;
            case 3:
                skillAttribute.pistol += 1;
                break;
            case 4:
                skillAttribute.rifle += 1;
                break;
            case 5:
                skillAttribute.shotgun += 1;
                break;
            case 6:
                skillAttribute.armor += 1;
                playerAttribute.armor += 0.1f;
                break;
            case 7:
                skillAttribute.shoe += 1;
                playerAttribute.moveSpeed += 0.5f;
                break;
            case 8:
                skillAttribute.maxHealth += 1;
                playerAttribute.health += 50;
                playerAttribute.maxHealth += 50;
                break;
            case 9:
                skillAttribute.damage += 1;
                break;
        }

        Game.Instance.gameAttribute.pause = false;
        gameObject.SetActive(false);
    }
}