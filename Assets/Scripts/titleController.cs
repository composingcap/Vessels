using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class titleController : MonoBehaviour
{
    public RectTransform[] titles;
    public int selectedTitle;
    // Start is called before the first frame update
    void Start()
    {

    }
    [Button]
    public void showTitle()
    {
        titles[selectedTitle].gameObject.SetActive(true);
    }
    [Button]
    public void hideTitle()
    {
        foreach (RectTransform t in titles)
        {
            t.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame


}
