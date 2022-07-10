using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchDaimondID : MonoBehaviour
{

    public GameObject Nperline;
    public GameObject Ndiamond;

    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();

        string a = "SELECT seq FROM sqlite_sequence WHERE sqlite_sequence.name = 'Diamond-Square'";
        int perit = int.Parse(MyDataBase.ExecuteQueryWithAnswer(a));

        for (int i = 1; i <= perit; i++)
            items.Add(i.ToString());


        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        dropdown.RefreshShownValue();
        //Ndiamond.gameObject.SetActive(false);
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
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

    }
}
