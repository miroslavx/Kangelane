using System;
using System.Collections.Generic;
using System.IO;

public class Kangelane
{
    private string _nimi;
    private string _asukoht;

    public Kangelane(string nimi, string asukoht)
    {
        _nimi = nimi;
        _asukoht = asukoht;
    }

    public string Nimi
    {
        get => _nimi;
        set => _nimi = value;
    }

    public string Asukoht
    {
        get => _asukoht;
        set => _asukoht = value;
    }    public virtual int Paasta(int ohus) => (int)Math.Round(ohus * 0.95);
    public virtual string Vormiriietus() => "Tavaline vormiriietus";
    public virtual string Tervitus() => $"Tere, ma olen {_nimi} ja kaitseks {_asukoht}!";
    public virtual string MissiooniStaatus() => "Saadaval";
    public override string ToString() => $"{_nimi}, asukoht: {_asukoht}";
}
public class SuperKangelane : Kangelane
{
    private double _osavus;
    private static Random random = new Random();
    public SuperKangelane(string nimi, string asukoht) : base(nimi, asukoht)
    {
        _osavus = Math.Round(random.NextDouble() * 4 + 1, 1);
    }
    public override int Paasta(int ohus) => (int)Math.Round(ohus * (0.95 + _osavus / 100));
    public override string Vormiriietus() => "Vinge superkostüüm";
    public override string Tervitus() => $"Ma olen suurkangelane {Nimi} ja minu osavus on {_osavus}!";
    public override string MissiooniStaatus() => "Juba missioonil";
    public override string ToString() => $"{base.ToString()}, osavus: {_osavus}";
}

public class Program
{
    public static List<Kangelane> kangelased = new List<Kangelane>();

    public static void LoeKangelasedFailist(string failinimi)
    {
        foreach (var rida in File.ReadAllLines(failinimi))
        {
            var osad = rida.Split('/');
            if (osad.Length != 2) continue;

            var nimi = osad[0].Trim();
            var asukoht = osad[1].Trim();

            if (nimi.Contains("*"))
            {
                kangelased.Add(new SuperKangelane(nimi.Replace("*", "").Trim(), asukoht));
            }
            else
            {
                kangelased.Add(new Kangelane(nimi, asukoht));
            }
        }
    }
    public static void Main()
    {LoeKangelasedFailist("andmed.txt");
        Kangelane tavaline = null;
        SuperKangelane super = null;
        foreach (var k in kangelased)
        {
            if (k is SuperKangelane s)
            {
                super = s;
                break;
            }
        }

        foreach (var k in kangelased)
        {
            if (k is not SuperKangelane)
            {
                tavaline = k;
                break;
            }
        }

        Console.WriteLine("Tavaline kangelane:");
        Console.WriteLine($"Päästetud inimesi: {tavaline.Paasta(1000)}");
        Console.WriteLine(tavaline);
        Console.WriteLine(tavaline.Vormiriietus());
        Console.WriteLine(tavaline.Tervitus());
        Console.WriteLine(tavaline.MissiooniStaatus());
        Console.WriteLine();
        Console.WriteLine("Superkangelane:");
        Console.WriteLine($"Päästetud inimesi: {super.Paasta(1000)}");
        Console.WriteLine(super);
        Console.WriteLine(super.Vormiriietus());
        Console.WriteLine(super.Tervitus());
        Console.WriteLine(super.MissiooniStaatus());
    }
}
