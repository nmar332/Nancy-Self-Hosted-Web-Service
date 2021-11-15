using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using System.Linq.Dynamic;  // -r:DynamicLinqUoA.dll

using Nancy;                // -r:Nancy.dll
using Nancy.Hosting.Self;   // -r:Nancy.Hosting.Self.dll

public class A8Module : NancyModule {
    
    // === XElements
    
    public class Customer {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }

    public class Order {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set;}
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public Customer Customer { get; set; }
    }

      public A8Module () {
        Console.WriteLine ("\r\n... Initalising");
        
        Get ("/", x => {
            Console.WriteLine ($"GET ...");


            return "G'day mate!";
        });

        Post ("/post/a8", x => {
            Console.WriteLine ($"POST ...");
            
            //var response = new { Request.Method, Request.Path, data }
            //return Response.AsJson (response);
            var str = new StreamReader (this.Request.Body);
            var data = str.ReadToEnd();

            string Table = new StringReader(data).ReadLine();
            
            if(Table == "Customers"){
                
                var root = XElement.Load(@"data/Customers.xml"); 
                var listRead = new List <Customer> ();

                foreach(var item in root.Elements("Customer")){
                    var id = item.Element("CustomerID");
                    var name = item.Element("CompanyName");
                    var contact = item.Element("ContactName");

                    var readobject = new Customer {
                        CustomerID = (string) id, 
                        CompanyName = (string) name, 
                        ContactName = (string) contact};

                    listRead.Add(readobject);
                }
               

            String line;
            List<string> input = new List<string>();
            IQueryable query = listRead.AsQueryable();

            using (StringReader cc = new StringReader(data)) {
                while((line = cc.ReadLine()) != null){
                
                input = line.Split(' ').ToList();

                if(input.First() == "Where") {
                    line = line.Replace(@"Where ", "");
                    query = query.Where(line);
                }
                else if(input.First() == "Select"){
                line = line.Replace(@"Select ", "");
                query = query.Select(line);
                    
                }
                else if(input.First() == "OrderBy"){
                line = line.Replace(@"OrderBy ", "");
                query = query.OrderBy(line);
                
                }
                
                else if(input.First() == "Skip"){
                line = line.Replace(@"Skip ", "");
                int b = Int32.Parse(line);
                query = query.Skip(b);
                }
                
                else if(input.First() == "Take"){
                line = line.Replace(@"Take ", "");
                int b = Int32.Parse(line);
                query = query.Take(b);
                }

            }
            }
            return Response.AsJson(query);

        
            }
            else if(Table == "Orders"){
                
                var root = XElement.Load(@"data/Orders.xml"); 
                var listRead = new List <Order> ();

                foreach(var item in root.Elements("Order")){
                    var id = item.Element("OrderID");
                    var cusID = item.Element("CustomerID");
                    
                    
                    var shipname = item.Element("ShipName");
                    var city = item.Element("ShipCity");
                    var country = item.Element("ShipCountry");

                    DateTime? date;
                    DateTime? shipped;
                    decimal? fri;

                    try{
                         date = (DateTime?) item.Element("OrderDate");
                    }
                    catch{
                        date = null;
                    }

                    try{
                        shipped = (DateTime?) item.Element("ShippedDate");
                    }
                    catch{
                        shipped = null;
                    }

                    try{
                         fri =  (decimal?) item.Element("Freight");
                    }
                    catch{
                        fri = null;
                    }



                    var readobject = new Order {
                        OrderID = (int) id, 
                        CustomerID = (string) cusID,
                        OrderDate = (DateTime?) date,
                        ShippedDate = (DateTime?) shipped,
                        Freight = (decimal?) fri,
                        ShipName = (string) shipname,
                        ShipCity = (string) city,
                        ShipCountry = (string) country};


                    listRead.Add(readobject);
                }
               

            String line;
            List<string> input = new List<string>();
            IQueryable query = listRead.AsQueryable();

            using (StringReader cc = new StringReader(data)) {
                while((line = cc.ReadLine()) != null){
                
                input = line.Split(' ').ToList();

                if(input.First() == "Where") {
                    line = line.Replace(@"Where ", "");
                    query = query.Where(line);
                }
                else if(input.First() == "Select"){
                line = line.Replace(@"Select ", "");
                query = query.Select(line);
                    
                }
                else if(input.First() == "OrderBy"){
                line = line.Replace(@"OrderBy ", "");
                query = query.OrderBy(line);
                
                }
                
                else if(input.First() == "Skip"){
                line = line.Replace(@"Skip ", "");
                int b = Int32.Parse(line);
                query = query.Skip(b);
                }
                
                else if(input.First() == "Take"){
                line = line.Replace(@"Take ", "");
                int b = Int32.Parse(line);
                query = query.Take(b);
                }

            }
            }
            
            return Response.AsJson(query);

        
            }

            return "fail";
        

                });        
    }
}
    
class A8Host {
    public static void Main () {
        using (var host = new NancyHost(new Uri("http://localhost:8081"))) {
            host.Start();
            Console.WriteLine("\r\n... Running on http://localhost:8081");
            var root = XElement.Load(@"data/Customers.xml");
            Console.WriteLine("/post/text", root);

            Console.ReadLine();
            
        }
    }    
   }
   

