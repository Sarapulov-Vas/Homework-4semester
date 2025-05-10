module RoundingWorkflow.Tests

open NUnit.Framework
open FsUnit
open RoundingWorkflow.Rounding

[<Test>]
let ``Basic calculation with 3 decimal places``() =
    let result = 
        rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

    result |> should equal 0.048 

[<Test>]
let ``Rounding to zero decimal places``() =
    let result = 
        rounding 0 {
            let! a = 2.0 / 3.0
            return a
        }

    result |> should equal 1

[<Test>]
let ``Multiple operations with intermediate rounding``() =
    let result = 
        rounding 2 {
            let! a = 10.0 / 3.0
            let! b = a * 2.0
            let! c = b + 1.5
            return c
        }

    result |> should equal 8.16

[<Test>]
let ``Negative numbers rounding``() =
    let result = 
        rounding 1 {
            let! a = -5.0 / 2.0
            return a
        }

    result |> should equal -2.5
