using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HW8
{
    public class Menu
    {
        private Workers[] worker;

        public void Handler()
        {
            int pos = 0;
            for (;;)
            {
                Clear();
                WriteLine("1 - Добавить сотрудника");
                WriteLine("2 - Вывести полную информацию обо всех сотрудниках");
                WriteLine("3 - Найти всех менеджеров");
                WriteLine("5 - Выход");
                WriteLine("Выберите пункт: ");
                if (!Int32.TryParse(ReadLine(), out pos))
                {
                    continue;
                }
                if (pos == 1)
                {
                    AddWorker();
                }
                else if (pos == 2)
                {
                    ShowWorker(worker);
                }
                else if (pos == 3)
                {
                    ShowWorker(Sort(FindManager()));
                }
                else if (pos == 4)
                {
                    break;
                }
            }
        }

        private bool AddWorker()
        {
            Workers newWorker = new Workers();

            Clear();
            WriteLine("Введите имя: ");
            newWorker.Name = ReadLine();
            WriteLine("1 - Босс");
            WriteLine("2 - Менеджер");
            WriteLine("3 - Работники");

            WriteLine("Выберите должность: ");
            int vacancy;
            if (Int32.TryParse(ReadLine(), out vacancy))
            {
                if (vacancy < 1 || vacancy > 3)
                {
                    Wrong();
                    return false;
                }
                newWorker.vacancy = (Vacancy)vacancy - 1;
            }
            else
            {
                Wrong();
                return false;
            }

            WriteLine("Введите зарплату: ");
            int salary;
            if (Int32.TryParse(ReadLine(), out salary))
            {
                newWorker.Salary = salary;
            }
            else
            {
                Wrong();
                return false;
            }

            newWorker.WorkerData = InputDate();
            if (newWorker.WorkerData == null)
            {
                Wrong();
                return false;
            }

            AddWorkerToArray(newWorker);
            return true;
        }

        private void Wrong()
        {
            WriteLine("Неверные данные! Нажмите ENTER что бы продолжить.");
            ReadLine();
        }

        private int[] InputDate()
        {
            int[] date = new int[3];
            WriteLine("Введите дату приема на работу (дд.мм.гггг): ");
            string str = ReadLine();

            if (str.Length > 10 || str.Length < 10)
            {
                return null;
            }

            int hour;
            if (Int32.TryParse(str.Remove(0, 6), out hour))
            {
                date[2] = hour;
            }
            else
            {
                return null;
            }

            int month;
            string strMonth = str.Remove(5, 5);
            if (Int32.TryParse(strMonth.Remove(0, 3), out month))
            {
                date[1] = month;
            }
            else
            {
                return null;
            }

            int day;
            if (Int32.TryParse(str.Remove(2), out day))
            {
                date[0] = day;
            }
            else
            {
                return null;
            }
            return date;
        }

        private void AddWorkerToArray(Workers emp)
        {
            if (worker != null)
            {
                Workers[] newWorker = new Workers[worker.Length];
                for (int i = 0; i < worker.Length; i++)
                {
                    newWorker[i] = worker[i];
                }

                worker = new Workers[newWorker.Length + 1];

                for (int i = 0; i < newWorker.Length; i++)
                {
                    worker[i] = newWorker[i];
                }

                worker[worker.Length - 1] = emp;
            }
            else
            {
                worker = new Workers[1];
                worker[worker.Length - 1] = emp;
            }
        }

        private void ShowWorker(Workers[] employee)
        {
            if (employee != null)
            {
                Clear();
                foreach (Workers em in worker)
                {
                    WriteLine("Имя: " + em.Name);
                    WriteLine("Должность: {0}", em.vacancy.ToString());
                    WriteLine("Зарплата: " + em.Salary);
                    WriteLine("Дата приема на работу: {0}.{1}.{2} \n",
                        em.WorkerData[0], em.WorkerData[1], em.WorkerData[2]);
                }
                ReadLine();
            }
        }

        private Workers[] FindManager()
        {
            if (worker != null)
            {
                int ASalary = 0;
                int countClerk = 0;

                Clear();
                foreach (Workers em in worker)
                {
                    if (em.vacancy == Vacancy.Worker)
                    {
                        ASalary += em.Salary;
                        countClerk++;
                    }
                }
                if (countClerk != 0)
                {
                    ASalary /= countClerk;
                }

                int count = 0;
                foreach (Workers em in worker)
                {
                    if (em.vacancy == Vacancy.Manager && em.Salary > ASalary)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    Workers[] manager = new Workers[count];
                    for (int i = 0, j = 0; i < worker.Length; i++)
                    {
                        if (worker[i].vacancy == Vacancy.Manager && worker[i].Salary > ASalary)
                        {
                            manager[j++] = worker[i];
                        }
                    }
                    return manager;
                }
            }
            return null;
        }

        private Workers[] FindWorkerAfterBoss()
        {
            int[] dateBoss = null;
            foreach (Workers em in worker)
            {
                if (em.vacancy == Vacancy.Boss)
                {
                    dateBoss = em.WorkerData;
                    break;
                }
            }
            if (dateBoss != null)
            {
                int count = 0;
                foreach (Workers em in worker)
                {
                    if (em.WorkerData[2] > dateBoss[2])
                        count++;
                    else if (em.WorkerData[2] == dateBoss[2])
                    {
                        if (em.WorkerData[1] > dateBoss[1])
                            count++;
                        else if (em.WorkerData[1] == dateBoss[1])
                        {
                            if (em.WorkerData[0] > dateBoss[0])
                                count++;
                        }
                    }
                }
                if (count != 0)
                {
                    Workers[] resWorker = new Workers[count];
                    for (int i = 0, j = 0; i < worker.Length; i++)
                    {
                        if (worker[i].WorkerData[2] > dateBoss[2])
                            resWorker [j++] = worker[i];
                        else if (worker[i].WorkerData[2] == dateBoss[2])
                        {
                            if (worker[i].WorkerData[1] > dateBoss[1])
                                resWorker[j++] = worker[i];
                            else if (worker[i].WorkerData[1] == dateBoss[1])
                            {
                                if (worker[i].WorkerData[0] > dateBoss[0])
                                    resWorker[j++] = worker[i];
                            }
                        }
                    }
                    return resWorker;
                }
            }
            return null;
        }

        private Workers[] Sort(Workers[] emp)
        {
            for (int i = 0; i < emp.Length; i++)
            {
                for (int j = 0; j < emp.Length; j++)
                {
                    if (String.Compare(emp[i].Name, emp[j].Name) == -1)
                    {
                        Workers temporary = emp[i];
                        emp[i] = emp[j];
                        emp[j] = temporary;
                    }
                }
            }
            return emp;
        }
    }
}