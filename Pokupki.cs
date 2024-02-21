using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//добавить выбор размерности покупки-шт/кг/г/л
namespace SpisokPokupok
{
    internal class Pokupki
    {
        string nazvanie;
        int kolichestvo;
        int cena;

        public Pokupki(string _nazvanie,int _kolichestvo,int _cena)
        {
            nazvanie = _nazvanie;
            kolichestvo = _kolichestvo;
            cena = _cena;
        }
        public int SummaPokupki()
        {
            return kolichestvo * cena;
        }
        public string ShowInfo()
        {
            string str = nazvanie+"   "+kolichestvo.ToString()+"шт./кг.   "+cena.ToString()+"руб.";
            return str;
        }
    }
}
