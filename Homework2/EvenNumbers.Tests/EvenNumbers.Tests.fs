module EvenNumbers.Tests

open NUnit.Framework
open Homework2.CountEvenNumbers
open FsUnit
open FsCheck

/// Comparing the equivalence of the first and second functions.
let firstSecondEquivalenceTest (list:list<int>) = 
    firstCountEvenNumbers list = secondCountEvenNumbers list

/// Comparing the equivalence of the second and third functions.
let SecondThirdEquivalenceTest (list:list<int>) = 
    secondCountEvenNumbers list = thirdCountEvenNumbers list

/// First count even numbers test.
[<Test>]
let ``First count even Numbers test`` () =
    [1; 2; 3] |> secondCountEvenNumbers |> should equal 1
    [2; 3; 4; 6] |> secondCountEvenNumbers |> should equal 3
    [1; 3; 5] |> secondCountEvenNumbers |> should equal 0
    [] |> secondCountEvenNumbers |> should equal 0
    [1] |> secondCountEvenNumbers |> should equal 0
    [10] |> secondCountEvenNumbers |> should equal 1

/// Functional Equivalence Test.
[<Test>]
let ``Functional equivalence test`` () =
    Check.QuickThrowOnFailure firstSecondEquivalenceTest
    Check.QuickThrowOnFailure SecondThirdEquivalenceTest

