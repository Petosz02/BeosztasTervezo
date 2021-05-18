using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

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

    public List<muszakok> mEnumList;
    public List<string> mList;

    public List<string> honapok = new List<string> { "Január", "Február", "Március", "Április", "Május", "Júnis", "Július", "Augusztus", "Szeptember", "Október", "November", "December" };
    public int honapNapjai;


    // Start is called before the first frame update
    void Start()
    {
        SetupHonapValaszto();
        honapNapjai = DateTime.DaysInMonth(DateTime.Now.Year, honapValaszto.value + 1);
        mEnumList = Enum.GetValues(typeof(muszakok)).Cast<muszakok>().ToList();
        mList = new List<string>();
        foreach (muszakok muszak in mEnumList)
        {
            mList.Add(muszak.ToString());
        }
        SetupHonapNapjaiPanel();
        UresBeosztas();

        Debug.Log(dolgozok[0].beosztas[2].ToString());

        FindUres(dolgozok);

        for (int i = 0; i < 10; i++)
        {
            MuszakBeallitas(FindUres(dolgozok));
        }
    }

    public void GenerateMuszak()
    {
        while(FindUres(dolgozok) != new Vector2Int (-1,-1))
            MuszakBeallitas(FindUres(dolgozok));
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
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250 - i * 50);
            panel.GetComponent<RectTransform>().localScale = Vector2.one;
            panel.name = dolgozok[i].nev;

            GameObject nev = Instantiate(nevPanel);
            nev.transform.SetParent(panel.transform);
            nev.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            nev.GetComponent<RectTransform>().localScale = Vector2.one;
            nev.GetComponentInChildren<Text>().text = dolgozok[i].nev;

            for (int j = 0; j < dolgozok[i].beosztas.Count; j++)
            {
                GameObject m = Instantiate(muszakPanel);
                m.transform.SetParent(panel.transform);
                m.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 + j * 60, 0);
                m.GetComponent<RectTransform>().localScale = Vector2.one;

                int d = i;
                int y = j;

                Dropdown dp = m.GetComponent<Dropdown>();
                dp.ClearOptions();
                dp.AddOptions(mList);
                dp.onValueChanged.AddListener(x => {
                    Debug.Log("működik" + d + ":" + y);
                    muszakok musz = (muszakok)dp.value;
                    dolgozok[d].beosztas[y] = musz;
                });

                dp.value = (int)dolgozok[i].beosztas[j];
                m.name = dolgozok[i].nev + j;
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

    Vector2Int FindUres(List<Dolgozo> dolgozos)
    {
        Vector2Int poz = new Vector2Int(-1, -1);
        int i = 0;
        while (i < dolgozos.Count)
        {
            Debug.Log(i);
            int index = dolgozos[i].beosztas.FindIndex(x => x == muszakok.Ures);
            if(index == -1)
            {
                i++;
            }
            else
            {
                Debug.Log(i + " " + index);
                poz.x = i;
                poz.y = index;
                return poz;
            }
        }
        Debug.Log(poz);
        return poz;
    }

    void MuszakBeallitas(Vector2Int poz)
    {
        dolgozok[poz.x].beosztas[poz.y] = muszakok.Nappal;
        Debug.Log("Canvas" + "/" + dolgozok[poz.x] + "/" + dolgozok[poz.x] + poz.y);
        Debug.Log(GameObject.Find("Canvas" + "/" + dolgozok[poz.x].nev + "/" + dolgozok[poz.x].nev + poz.y));
        GameObject.Find("Canvas" + "/" + dolgozok[poz.x].nev + "/" + dolgozok[poz.x].nev + poz.y).GetComponent<Dropdown>().value = (int)muszakok.Nappal;
    }

}
