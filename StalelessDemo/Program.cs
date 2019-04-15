using System;

namespace StalelessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var onOffSwitch = new FsmOnOff(FsmOnOff.OFF_State);

            Console.WriteLine("Press '-' , '=' key to toggle the switch.  'x' key will exit the program.");

            while (true)
            {
                Console.WriteLine(onOffSwitch.DisplayCurrentState());

                var pressedKey = Console.ReadKey().KeyChar;

                if (pressedKey == 'x') { break; }

                try
                {
                    onOffSwitch.Input(pressedKey);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                }
            }
        }
    }
}
