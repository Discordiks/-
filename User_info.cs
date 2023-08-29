using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Проект
{
    static class User_info
    {
        public static string user_id { get; set; }
        public static string user_ima { get; set; }
        public static string user_role { get; set; }
        public static string title_champ { get; set; } //запоминание id добавленного чемпионата
        public static string title_id { get; set; } //запоминание id добавленного скилла к чемпионату
        public static string skills_id_update { get; set; } //запоминание id изменённого скилла к чемпионату
        public static string championship_delete { get; set; } //запоминание предыдущего fio у главного эксперта в users 
    }
}
