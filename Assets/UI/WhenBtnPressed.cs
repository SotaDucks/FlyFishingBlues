using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenBtnPressed : MonoBehaviour
{
    public GameObject gameObjectA;
    public GameObject gameObjectD;
    public GameObject gameObjectS;
    public GameObject gameObjectSpace;

    void Update()
        {
            // A ¼ü¿ØÖÆ
            if (Input.GetKeyDown(KeyCode.A))
            {
                gameObjectA.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                gameObjectA.SetActive(false);
            }

            // D ¼ü¿ØÖÆ
            if (Input.GetKeyDown(KeyCode.D))
            {
                gameObjectD.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                gameObjectD.SetActive(false);
            }

            // S ¼ü¿ØÖÆ
            if (Input.GetKeyDown(KeyCode.S))
            {
                gameObjectS.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                gameObjectS.SetActive(false);
            }

            // ¿Õ¸ñ¼ü¿ØÖÆ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObjectSpace.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gameObjectSpace.SetActive(false);
            }
        
    }

}
