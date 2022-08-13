using System;

namespace pavelantropov.qms_consoleapp.Helpers;

public class ConsoleHelper : IOutputHelper
{
	public void Print(string message)
	{
		Console.WriteLine(message);
	}
}