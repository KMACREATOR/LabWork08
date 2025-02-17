using System;
using System.IO;
using System.Xml.Serialization;
using AnimalLibrary;

class main
{
    static void Main()
    {
        // Создание объекта коровы с заданными свойствами
        Animal cow = new Cow { Name = "Буренка", Country = "Россия", HideFromOtherAnimals = false };

        // Создание сериализатора для типа Cow
        XmlSerializer serializer = new XmlSerializer(typeof(Cow));

        // Сериализация объекта коровы в XML и запись в файл
        using (FileStream fs = new FileStream("animals.xml", FileMode.Create))
        {
            serializer.Serialize(fs, cow);
        }

        // Вывод сообщения о завершении сериализации
        Console.WriteLine("XML файл с животным создан.");

        // Десериализация объекта из XML-файла
        using (FileStream fs = new FileStream("animals.xml", FileMode.Open))
        {
            Animal deserializedAnimal = (Animal)serializer.Deserialize(fs);

            // Вывод информации о десериализованном объекте
            Console.WriteLine($"Имя: {deserializedAnimal.Name}, Страна: {deserializedAnimal.Country}, Любимая еда: {deserializedAnimal.GetFavouriteFood()}, Классификация: {deserializedAnimal.GetClassificationAnimal()}");
        }
    }
}