using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kleuren.Lib.Entities
{
    public class Kleur
    {
        public int Id { get; set; }
		
		private string naam;

		public string Naam
		{
			get { return  naam; }
			set 
			{
				if ((value.Trim()).Length < 3) 
					throw new Exception("Geef een kleurnaam van minstens 3 letters");
				else naam = value; 
			}
		}

		private int[] rgb;

		public int[] Rgb
		{
			get { return rgb; }
			set 
			{
				if (value.Length != 3 || !ZijnAlleRgbWaardenGeldig(value)) 
					throw new Exception("Geef 3 waarden door tussen 0 en 255");
				else rgb = value; 
			}
		}

		bool ZijnAlleRgbWaardenGeldig(int[] rgbWaarden)
		{
			bool geldig = true;
			foreach (int getal in rgbWaarden)
			{
				if (getal <0  || getal > 255)
				{
					geldig = false;
					break;
				}
			}
			return geldig;
		}

		public static int MaxId { get; private set; }

		public List<int> ToegekendeIds { get; set; } = new List<int>();

		public Kleur(string kleurNaam, int[] rgbWaarde, int id = 0)
		{
			Naam = kleurNaam;
			Rgb = rgbWaarde;
			if (id <= 0 || ToegekendeIds.Contains(id)) Id = ++MaxId;
			else Id = id;
		}

		public override string ToString()
		{
			string rgbCombinatie = String.Join(",", Rgb);
			string info = $"{Id} - {Naam}\n\tRGB: {rgbCombinatie}";
			return info;
		}

		public string GeefHexCode()
		{
			string hexCode = "#";
			foreach (int rgbWaarde in Rgb)
			{
				//rgb wordt omgezet naar hexadecimaal en er wordt een 0 voor geplaatst
				string hexRGB = "0" + rgbWaarde.ToString("X");
				//de laatste twee tekens van hexRGB worden behouden
				hexRGB = hexRGB.Substring(hexRGB.Length - 2, 2);
				hexCode += hexRGB;
			}
			return hexCode;
		}

		public Brush GeefBrush()
		{
			BrushConverter bc = new BrushConverter();
			Brush brush = (Brush)bc.ConvertFrom(GeefHexCode());
			return brush;
		}


	}

}
