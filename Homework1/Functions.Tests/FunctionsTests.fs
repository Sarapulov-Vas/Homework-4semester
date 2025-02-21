module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit

/// Test of the function realizing a series of powers of two.
[<Test>]
let ``Factorial Tests`` () =
    Functions.degreeRange 1 2 |> should equal (Some [2; 4])
    Functions.degreeRange 3 5 |> should equal (Some [8; 16; 32; 64; 128])
    Functions.degreeRange -1 2 |> should equal None
