using System;
using Nancy.Hosting.Self;

class SampleHost {
    public static void Main () {
        using (var host = new NancyHost(new Uri("http://localhost:8081"))) {
           host.Start();
           Console.WriteLine("Running on http://localhost:8081");
           Console.ReadLine();
        }    
    }
}
