using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwichNoises : MonoBehaviour
{
    public Text TextBox;
    public GameObject Nperline;
    public GameObject Ndiamond;
    // Start is called before the first frame update
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

        Ndiamond.gameObject.SetActive(false);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        if (dropdown.value == 1)
        {
            Nperline.gameObject.SetActive(false);
            Ndiamond.gameObject.SetActive(true);
        }
        if (dropdown.value == 0)
        {
            Nperline.gameObject.SetActive(true);
            Ndiamond.gameObject.SetActive(false);
        }
        int index = dropdown.value;
        TextBox.text = dropdown.options[index].text;
    }
}
