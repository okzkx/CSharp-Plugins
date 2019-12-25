using System;

namespace OKZKX.Util
{
    public class MyConsole
    {
        public static void Show<T>(T t)
        {
            Console.WriteLine(CSharpTools.ToString(t));
        }

        public static void Show<T>(T[,] ts)
        {
            for (int i = 0; i < ts.GetLength(0); i++)
            {
                for (int j = 0; j < ts.GetLength(1); j++)
                {
                    Console.Write(ts[i, j] + ",");
                }
                Console.WriteLine();
            }
        }
    }
}
