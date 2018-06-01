using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Util
{
    static class Ui
    {

        /// <summary>
        /// Default prompt to be printed to the user. Can be changed through a command and will be 
        /// loaded from the settings if it was overriden.
        /// </summary>
        public static string DefaultPrompt { get; set; } = "> ";

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

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void Write(string text)
        {
            Console.Write(text);
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void WritePrompt()
        {
            NewLine();
            Console.Write(DefaultPrompt);
        }

        public static void PrintError(string error)
        {
            SetTemporalColor(ConsoleColor.Red, () => WriteLine(error));
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

        #region Cyan printing
        public static void WriteLineCyan(string text)
        {
            SetTemporalColor(ConsoleColor.Cyan, () => Console.WriteLine(text));
        }

        public static void WriteCyan(string text)
        {
            SetTemporalColor(ConsoleColor.Cyan, () => Console.Write(text));
        }
        #endregion
    }
}
