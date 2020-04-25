using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDOM
{
    public class Apartment
    {
        /// <summary>
        /// Этаж
        /// </summary>
        public int Florr;

        /// <summary>
        /// Количество этажей
        /// </summary>
        public int Num_Floors;

        /// <summary>
        /// Тип дома
        /// </summary>
        public string TypeHome;

        /// <summary>
        /// Количество комнат
        /// </summary>
        public int Num_Rooms;

        /// <summary>
        /// Общая площадь квартиры
        /// </summary>
        public int Total_Area;

        /// <summary>
        /// Жилая площадь квартиры
        /// </summary>
        public int LivingArea;

        /// <summary>
        /// Площадь кухни
        /// </summary>
        public int KitchenArea;

        /// <summary>
        /// Стоимость квартиры
        /// </summary>
        public int Price;

        public Apartment ()
        {

        }
    }
}
