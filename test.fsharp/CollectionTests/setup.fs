namespace collectiontest.setup
    type House = { Address:string; Price:double }

    module House = 
        let private random = System.Random (Seed = 1)
        let getRandom count = 
            Array.init count (fun i ->
                { Address = sprintf "%i Stochastic Dr" (i+1); Price = random.Next(50000, 500000) |> double })

    module Distance = 
        let private random = System.Random (Seed = 1)
        let tryToSchool ( house:House ) = 
            let dist = random.Next(10) |> double
            if dist < 5 then
                Some dist
            else
                None

    type PriceBand = | Cheap | Medium | Expensive

    module PriceBand = 
        let fromPrice ( price:decimal ) = 
            if price < 100000 then
                Cheap
            elif price < 200000 then
                Medium
            else
                Expensive
