using CA24110403;
using System.Text;

List<Versenyzo> versenyzok = [];

using StreamReader sr = new("..\\..\\..\\src\\forras.txt", Encoding.UTF8);
while (!sr.EndOfStream) versenyzok.Add(new(sr.ReadLine()));

Console.WriteLine($"versenyzok szama: {versenyzok.Count}");

var f1 = versenyzok.Count(v => v.Kategoria == "25-29");
Console.WriteLine($"20-29 kategoriaban a versenyzok szama: {f1} fo");

var f2 = versenyzok.Average(v => 2014 - v.SzulEv);
Console.WriteLine($"versenyzok atlageletkora: {f2:0.00} ev");

var f3 = versenyzok.Sum(v => v.Idok["úszás"].TotalHours);
Console.WriteLine($"uszassal toltott ido: {f3:0.00} ora");

var f4 = versenyzok
    .Where(v => v.Kategoria == "elit")
    .Average(v => v.Idok["úszás"].TotalMinutes);
Console.WriteLine($"atlagos uszasi ido elit kategoriaban: {f4:0.00} perc");

var f5 = versenyzok
    .Where(v => v.Nem)
    .MinBy(v => v.OsszIdo);
Console.WriteLine($"gyoztes ferfi: {f5}");

var f6 = versenyzok.GroupBy(v => v.Kategoria).OrderBy(g => g.Key);
Console.WriteLine("kategoriankent a versenyt befejezok szama:");
foreach (var grp in f6)
{
    Console.WriteLine($"\t{grp.Key,11}: {grp.Count(),2}");
}

var f7 = versenyzok
    .GroupBy(v => v.Kategoria)
    .ToDictionary(g => g.Key, g => g.Average(v =>
        v.Idok["I. depó"].TotalMinutes +
        v.Idok["II. depó"].TotalMinutes))
    .OrderBy(kvp => kvp.Key);

Console.WriteLine("kategoriankent az atlagos depo ido:");
foreach (var kvp in f7)
{
    Console.WriteLine($"\t{kvp.Key, 11}: {kvp.Value:0.00} perc");
}

