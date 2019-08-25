namespace test.fsharp

module Animal = 
    type AnimalBase(name, noiseMakingStrategy) = 
       member this.Name = name
       member this.Noise = noiseMakingStrategy()
       member this.MakeNoise = 
           printfn "%s is %s" this.Name this.Noise  

    // now create a cat 
    let cat = AnimalBase("Chloe", fun _ ->  "meowing")

    // .. and a dog
    let dog = AnimalBase("Spot", fun _ -> 
        if (System.DateTime.Now.Second % 2 = 0) then 
            "woofing" 
            else 
            "barking")
