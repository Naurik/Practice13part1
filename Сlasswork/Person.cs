using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.Serialization.Formatters.Soap;

namespace Сlasswork_On_Serializable
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string BirthYear { get; set; }
        public void ShowPerson()
        {
            Console.WriteLine("Имя: {0}", Name);
            Console.WriteLine("Фамилия: {0}", Surname);
            Console.WriteLine("Телефон: {0}", Phone);
            Console.WriteLine("Год рождения: {0}", BirthYear);
            Console.WriteLine("||||||||||||||||||||||||");
        }
    }
}
