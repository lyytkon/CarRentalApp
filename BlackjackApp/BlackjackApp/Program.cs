using System;
using System.Collections.Generic;

class Program
{
	static Random rng = new Random();

	static void Main()
	{
		Console.WriteLine("Blackjack");

		bool pelaaUudestaan = true;

		while (pelaaUudestaan)
		{
			Console.Clear(); // Tyhjentää konsolin ennen uutta peliä
			Console.WriteLine("Peataan BLACKJACK");
			Console.WriteLine("Tavoite: päästä lähelle 21 ylittämättä");

			Console.WriteLine("Paina Enter aloittaaksesi uuden pelin...");
			Console.ReadLine();

			PelaaYksiPeli();

			Console.Write("\nHaluatko pelata uudestaan? (k/e): ");
			string vastaus = Console.ReadLine()?.ToLower() ?? "e";
			pelaaUudestaan = (vastaus == "k");
			Console.WriteLine();
		}

		Console.WriteLine("Kiitos pelaamisesta!");
	}

	static void PelaaYksiPeli()
	{
		List<int> pelaaja = new List<int>();
		List<int> jakaja = new List<int>();

		// Alkujaot: 2 korttia pelaajalle, 2 jakajalle
		pelaaja.Add(NostaKortti());
		pelaaja.Add(NostaKortti());
		jakaja.Add(NostaKortti());
		jakaja.Add(NostaKortti());

		// Pelaajan vuoro
		bool pelaajaPelaa = true;
		while (pelaajaPelaa)
		{
			NaytaTilanne(pelaaja, jakaja, piilotaJakajanToinenKortti: true);

			int pelaajaSumma = LaskeSumma(pelaaja);
			if (pelaajaSumma > 21)
			{
				Console.WriteLine("Yli 21! Hävisit.");
				return;
			}

			Console.Write("Otatko kortin? (k = kyllä, e = en): ");
			string valinta = Console.ReadLine()?.ToLower() ?? "e";

			if (valinta == "k")
				pelaaja.Add(NostaKortti());
			else
				pelaajaPelaa = false;
		}

		// Jakajan vuoro: nostaa kunnes summa vähintään 17
		while (LaskeSumma(jakaja) < 17)
			jakaja.Add(NostaKortti());

		// Näytetään lopputilanne
		NaytaTilanne(pelaaja, jakaja, piilotaJakajanToinenKortti: false);

		int pSum = LaskeSumma(pelaaja);
		int jSum = LaskeSumma(jakaja);

		// Voittologiikka
		if (jSum > 21)
			Console.WriteLine("Jakaja meni yli 21. Voitit!");
		else if (pSum > jSum)
			Console.WriteLine("Voitit!");
		else if (pSum < jSum)
			Console.WriteLine("Hävisit.");
		else
			Console.WriteLine("Tasapeli.");
	}

	static int NostaKortti()
	{
		// 2-11, jossa 11 = ässä (tässä aina 11 yksinkertaisuuden vuoksi)
		int[] kortit = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
		return kortit[rng.Next(kortit.Length)];
	}

	static int LaskeSumma(List<int> kasi)
	{
		int summa = 0;
		foreach (int kortti in kasi)
			summa += kortti;

		// Ässä voi olla 11 tai 1: jos yli 21, muutetaan ässiä 11 -> 1
		int assia = kasi.FindAll(k => k == 11).Count;
		while (summa > 21 && assia > 0)
		{
			summa -= 10; // 11 -> 1
			assia--;
		}

		return summa;
	}

	static void NaytaTilanne(List<int> pelaaja, List<int> jakaja, bool piilotaJakajanToinenKortti)
	{
		Console.WriteLine("\n--- Tilanne ---");

		Console.Write("Sinun kortit: ");
		TulostaKasi(pelaaja);
		Console.WriteLine($" (summa: {LaskeSumma(pelaaja)})");

		Console.Write("Jakajan kortit: ");
		if (piilotaJakajanToinenKortti)
		{
			Console.Write($"{jakaja[0]}, ?");
			Console.WriteLine($" (näkyvä summa: {jakaja[0]})");
		}
		else
		{
			TulostaKasi(jakaja);
			Console.WriteLine($" (summa: {LaskeSumma(jakaja)})");
		}
	}

	static void TulostaKasi(List<int> kasi)
	{
		for (int i = 0; i < kasi.Count; i++)
		{
			Console.Write(kasi[i]);
			if (i < kasi.Count - 1) Console.Write(", ");
		}
	}
}