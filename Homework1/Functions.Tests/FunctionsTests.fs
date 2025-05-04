module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit
open FsCheck

let searchTest (list:list<int>, n:int) =
    Functions.numberSearch list n = List.tryFindIndex (fun x -> x = n) list

/// Test of the function of searching for an item in the list.
[<Test>]
let ``Number Search Tests`` () =
    Check.QuickThrowOnFailure searchTest

let revTest (list:list<int>) = Functions.listReverse list = List.rev list

/// Test of the list reversal function.
[<Test>]
let ``List reverse Tests`` () =
    Check.QuickThrowOnFailure revTest

/// Test of the function calculating the n-th Fibonacci number.
[<Test>]
let ``fibonacci Tests`` () =
    Functions.fibonacci 2 |> should equal (Some 1)
    Functions.fibonacci 5 |> should equal (Some 5)
    Functions.fibonacci -2 |> should equal None

/// Test of the function calculating the factorial of n.
[<Test>]
let ``Factorial Tests`` () =
    Functions.factorial 0 |> should equal (Some 1)
    Functions.factorial 5 |> should equal (Some 120)
    Functions.factorial -2 |> should equal None
