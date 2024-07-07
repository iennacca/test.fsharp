#load "ResultBuilder.fs"

open GrokTests.Core

    // Testing ResultBuilder
let validateName name = 
    match name with
    | null -> Error ["Name must not be null."]
    | "" -> Error ["Name must not be empty."]
    | _ -> Ok name

result {
    let! s1 = validateName "Jerry"
    and! s2 = validateName "Wendell"
    and! s3 = validateName "Chaves"
    return  (s1, s2, s3)
}

result {
    let! s1 = validateName ""
    and! s2 = validateName ""
    return (s1, s2)
}
