namespace headlock
{
    using System;
    using model;
    using processes;

    internal class Program
    {
        private static Process _process;

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0 && args[0] == "-v")
                {
                    _process = new ProcessToVerifyHeadlock();
                }
                else
                {
                    _process = new ProcessToPutInHeadlock();
                }

                var p = @"C:\Users\sellersd\Desktop\test_headlock";
                _process.Go(new ExecutionPoint(p));

                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey(true);
            }
        }
    }
}