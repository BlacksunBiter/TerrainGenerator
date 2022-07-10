using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwichTexture : MonoBehaviour
{
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("Noise Perline");
        items.Add("Noise Diamon-Square");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
    }

}
