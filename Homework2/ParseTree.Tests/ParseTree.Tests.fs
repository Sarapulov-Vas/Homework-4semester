module ParseTree.Tests

open NUnit.Framework
open Homework2.ParseTree
open FsUnit

[<Test>]
let ``parse tree calculation test`` () =
    let tree = 
        Operator((+), Operand 1, Operand 1)
    calculate tree |> should equal 2

    let tree = 
        Operator((+), 
        Operator((/), Operand 10, Operand 2),
        Operator((*), 
        Operator((-), Operand 7, Operand 2),
        Operand 2))
    calculate tree |> should equal 15

let ``parse tree calculation operand test`` () =
    let tree =  Operand 1
    calculate tree |> should equal 1
