using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Util
{
    static class UI
    {
        public static void PrintMainTitle()
        {
            WriteLineBlue(
                @"
                   ____                      _      _   _       _     
                  / ___|___  _ __  ___  ___ | | ___| | | |_   _| |__  
                 | |   / _ \| '_ \/ __|/ _ \| |/ _ \ |_| | | | | '_ \ 
                 | |__| (_) | | | \__ \ (_) | |  __/  _  | |_| | |_) |
                  \____\___/|_| |_|___/\___/|_|\___|_| |_|\__,_|_.__/ 
                                                      
                "
            );
        }

        private static void SetTemporalColor(ConsoleColor color, Action print)
        {
            Console.ForegroundColor = color;
            print();
            Console.ResetColor();
        }

        #region Red printing
        public static void WriteLineRed(string text)
        {
            SetTemporalColor(ConsoleColor.Red, () => Console.WriteLine(text));
        }

        public static void WriteRed(string text)
        {
            SetTemporalColor(ConsoleColor.Red, () => Console.Write(text));
        }
        #endregion

        #region Blue printing
        public static void WriteLineBlue(string text)
        {
            SetTemporalColor(ConsoleColor.Blue, () => Console.WriteLine(text));
        }

        public static void WriteBlue(string text)
        {
            SetTemporalColor(ConsoleColor.Blue, () => Console.Write(text));
        }
        #endregion

        #region Green printing
        public static void WriteLineGreen(string text)
        {
            SetTemporalColor(ConsoleColor.Green, () => Console.WriteLine(text));
        }

        public static void WriteGreen(string text)
        {
            SetTemporalColor(ConsoleColor.Green, () => Console.Write(text));
        }
        #endregion
    }
}
