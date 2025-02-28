module EvenNumbers.Tests

open NUnit.Framework
open EvenNumbers
open FsUnit
open FsCheck

/// Comparing the equivalence of the first and second functions.
let firstSecondEquivalenceTest (list:list<int>) = 
    CountEvenNumbers.firstCountEvenNumbers list = CountEvenNumbers.secondCountEvenNumbers list

/// Comparing the equivalence of the second and third functions.
let SecondThirdEquivalenceTest (list:list<int>) = 
    CountEvenNumbers.secondCountEvenNumbers list = CountEvenNumbers.thirdCountEvenNumbers list

/// First count even numbers test.
[<Test>]
let ``First count even Numbers test`` () =
    [1; 2; 3] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 1
    [2; 3; 4; 6] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 3
    [1; 3; 5] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 0
    [] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 0
    [1] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 0
    [10] |> CountEvenNumbers.secondCountEvenNumbers |> should equal 1

/// Functional Equivalence Test.
[<Test>]
let ``Functional equivalence test`` () =
    Check.QuickThrowOnFailure firstSecondEquivalenceTest
    Check.QuickThrowOnFailure SecondThirdEquivalenceTest

