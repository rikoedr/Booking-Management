using System.Diagnostics;

namespace API.Utilities;

public class Debugger
{
    public static void Message(string message)
    {
        Debug.WriteLine("Debugger " + message);
    }
}
