let matchList l =
    match l with
    | [] -> 
        printf "0: empty\n"
    | [head] ->
        printf "1: %A\n" head
    | head::tail ->
            printf "head: %A, tail: %A\n" head tail 

let minmax l =
    l |> List.min, l |> List.max

let l = [1.0; 2.0; 3.0; 4.0; 5.0] 

l |> matchList
l |> minmax

type Track = { Title: string; Artist: string }
