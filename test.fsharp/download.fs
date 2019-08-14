namespace test.fsharp

// "open" brings a .NET namespace into visibility
open System.Net
open System
open System.IO

module WebPageDownloader = 
    // Fetch the contents of a web page
    let fetchUrl callback url =        
        let req = WebRequest.Create(Uri(url)) 
        use resp = req.GetResponse() 
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        callback reader url

    let myCallback (reader:IO.StreamReader) url = 
        let html = reader.ReadToEnd()
        let html1000 = html.Substring(0,1000)
        printfn "Downloaded %s. First 1000 is %s" url html1000
        html      // return all the html
