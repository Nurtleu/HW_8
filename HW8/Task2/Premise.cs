using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task2
{
    public class Premise
    {
        private List<Student> student = new List<Student>();
        private int MinimumSalary = 250000;
        public void Handler()
        {
            int pos = 0;
            for (;;)
            {
                Clear();
                WriteLine("1 - Добавить студента");
                WriteLine("2 - Список студентов");
                WriteLine("3 - Выход");
                WriteLine("Выберите пункт: ");
                if (!Int32.TryParse(ReadLine(), out pos))
                {
                    continue;
                }
                if (pos == 1)
                {
                    AddStudent();
                }
                else if (pos == 2)
                {
                    ShowStudent();
                }
                else if (pos == 3)
                {
                    break;
                }
            }
        }

        private void ShowStudent()
        {
            student.Sort();
            Clear();
            int pos = 0;
            foreach (Student student in student)
            {
                WriteLine("{0} Место", ++pos);
                WriteLine("Фамилия: {0}", student.Surname);
                WriteLine("Имя: {0}", student.Name);
                WriteLine("Отчество: {0}", student.Patronymic);
                WriteLine("Пол: {0}", student.sex);
                WriteLine("Группа: {0}", student.Group);
                WriteLine("Средний балл: {0}", student.Score);
                WriteLine("Доход на члена семьи: {0}", student.FamilyIncome);
                WriteLine("Форма обучения: {0}\n", student.form);
            }
            ReadLine();
        }

        private void Wrong()
        {
            WriteLine("Неверные данные! Нажмите ENTER что бы продолжить.");
            ReadLine();
        }

        private void AddStudent()
        {
            Student newStudent = new Student();

            Clear();

            WriteLine("Введите имя: ");
            newStudent.Name = ReadLine();

            WriteLine("1 - Мужской");
            WriteLine("2 - Женский");
            WriteLine("Выберите пол: ");
            int sex;
            if (Int32.TryParse(ReadLine(), out sex))
            {
                if (sex < 1 || sex > 2)
                {
                    Wrong();
                    return;
                }
                newStudent.sex = (Sex)sex - 1;
            }
            else
            {
                Wrong();
                return;
            }

            WriteLine("1 - Очная");
            WriteLine("2 - Заочная");
            WriteLine("Выберите форму обучения: ");
            int form;
            if (Int32.TryParse(ReadLine(), out form))
            {
                if (form < 1 || form > 2)
                {
                    Wrong();
                    return;
                }
                newStudent.form = (FormOfTraining)form - 1;
            }
            else
            {
                Wrong();
                return;
            }

            WriteLine("Введите название группы: ");
            newStudent.Group = ReadLine();

            WriteLine("Введите средный балл: ");
            int score;
            if (!Int32.TryParse(ReadLine(), out score))
            {
                Wrong();
                return;
            }
            newStudent.Score = score;

            WriteLine("Введите доход на члена семьи: ");
            int incone;
            if (!Int32.TryParse(ReadLine(), out incone))
            {
                Wrong();
                return;
            }
            newStudent.FamilyIncome = incone;

            student.Add(newStudent);
        }
    }
}