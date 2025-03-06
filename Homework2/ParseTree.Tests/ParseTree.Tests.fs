module ParseTree.Tests

open NUnit.Framework
open Homework2
open FsUnit

[<Test>]
let ```parse tree calculation test`` () =
    let tree = 
        ParseTree.Operator((+), ParseTree.Operand 1, ParseTree.Operand 1)
    ParseTree.calculate tree |> should equal 2

    let tree = 
        ParseTree.Operator((+), 
        ParseTree.Operator((/), ParseTree.Operand 10, ParseTree.Operand 2),
        ParseTree.Operator((*), 
        ParseTree.Operator((-), ParseTree.Operand 7, ParseTree.Operand 2),
        ParseTree.Operand 2))
    ParseTree.calculate tree |> should equal 15

let ```parse tree calculation operand test`` () =
    let tree =  ParseTree.Operand 1
    ParseTree.calculate tree |> should equal 1
