using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHider : MonoBehaviour
{
    [SerializeField]
    Color m_Color = Color.white;
    [SerializeField]
    Color changeColor = new Color(1, 1, 1, 0.2f);
    [SerializeField]
    SpriteRenderer m_Renderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Renderer.color = changeColor;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        m_Renderer.color  = m_Color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
