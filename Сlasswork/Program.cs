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
    class Program
    {
        static void Main(string[] args)
        {
            /*Из csv файла (имя; фамилия; телефон; год рождения) прочитать записи в коллекцию. 
             * Каждая запись коллекции должна иметь тип Person. 
             * Сериализовать коллекцию с помощью класса SoapFormatter и сохранить в файл.*/

            Random random = new Random();

            char[] name;
            char[] surname;
            int[] phone;
            int day;
            int month;
            int year;
            string birthDay;
            string phoneNum = "";

            Console.Write("Введите количество людей для включения в список: ");
            int countPerson = int.Parse(Console.ReadLine());
            List<Person> people = new List<Person>(countPerson);
            Person person;

            for (int j = 0; j < countPerson; j++)
            {
                int nameLength = random.Next(4, 9);
                name = new char[nameLength];

                for (int i = 0; i < name.Length; i++)
                {
                    name[i] = (char)random.Next(97, 122);
                }
                name[0] = char.ToUpper(name[0]);

                int surnameLength = random.Next(5, 15);

                surname = new char[surnameLength];

                for (int i = 0; i < surname.Length; i++)
                {
                    surname[i] = (char)random.Next(97, 122);
                }
                surname[0] = char.ToUpper(surname[0]);

                int phoneLength = 10;

                phone = new int[phoneLength];

                phone[0] = 8;
                phone[1] = 7;
                for (int i = 2; i < phone.Length; i++)
                {
                    phone[i] = random.Next(0, 9);
                }
                for (int i = 0; i < phone.Length; i++)
                {
                    phoneNum += phone[i].ToString();
                }

                day = random.Next(1, 30);
                month = random.Next(1, 12);
                year = random.Next(1975, 2019);

                birthDay = day.ToString() + "." + month.ToString() + "." + year.ToString();
                if (day < 10)
                {
                    birthDay = "0" + day.ToString() + "." + month.ToString() + "." + year.ToString();
                }

                if (month < 10)
                {
                    birthDay = day.ToString() + "." + "0" + month.ToString() + "." + year.ToString();
                }

                person = new Person()
                {
                    Name = new string(name),
                    Surname = new string(surname),
                    Phone = phoneNum,
                    BirthYear = birthDay
                };

                people.Add(person);
                phoneNum = "";
            }

            foreach (var pers in people)
            {
                pers.ShowPerson();
            }

            string csvPath = @"person.csv";
            using (StreamWriter sw = new StreamWriter(csvPath, false, System.Text.Encoding.Default))
            {
                foreach (var p in people)
                {
                    sw.Write(p.Name + ",");
                    sw.Write(p.Surname + ",");
                    sw.Write(p.Phone + ",");
                    sw.WriteLine(p.BirthYear);
                }

            }

            List<Person> people2 = new List<Person>();
            Person person2;

            using (TextFieldParser tfp=new TextFieldParser(csvPath))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                while (!tfp.EndOfData)
                {

                    string[] fields=tfp.ReadFields();

                    person2 = new Person()
                    {
                        Name = fields[0],
                        Surname = fields[1],
                        Phone = fields[2],
                        BirthYear = fields[3]
                    };
                    people2.Add(person2);
                }
            }

            foreach (var pers2 in people2)
            {
                pers2.ShowPerson();
            }

            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream("person.soap", FileMode.OpenOrCreate))
            {
                foreach (var p in people2)
                {
                     formatter.Serialize(fs, p);
                }
            }

            Console.ReadLine();

        }

    }

}
