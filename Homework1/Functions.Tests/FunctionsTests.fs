module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit
open FsCheck

let revTest (list:list<int>, n:int) =
    Functions.numberSearch list n = List.tryFindIndex (fun x -> x = n) list

/// Test of the function of searching for an item in the list.
[<Test>]
let ``List reverse Tests`` () =
    Check.QuickThrowOnFailure revTest
