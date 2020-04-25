using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentLib
{
    public class Apartment2
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
        /// Год постройки
        /// </summary>
        public int Year;

        /// <summary>
        /// Наличие балкона
        /// </summary>
        public bool isBalcony;

        /// <summary>
        /// Район
        /// </summary>
        public string District;

        /// <summary>
        /// Стоимость квартиры
        /// </summary>
        public int Price;

        public Apartment2()
        {
            Florr = 0;
            Num_Floors = 0;
            TypeHome = "";
            Num_Rooms = 0;
            Total_Area = 0;
            LivingArea = 0;
            KitchenArea = 0;
            Price = 0;
        }

        public Apartment2(int _Florr, int _Num_Floors, string _TypeHome, int _Num_Rooms, int _Total_Area, int _LivingArea, int _KitchenArea, int _Price)
        {
            Florr = _Florr;
            Num_Floors = _Num_Floors;
            TypeHome = _TypeHome;
            Num_Rooms = _Num_Rooms;
            Total_Area = _Total_Area;
            LivingArea = _LivingArea;
            KitchenArea = _KitchenArea;
            Price = _Price;
        }

        /// <summary>
        /// Добавить запись в таблицу Apartment
        /// </summary>
        /// <returns></returns>
        public bool insertApartment()
        {
            DBConnection conn = new DBConnection();
            try
            {
                using (SqlConnection myConnection = new SqlConnection())
                {
                    conn.Open(myConnection);
                    string sql = "INSERT INTO Apartment([Florr], [Num_Floors], [TypeHome],[Num_Rooms], [Total_Area],[LivingArea], [KitchenArea], [Price]) VALUES (@Florr, @Num_Floors, @TypeHome,@Num_Rooms, @Total_Area, @LivingArea, @KitchenArea, @Price)";
                    SqlCommand cmd = new SqlCommand(sql, myConnection);

                    cmd.Parameters.AddWithValue("Florr", Florr);
                    cmd.Parameters.AddWithValue("Num_Floors", Num_Floors);
                    cmd.Parameters.AddWithValue("TypeHome", TypeHome);
                    cmd.Parameters.AddWithValue("Num_Rooms", Num_Rooms);
                    cmd.Parameters.AddWithValue("Total_Area", Total_Area);
                    cmd.Parameters.AddWithValue("LivingArea", LivingArea);
                    cmd.Parameters.AddWithValue("KitchenArea", KitchenArea);
                    cmd.Parameters.AddWithValue("Price", Price);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
        }
        
        public bool CheckApartment()
        {
            DBConnection conn = new DBConnection();
            try
            {
                using (SqlConnection myConnection = new SqlConnection())
                {
                    conn.Open(myConnection);
                    string query = "SELECT top 1 1 FROM Apartment WHERE Apartment.Florr = " + Florr.ToString() + " and Num_Floors = " + Num_Floors.ToString() + " and TypeHome = '" + TypeHome + "' and Num_Rooms = " + Num_Rooms.ToString() + " and Total_Area = " + Total_Area.ToString() + " and LivingArea = " + LivingArea.ToString() + " and KitchenArea = " + KitchenArea.ToString() + " and Price = " + Price.ToString();

                    SqlCommand command = new SqlCommand(query, myConnection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return Convert.ToInt32(reader[0]) == 1 ;
                    }
                    reader.Close();
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
            return false;
        }
        public void Clear()
        {
            Florr = 0;
            Num_Floors = 0;
            TypeHome = "";
            Num_Rooms = 0;
            Total_Area = 0;
            LivingArea = 0;
            KitchenArea = 0;
            Price = 0;
        }
    }
}
