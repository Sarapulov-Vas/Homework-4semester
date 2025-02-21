module Functions.Tests

open NUnit.Framework
open Homework1
open FsUnit
open FsCheck

let revTest (list:list<int>) = Functions.listReverse list = List.rev list

/// Test of the list reversal function.
[<Test>]
let ``List reverse Tests`` () =
    Check.QuickThrowOnFailure revTest
