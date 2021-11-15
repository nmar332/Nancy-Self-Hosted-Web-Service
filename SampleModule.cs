using System;
using System.IO;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

public class SampleModule : NancyModule {
    public SampleModule () {
        Get ("/", _ => "Hello World!");
        
        Get ("/greet/", _ => "Hello Anonymous!");
        
        Get ("/greet/{name}", x => {
            return "Hello " + x.name;
        });
        
        // === Something like this will be needed for A8, combined with a Response.ToJson
        
        Post ("/post/text", x => {
            Console.WriteLine ("\r\n/post/text");
            
            var str = new StreamReader (this.Request.Body);
            var data = str.ReadToEnd ();
            Console.WriteLine (data);
            
            return Response.AsJson (new [] {"alfa", "beta"});
        });
        
        // === The rest is more advanced stuff, not needed for A8
        
        Post ("/post/json", x => {
            Console.WriteLine ("\r\n/post/json");

            var str = new StreamReader (this.Request.Body);
            var data = str.ReadToEnd ();
            Console.WriteLine (data);            
            
            return Response.AsJson (new [] { (101, 201), (301, 401), });
            //return Response.AsXml (new [] { (101, 201), (301, 401), });
        });
        
        Post ("/post/xml", x => {
            Console.WriteLine ("\r\n/post/xml");

            var str = new StreamReader (this.Request.Body);
            var data = str.ReadToEnd ();
            Console.WriteLine (data);            
            
            return Response.AsJson (new [] { (102, 202), (302, 402), });
            //return Response.AsXml (new [] { (102, 202), (302, 402), });
        });
        
        Post ("/post/model0", x => {
            Console.WriteLine ("\r\n/post/model0");

            var data = this.Bind <pair> ();
            Console.WriteLine ($"... ({data.x}, {data.y})");
            return Response.AsJson (data);
            //return Response.AsXml (data);
        });
        
        Post ("/post/modeljson", x => {
            Console.WriteLine ("\r\n/post/modeljson");

            var data = this.Bind <pair[]> ();
            foreach (var d in data) Console.Write ($"... ({d.x}, {d.y})");
            Console.WriteLine ();
            return Response.AsJson (data);
            //return Response.AsXml (data);
        });
        
        Post ("/post/modelxml", x => {
            Console.WriteLine ("\r\n/post/modelxml");

            var _doc = new doc ();
            var data = this.BindTo (_doc);
            foreach (var d in data.root) Console.Write ($"... ({d.x}, {d.y})");
            Console.WriteLine ();
            return Response.AsJson (data);
            //return Response.AsXml (data);
        });
    }
}

public class doc {
    public pair[] root { get; set; }
}

public class pair {
    public int x { get; set; }
    public int y { get; set; }
}

