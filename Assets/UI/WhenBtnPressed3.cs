using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhenBtnPressed3 : MonoBehaviour
{
    public GameObject gameObjectD;
    public GameObject gameObjectS;
    public GameObject gameObjectSpace;

    // 按下和松开时使用的图片
    public Sprite pressedSpriteD;
    public Sprite unpressedSpriteD;
    public Sprite pressedSpriteS;
    public Sprite unpressedSpriteS;
    public Sprite pressedSpriteSpace;
    public Sprite unpressedSpriteSpace;

    void Update()
    {
        // D 键控制
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObjectD.GetComponent<Image>().sprite = pressedSpriteD;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            gameObjectD.GetComponent<Image>().sprite = unpressedSpriteD;
        }

        // S 键控制
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObjectS.GetComponent<Image>().sprite = pressedSpriteS;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            gameObjectS.GetComponent<Image>().sprite = unpressedSpriteS;
        }

        // 空格键控制
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObjectSpace.GetComponent<Image>().sprite = pressedSpriteSpace;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObjectSpace.GetComponent<Image>().sprite = unpressedSpriteSpace;
        }
    }

}
