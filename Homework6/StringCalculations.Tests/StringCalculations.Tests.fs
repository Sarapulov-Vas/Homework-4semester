module StringCalculations.Tests

open NUnit.Framework
open FsUnit
open StringCalculations.Workflow

[<Test>]
let ``Valid numbers``() =
    let result = calculate {
        let! x = "1"
        let! y = "2"
        let! z = "3"
        return (x + y) * z
    }

    result |> should equal (Some 9)

[<Test>]
let ``Invalid number``() =
    let result = calculate {
        let! x = "1"
        let! y = "Ъ"
        let z = x + y
        return z
    }

    result |> should equal None
