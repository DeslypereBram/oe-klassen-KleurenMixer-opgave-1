using Kleuren.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kleuren.Lib.Services
{
    public class KleurenBeheer
    {
        const int indexRood = 0;
        const int indexGroen = 1;
        const int indexBlauw = 2;

        public static List<Kleur> Kleuren { get; set; } = new List<Kleur>();

        public static void MaakVoorbeeldKleuren()
        {
            int[] rgbRood = new int[3];
            rgbRood[indexRood] = 255;
            rgbRood[indexGroen] = 0;
            rgbRood[indexBlauw] = 0;
            Kleur rood = new Kleur("Red", rgbRood);
            Kleur groen = new Kleur("GrasKleur", new int[] { 0, 255, 0 });
            Kleur blauw = new Kleur("Zeekleur", new int[] { 0, 0, 255 },5);

            Kleuren = new List<Kleur> { rood, groen, blauw };
        }

        static bool HeeftBestaandeIdInKleuren(Kleur teChecken)
        {
            bool gevonden = false;
            foreach (Kleur kleurke in Kleuren)
            {
                if (teChecken.Id == kleurke.Id)
                {
                    gevonden = true;
                    break;
                }
            }
            return gevonden;
        }

        static int GeefIndexInKleuren(Kleur teChecken)
        {
            int index = -1;
            for (int i = 0; i < Kleuren.Count; i++)
            {
                if(Kleuren[i].Id == teChecken.Id)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public static void SlaOp(Kleur opTeSlaan)
        {
            if (opTeSlaan == null) throw new Exception("Geef een geldige kleur door");
            //Bestaat de kleur al of is het een nieuwe kleur
            if (HeeftBestaandeIdInKleuren(opTeSlaan))
            {
                //Code om kleur aan te passen
                int indexKleur = GeefIndexInKleuren(opTeSlaan);
                Kleuren[indexKleur] = opTeSlaan;
            }
            else
            {
                //Code om nieuwe kleur toe te voegen
            }
        }
    }
}
