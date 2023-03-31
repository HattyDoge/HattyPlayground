using System.IO.Enumeration;

namespace HattyTyper
{
	// TO DO: Percentage of errors, Number of mistakes, Average mistakes per minute, Total time, Average Number of Types per second
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine("Press r or R when ready to start typing");
			char inputChar;
			do
			{
				inputChar = Console.ReadKey().KeyChar;
			} while (inputChar != 'r' && inputChar != 'R');
			Console.Clear();
			string fileName = "..\\..\\..\\Phrases.txt";
			string[] phrases;
			int phrasesIndex = 0;
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
					sr.ReadLine();
					phrasesIndex++;
                }
                sr.Close();
            }
			phrases = new string[phrasesIndex];
            using (StreamReader sr = new StreamReader(fileName))
            {
				phrasesIndex = 0;
                while (!sr.EndOfStream)
                {
					phrases[phrasesIndex] = sr.ReadLine();
					phrasesIndex++;
                }
                sr.Close();
            }

            int errors = 0;
			int numberCharTotal = 0;
			double totalTime = 0.0;

			for (int i = 0; i < phrases.Length; i++)
			{
				Console.WriteLine(phrases[i]);
				Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
				DateTime before = DateTime.Now;
				for (int j = 0; j < phrases[i].Length; j++)
				{
					inputChar = Console.ReadKey().KeyChar;
					numberCharTotal++;

					if (inputChar != phrases[i][j])
					{

                        if (inputChar == '\r')
                        {
                            
                            Console.ForegroundColor = ConsoleColor.Red;
							Console.SetCursorPosition(Console.CursorLeft + j, Console.CursorTop); // dovrebbe riportarlo alla posizione di quando si è fatto l'invio
                            Console.Write(inputChar);
                            Console.ForegroundColor = ConsoleColor.White;
                            j--;
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
						Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
						Console.Write(inputChar);
						Console.ForegroundColor = ConsoleColor.White;

						errors++;
						
                        
						if (inputChar == '\b') {
                            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop); // cosi non si sfasa la scrittura
                            j--;
                        }
                        if (inputChar == ' ')
						{
							Console.BackgroundColor = ConsoleColor.Red;
							Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
							Console.Write(inputChar);
							Console.BackgroundColor = ConsoleColor.Black;

						}
					}
					else
					{
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(inputChar);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
				}

				TimeSpan diff = DateTime.Now.Subtract(before);
				totalTime += diff.TotalSeconds;

				Console.SetCursorPosition(0, 0);
				Console.Clear();
			}
			
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(	$"Number of mistakes you made {errors} " +
								$"\nAverage mistakes/pm {errors / (totalTime / 60)} pm" +
								$"\nPercentage of errors {(double)errors / numberCharTotal * 100.0}%");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(	$"Number of Types per second {numberCharTotal/totalTime} s" +
								$"\nAccuracy {100.0 - ((double)errors / numberCharTotal * 100.0)}%");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(	$"Number of total character {numberCharTotal}");
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}