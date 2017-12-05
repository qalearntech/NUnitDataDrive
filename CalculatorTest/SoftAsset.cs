using System;

namespace CalculatorTest
{
    public static class Soft
    {
        public static bool softAssertFailed = false;
        public static void Assert(Action assertion)
        {
            try
            {
                assertion();
            }
            catch(Exception ex)
            {
                //log here to Extent report
                Console.WriteLine($"Soft assert failed: {ex.Message}");
                if (!softAssertFailed)
                    softAssertFailed = true;
            }
        }
    }
}
