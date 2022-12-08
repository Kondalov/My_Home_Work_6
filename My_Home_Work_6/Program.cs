using System.Globalization;

while (true)
{
    Console.WriteLine("Выберите действие:\n0 - создать файл\n1 - прочитать файл\n2 - добавить сотрудника\n3 - выход");
    string menu = Console.ReadLine();
    Menu(menu);
}

void Menu(string menu)
{
    switch (menu)
    {
        case "0":
            CreateFail();
            break;
        case "1":
            if (File.Exists(@"Skillbox_file.txt"))
            {
                PrintFile();
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Файл не существует!");
                Thread.Sleep(1000);
            }
            break;
        case "2":
            AddFile(AddAbout());
            Thread.Sleep(1000);
            break;
        case "3":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Ошибка ввода!\nПовторите ввод...");
            break;
    }
}

string AddAbout()
{
    int id;
    byte age;
    byte height;
    string name;
    string city;
    string inputDate;
    DateTime birthDay;

    Console.WriteLine("Пожалуйста, заполните информацию о себе: ");

    string[] about = { "ФИО", "Возраст", "Рост", "Дата рождения", "Город проживания" };

    string[] lines = File.ReadAllLines(@"Skillbox_file.txt");
    id = lines.Length + 1;

    DateTime dateTime = DateTime.Now;

    Console.WriteLine(about[0]);
    name = Console.ReadLine();

    Console.WriteLine(about[1]);
    while (!byte.TryParse(Console.ReadLine(), out age))
    {
        Console.WriteLine("Ошибка ввода!\nПовторите ввод...");
    }

    Console.WriteLine(about[2]);
    while (!byte.TryParse(Console.ReadLine(), out height))
    {
        Console.WriteLine("Ошибка ввода!\nПовторите ввод...");
    }

    Console.WriteLine(about[3]);
    do
    {
        Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
        inputDate = Console.ReadLine();
    }
    while (!DateTime.TryParseExact(inputDate, "dd.MM.yyyy", null, DateTimeStyles.NoCurrentDateDefault, out birthDay));
    Console.WriteLine("Ты родился " + $"{birthDay.ToString("d")}");

    Console.WriteLine(about[4]);
    city = Console.ReadLine();

    string result = $"{id}#{dateTime}#{name}#{age}#{height}#{birthDay.ToString("d")}#{city}" + "\n";

    return result;

}

void PrintFile()
{
    string line;

    using (StreamReader readFile = new StreamReader(@"Skillbox_file.txt"))
    {
        while ((line = readFile.ReadLine()) != null)
        {
            string[] lines = line.Split("#");

            for (int i = 0; i < lines.Length; i++)
            {
                Console.Write(lines[i] + " ");
            }
            Console.WriteLine();
        }
        readFile.Close();
    }
}

void AddFile(string about)
{
    using (StreamWriter streamWriter = new StreamWriter(@"Skillbox_file.txt", true))
    {
        streamWriter.Write(about);
    }
}

void CreateFail()
{
    try
    {
        if (File.Exists(@"Skillbox_file.txt") == true)
        {
            Console.WriteLine("Файл создан ранее...");
        }

        else
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Skillbox_file.txt");

            using (FileStream file = File.Create(path))
            file.Close();
            Thread.Sleep(1000);
            Console.WriteLine("File created!\nПуть файла ===" + Environment.CurrentDirectory, path);
        }

    }
    catch (Exception errorpath)
    {
        Console.WriteLine("Процесс не может получить доступ к файлу 'Skillbox_file.txt, потому что он используется другим процессом" + errorpath.Message);
        return;
    }
}