namespace test.fsharp

module WordCount = 
    let wordCountMap w = (w,1)
    let wordCountReduce acc (w,c) = 
        w
        