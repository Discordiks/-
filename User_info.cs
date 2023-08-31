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
        public static string name_champ { get; set; } //запоминание названия чемпионата для последующей работы с ним
        public static string id_champ_combo { get; set; } //запоминание id чемпионата для последующей работы с ним
        public static string id_protocol { get; set; } //запоминание id добавленного протокола
        public static string id_role_protocol { get; set; } //запоминание id добавленной роли к протоколу
        public static string role_id_update { get; set; } //запоминание id изменённой роли к протоколу
        public static string id_credential { get; set; } //запоминание id добавленного полномочия
        public static string id_role_credential { get; set; } //запоминание id добавленной роли к полномочию
        public static string role_id_update_credential { get; set; } //запоминание id изменённой роли к полномочию


    }
}
