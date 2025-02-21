module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit

/// Test of the function calculating the n-th Fibonacci number.
[<Test>]
let ``fibonacci Tests`` () =
    Functions.fibonacci 2 |> should equal (Some 1)
    Functions.fibonacci 5 |> should equal (Some 5)
    Functions.fibonacci -2 |> should equal None
