using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int cycleStep = 7;
            int upperLimit = 96;
            int bottomLimit = 5;
            
            Console.WriteLine($"Все мы знаем, что {cycleStep} - это счастливое число. Но мало кто знает, что число {bottomLimit} - это символ человека и его устремлений. Так давай же сделаем следующее: ");

            for(int i = bottomLimit; i <= upperLimit; i += cycleStep)
                Console.Write(i + " ");

            Console.WriteLine($"\nПодожди-ка, ты что, использовал {bottomLimit} как первое число, а потом каждый раз прибавлял {cycleStep}? ГЕНИЙ!!!");
        }
    }
}