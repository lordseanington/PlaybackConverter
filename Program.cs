using System;
using System.Collections.Generic;
using static PlaybackConverter.PlaybackData;

namespace PlaybackConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            string operation;
            string fileIn;
            string fileOut;
            if (args.Length < 3)
            {
                Console.WriteLine("Please enter the operation (d= Decompress Playback to XML, c = Compress XML to Playback)");
                operation = Console.ReadLine();

                Console.WriteLine("Please enter the input file");
                fileIn = Console.ReadLine();

                Console.WriteLine("Please enter the output file");
                fileOut = Console.ReadLine();
            }
            else
            {
                operation = args[0];
                fileIn = args[1];
                fileOut = args[2];
            }


            if (operation == "d")
            {
                List<ChaserState> list = Import(fileIn);
                if (list == null)
                    Console.WriteLine("Failed to load file " + fileIn);
                else
                    ExportXML(list, fileOut);

            }
            else
            if (operation == "c")
            {
                List<ChaserState> list = ImportXML(fileIn);
                if (list == null)
                    Console.WriteLine("Failed to load XML file " + fileIn);
                else
                    Export(list, fileOut);
            }
            else
                Console.WriteLine("Unknown operation entered: " + operation);
        }
    }
}
