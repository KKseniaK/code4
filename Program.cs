using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isCorrect;
            int option = -1;
            bool exit = false;
            int[] arr1 = null; // место для массива
            
            while (!exit)
            {
                Console.Clear();

                //Вывод меню
                string[] menuOptions = { "1 - создать массив",
                                         "2 - напечатать массив",
                                         "3 - удалить все четные элементы",
                                         "4 - вставить К элементов в конец массива",
                                         "5 - переставить все положительные элементы в начало, отрицательные в конец",
                                         "6 - найти первый отрицательный элемент в массиве и посчитать кол-во сравнений",
                                         "7 - сортировка простым выбором",
                                         "8 - поиск введенного элемента в массиве",
                                         "9 - выход"  };
                foreach (string x in menuOptions)
                {
                    Console.WriteLine(x);
                }

                //Ввод позиции меню
                do
                {
                    Console.Write("Выберете дейсвие и введите соответвующий номер: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out option);
                    if ((option < 1 || option > 9) || !isCorrect)
                    {
                        Console.WriteLine("Ошибка при выборе пункта меню. Введите номер от 1 до 9");
                        isCorrect = false;
                    }
                } while (!isCorrect);
                Console.Clear();

                
                switch (option)
                {
                    case 1: // Создаем массив
                        Console.WriteLine("Выберите способ создания массива:");
                        Console.WriteLine("1 - с помощью датчика случайных чисел");
                        Console.WriteLine("2 - ввести массив с клавиатуры");
                        bool isCorrect2;
                        int wayOfCreating;

                        //проверка выбора 
                        do
                        {
                            isCorrect2 = int.TryParse(Console.ReadLine(), out wayOfCreating);
                            if (wayOfCreating < 1 || wayOfCreating > 2)
                            {
                                Console.WriteLine("Ошибка при выборе пункта меню. Введите номер 1 или 2");
                                isCorrect2 = false;
                            }
                        } while (!isCorrect2);

                        //Два способа создать массив
                        switch (wayOfCreating)
                        {
                            case 1: //массив из рандомных k штук элементов
                                Random rnd = new Random();
                                int len;
                                bool isLenCorrect;

                                Console.WriteLine("Вы выбрали создать массив датчиком случайных чисел");
                                //проверка адекватности длины массива
                                do
                                {
                                    Console.Write("\nВведите длину массива: ");
                                    isLenCorrect = int.TryParse(Console.ReadLine(), out len);
                                    if (len <= 0 || !isLenCorrect)
                                    {
                                        Console.WriteLine("\nОшибка при вводе длины массива, введите целое число больше нуля");
                                        isLenCorrect = false;
                                    }
                                } while (!isLenCorrect);

                                arr1 = new int[len];//создали массив длины k

                                //заполнили массив рандомными числами от -100 до 100
                                for (int i = 0; i < len; i++)
                                {
                                    arr1[i] = rnd.Next(-100, 100);
                                }

                                //вывод результатов кейса
                                Console.Write($"\nСгенерированный массив из {len} элементов: ");
                                foreach (int i in arr1) Console.Write(i + " ");
                                break;

                            case 2:
                                Console.WriteLine("\nВы выбрали ввести массив с клавиатуры");

                                //проверка адекватности длины массива
                                do
                                {
                                    Console.Write("\nВведите длину массива: ");
                                    isLenCorrect = int.TryParse(Console.ReadLine(), out len);
                                    if (len <= 0 || !isLenCorrect)
                                    {
                                        Console.WriteLine("\nОшибка при вводе длины массива, введите целое число больше нуля");
                                        isLenCorrect = false;
                                    }
                                } while (!isLenCorrect);

                                arr1 = new int[len]; //создали массив длины k

                                //цикл для записи элементов массива с клавиатуры
                                for (int i = 0; i < len; i++)
                                {
                                    int item;
                                    //проверка адекватности элемента массива
                                    do
                                    {
                                        Console.WriteLine($"Введите {i + 1} элемент массива");
                                        isLenCorrect = int.TryParse(Console.ReadLine(), out item);
                                        if (!isLenCorrect)
                                        {
                                            Console.WriteLine("\nОшибка при вводе элемента, введите целое число");
                                            isLenCorrect = false;
                                        }
                                    } while (!isLenCorrect);
                                    arr1[i] = item; //записываем в массив
                                }

                                //вывод результатов кейса
                                Console.Write($"\nВаш массив из {len} элементов: ");
                                foreach (int i in arr1) Console.Write(i + " ");
                                break;
                        }
                        break;

                    // печатаем массив
                    case 2:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            Console.Write("Ваш массив: ");
                            foreach (int i in arr1) Console.Write(i + " ");
                        }
                        break;

                    // удаляем все четные элементы
                    case 3:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            //для наглядности вывод исходного массива
                            Console.Write("Массив, над которым выполнялись дейсвия: ");
                            foreach (int i in arr1) Console.Write(i + " ");

                            int oddCount = 0; // кол-во нечетных элементов 
                            //проходим по массиву, считаем нечетные элементы
                            for (int i = 0; i < arr1.Length; i++)
                            {
                                if (arr1[i] % 2 != 0) oddCount++;
                            }

                            //если в массиве были нечетные элементы, то создадим новый массив длины oddCount
                            if (oddCount > 0)
                            {
                                int[] temp = new int[oddCount]; //создали массив
                                int j = 0;
                                //прошлись по старому, списали нечтные элементы
                                for (int i = 0; i < arr1.Length; i++)
                                {
                                    if (arr1[i] % 2 != 0) temp[j++] = arr1[i];
                                }
                                arr1 = temp; // унаследовали массив с этого кейса
                            }
                            else //если в исходном массиве не было нечетных элементов --> мы уничтожим все элементы 
                            {
                                Console.WriteLine("\nВ исходном массиве были только четные элементы\nТеперь массив пустой, создайте его снова");
                                //arr1 = new int[0];
                                arr1 = null; //-->пусть пользователь снова заполняет массив 
                            }
                            //если нечетные элементы были, обновленный массив выводиться на экран
                            if (oddCount > 0)
                            {
                                Console.Write("\nУдалили из массива все четные числа: ");
                                foreach (int i in arr1) Console.Write(i + " ");
                            }
                        }
                        break;
                    // вставляем К элементов в конец массива(сделать 2 способами с клавиатуры и рандомом)
                    case 4:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            Console.WriteLine("Как вы хотите добавить K значений в массив?");
                            Console.WriteLine("1 - я не знаю, что я хочу, давайте рандом");
                            Console.WriteLine("2 - я точно знаю, что я хочу добавить");
                            bool isCorrect3;
                            int wayOfAdding;

                            //проверка адекватности выбора пункта меню
                            do
                            {
                                isCorrect3 = int.TryParse(Console.ReadLine(), out wayOfAdding);
                                if (wayOfAdding < 1 || wayOfAdding > 2)
                                {
                                    Console.WriteLine("Ошибка при выборе пункта меню. Введите номер 1 или 2");
                                    isCorrect3 = false;
                                }
                            } while (!isCorrect3);

                            switch (wayOfAdding)
                            {
                                case 1: //добавляем в конец K штук рандомных элементов
                                    Random rnd = new Random();
                                    int k;
                                    bool isKCorrect;

                                    Console.WriteLine("Вы выбрали «я не знаю, что я хочу, давайте рандом»");

                                    //проверка адекватности для K
                                    do
                                    {
                                        Console.Write("\nВведите, какое кол-во чисел вы хотите добавить: ");
                                        isKCorrect = int.TryParse(Console.ReadLine(), out k);
                                        if (k <= 0 || !isKCorrect)
                                        {
                                            Console.WriteLine("\nОшибка при вводе длины массива, введите целое число больше нуля");
                                            isKCorrect = false;
                                        }
                                    } while (!isKCorrect);

                                    int[] arr4 = new int[arr1.Length + k]; //новый массив длины длина_массива_arr1 + K
                                    
                                    //заполняем новый массив
                                    for (int i = 0; i < arr4.Length; i++)
                                    {
                                        if (i < arr1.Length)
                                        {
                                            arr4[i] = arr1[i];
                                        } else
                                        {
                                            arr4[i] = rnd.Next(-100, 100);
                                        }
                                        
                                    }
                                    Console.Write($"\nДобавили в конец массива {k} сгенерерованных элемента: ");
                                    
                                    arr1 = arr4; // унаследовали массив с этого шага
                                    foreach (int i in arr1) Console.Write(i + " ");
                                    break;

                                case 2://добавляем в конец K элементов с клавиатуры
                                    Console.WriteLine("Вы выбрали «я точно знаю, что я хочу добавить»");

                                    //проверка адекватности для K
                                    do
                                    {
                                        Console.Write("\nВведите, какое кол-во чисел вы хотите добавить: ");
                                        isKCorrect = int.TryParse(Console.ReadLine(), out k);
                                        if (k <= 0 || !isKCorrect)
                                        {
                                            Console.WriteLine("\nОшибка при вводе длины массива, введите целое число больше нуля");
                                            isKCorrect = false;
                                        }
                                    } while (!isKCorrect);

                                    arr4 = new int[arr1.Length + k]; //новый массив длины длина_массива_arr1 + K

                                    //заполняем новый массив
                                    for (int i = 0; i < arr4.Length; i++)
                                    {
                                        if (i < arr1.Length) //списали сущесвующие элементы из arr1
                                        {
                                            arr4[i] = arr1[i];
                                        } else //запись новых элементов
                                        {
                                            int item;
                                            //проверка адекватности очередного элемента
                                            do
                                            {
                                                Console.WriteLine($"Введите {(i + 1) - arr1.Length} из {k} элементов, которые хотите добавить в массив: ");
                                                isKCorrect = int.TryParse(Console.ReadLine(), out item);
                                                if (!isKCorrect)
                                                {
                                                    Console.WriteLine("\nОшибка при вводе элемента, введите целое число");
                                                    isKCorrect = false;
                                                }
                                            } while (!isKCorrect);
                                            arr4[i] = item; //запись элемента
                                        }  
                                    }
                                    //вывод результатов кейса
                                    Console.Write($"\nДобавили в конец массива {k} своих элементов: ");
                                    arr1 = arr4; //унаследовали массив с этого шага
                                    foreach (int i in arr1) Console.Write(i + " ");
                                    break;
                            }
                        }
                        break;
                    // переставляем все положительные элементы в начало, отрицательные в конец
                    case 5:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            // Проверка массивов только из положительных/отрицательный или нулей
                            bool allPositive = true;
                            bool allNegative = true;
                            bool allZeros = true;

                            foreach (int num in arr1)
                            {
                                if (num > 0)
                                {
                                    allNegative = false;
                                    allZeros = false;
                                }
                                else if (num < 0)
                                {
                                    allPositive = false;
                                    allZeros = false;
                                }
                                else
                                {
                                    allPositive = false;
                                    allNegative = false;
                                }
                            }

                            //вывод соответствующей надписи
                            if (allPositive)
                            {
                                Console.WriteLine("Все элементы массива положительные.");
                            }
                            else if (allNegative)
                            {
                                Console.WriteLine("Все элементы массива отрицательные.");
                            }
                            else if (allZeros)
                            {
                                Console.WriteLine("Все элементы массива равны нулю.");
                            }
                            else //массив не принадлежит типам выше
                            {
                                int[] temparr = new int[arr1.Length]; //создади новый временный пустой массив длины как arr1
                                int j = 0; //индекс нового массива
                                
                                for (int i = 0; i < arr1.Length; i++)
                                {
                                    if (arr1[i] >= 0)
                                    {
                                        temparr[j] = arr1[i];
                                        j++;
                                    }
                                }
                                
                                for (int i = 0; i < arr1.Length; i++)
                                {
                                    if (arr1[i] < 0)
                                    {
                                        temparr[j] = arr1[i];
                                        j++;
                                    }
                                }
                                arr1 = temparr; //унаследовали массив с этого шага 
                            }
                            //вывод результатов кейса
                            Console.Write($"\nПереставили все положительные элементы в начало, отрицательные - в конец: ");
                            foreach (int i in arr1) Console.Write(i + " ");
                        }
                        break;
                    // найти первый отрицательный элемент в массиве и посчитать кол-во сравнений
                    case 6:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            int desiredIndex = -1; // для индекса
                            for (int i = 0; i < arr1.Length; i++)
                            {
                                if (arr1[i] < 0)
                                {
                                    desiredIndex = i; break;
                                };
                            }
                            if (desiredIndex < 0)
                            {
                                //Console.OutputEncoding = System.Text.Encoding.UTF8;
                                Console.WriteLine("В исходном массиве нет отрицательных чисел \\_(o_O)_/ \nВыводить нечего");
                            }
                            else
                            {
                                Console.WriteLine($"\nПервый отрицательный элемент в этом массиве был под номером {desiredIndex + 1}");
                                Console.WriteLine($"Чтобы его найти понадобилось {desiredIndex+1} сравнения(ий)");
                            }
                            
                        }
                        break;
                    // сортировака простым выбором
                    case 7:
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            for (int i = 0; i < arr1.Length - 1; i++)
                            {
                                int min = arr1[i];
                                int nomMin = i;
                                for (int j = i + 1; j < arr1.Length; j++)
                                {
                                    if (arr1[j] < min)
                                    {
                                        min = arr1[j];
                                        nomMin = j;
                                    }
                                }
                                arr1[nomMin] = arr1[i];
                                arr1[i] = min;
                            }
                            Console.Write($"Наш массив с сортировкой простым выбором: ");
                            foreach (int i in arr1) Console.Write(i + " ");
                        }
                           
                        break;
                    // поиск введенного с клавиатуры элемента в массиве
                    case 8:
                        Console.WriteLine("Вы выбрали поиск введенного с клавиатуры элемента в массиве");
                        if (arr1 == null)
                        {
                            Console.WriteLine("Массив еще не создан. Сначала создайте массив.");
                        }
                        else
                        {
                            int searchValue; //для искомого элемента
                            int searchIndex = -1; //для индекса искомого элемента
                            int left = 0; // Левая граница поиска
                            int right = arr1.Length - 1; // Правая граница поиска
                            int index = 0; // индекс, указывающий середину отрезка
                            int comparisons = 0; // кол-во сравнений
                            do
                            {
                                Console.Write("\nВведите элемент, который хотите найти в массиве: ");
                                isCorrect = int.TryParse(Console.ReadLine(), out searchValue);
                                if (!isCorrect)
                                {
                                    Console.WriteLine("\nОшибка при вводе элемента массива, введите целое число");
                                }
                            } while (!isCorrect);

                            // для сортировки тот же самый простой выбор
                            for (int i = 0; i < arr1.Length - 1; i++)
                            {
                                int min = arr1[i];
                                int nomMin = i;
                                for (int j = i + 1; j < arr1.Length; j++)
                                {
                                    if (arr1[j] < min)
                                    {
                                        min = arr1[j];
                                        nomMin = j;
                                    }
                                }
                                arr1[nomMin] = arr1[i];
                                arr1[i] = min;
                            }

                            while (left <= right)
                            {
                                comparisons++;
                                index = (right + left) / 2; ///Узнаем середину отрезка, округляем в меньшую сторону.

                                // Если мы нашли искомый элемент, записываем результат и выходим из цикла.
                                if (arr1[index] == searchValue)
                                {
                                    searchIndex = index;
                                    break;
                                }
                                // Меняем границы.
                                if (arr1[index] < searchValue)
                                {
                                    left = index + 1;
                                }
                                else
                                {
                                    right = index - 1;
                                }
                            }
                            Console.Write("\nВаш массив: ");
                            foreach (int i in arr1) Console.Write(i + " "); 
                            if (searchIndex < 0)
                            {
                                Console.WriteLine($"\nВ массиве нет элемента {searchValue}");
                            } else
                            {
                                Console.WriteLine($"\nВ масииве есть элемент {searchValue}, он находися по номером {searchIndex + 1}, чтобы его найти понадобилось {comparisons} сравнения(й)");
                            }
                        }
                        break;
                    // выходим
                    case 9:
                        Console.WriteLine("Выход...");
                        exit = true; // Завершаем программу
                        break;

                }
                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            } 

        }
    }
}
