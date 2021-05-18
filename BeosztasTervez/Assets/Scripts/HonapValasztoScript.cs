using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HonapValasztoScript : MonoBehaviour
{
    public List<Dolgozo> dolgozok;
    public List<string> nevek = new List<string> { "nev1", "nev2", "nev3" };
    public Dropdown honapValaszto;
    public GameObject honapNapjaiPanel;
    public GameObject napPanelPrefab;
    public GameObject dolgozoPanel;
    public GameObject muszakPanel;
    public GameObject nevPanel;

    public List<string> honapok = new List<string> { "Január", "Február", "Március", "Április", "Május", "Júnis", "Július", "Augusztus", "Szeptember", "Október", "November", "December" };
    public int honapNapjai;


    // Start is called before the first frame update
    void Start()
    {
        SetupHonapValaszto();
        honapNapjai = DateTime.DaysInMonth(DateTime.Now.Year, honapValaszto.value + 1);
        SetupHonapNapjaiPanel();
        UresBeosztas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UresBeosztas()
    {
        dolgozok = new List<Dolgozo>();
        foreach (string nev in nevek)
        {
            Dolgozo d = new Dolgozo();
            d.nev = nev;
            List<muszakok> m = new List<muszakok>();
            for (int i = 0; i < honapNapjai; i++)
            {
                m.Add(muszakok.Ures);
            }
            d.beosztas = m;
            dolgozok.Add(d);
        }

        for (int i = 0; i < dolgozok.Count; i++)
        {
            GameObject panel = Instantiate(dolgozoPanel);
            panel.transform.SetParent(honapValaszto.transform.parent);
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * 50);
            panel.GetComponent<RectTransform>().localScale = Vector2.one;

            for (int j = 0; j < dolgozok[i].beosztas.Count; j++)
            {
                GameObject m = Instantiate(muszakPanel);
                m.transform.SetParent(panel.transform);
                m.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 + j * 60, 0);
                m.GetComponent<RectTransform>().localScale = Vector2.one;
                m.GetComponent<Dropdown>().value = (int)dolgozok[i].beosztas[j];
            }
        }
    }

    void SetupHonapValaszto()
    {
        honapValaszto.ClearOptions();
        //foreach (string item in honapok)
        //{
            honapValaszto.AddOptions(honapok);
        //}
        honapValaszto.value = DateTime.Now.Month - 1;
        Debug.Log(DateTime.Now.Month);
    }

    void SetupHonapNapjaiPanel()
    {
        for (int i = 0; i < honapNapjai; i++)
        {
            GameObject go = Instantiate(napPanelPrefab);
            go.transform.SetParent(honapNapjaiPanel.transform);
            RectTransform rect = go.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(250 + i * 60, 0);
            go.GetComponentInChildren<Text>().text = (i + 1).ToString();
            rect.localScale = Vector3.one;
        }
    }
}
