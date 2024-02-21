
namespace SpisokPokupok
{
    internal class Pokupki
    {
        string nazvanie;
        int kolichestvo;
        string mera;
        int cena;

        public Pokupki(string _nazvanie, int _kolichestvo, string _mera, int _cena)
        {
            nazvanie = _nazvanie;
            kolichestvo = _kolichestvo;
            mera = _mera;
            cena = _cena;
        }
        public int SummaPokupki()
        {
            return kolichestvo * cena;
        }
        public string ShowInfo()
        {
            //string str = nazvanie + "   " + kolichestvo.ToString() + mera + " " + cena.ToString() + "руб.";
            string str = $"{nazvanie}  {kolichestvo.ToString()}{mera} {cena.ToString()}руб.";
            return str;
        }
    }
}
