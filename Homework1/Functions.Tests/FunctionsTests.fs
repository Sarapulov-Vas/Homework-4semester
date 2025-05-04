module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit

/// Test of the function calculating the factorial of n.
[<Test>]
let ``Factorial Tests`` () =
    Functions.factorial 0 |> should equal (Some 1)
    Functions.factorial 5 |> should equal (Some 120)
    Functions.factorial -2 |> should equal None
