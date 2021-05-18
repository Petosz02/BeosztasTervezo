using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum muszakok
{
    Ures,
    Nappal,
    Éjszaka,
    Alvo,
    Piheno,
    Szabi
}

[System.Serializable]
public class Dolgozo
{
    public string nev;
    public List<muszakok> beosztas;

    public Dolgozo()
    {

    }

    public Dolgozo(string nev, List<muszakok> beosztas)
    {
        this.nev = nev;
        this.beosztas = beosztas;
    }
}
