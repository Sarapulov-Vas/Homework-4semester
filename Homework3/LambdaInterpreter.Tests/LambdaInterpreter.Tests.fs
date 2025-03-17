module LambdaInterpreter.Tests

open NUnit.Framework
open FsUnit
open LambdaInterpreter.Interpreter

[<Test>]
let ``getFreeVariables Tests`` () =
    getFreeVariables (Variable "x") |> should equal ["x"]
    getFreeVariables (Abstraction ("x", Variable "x")) |> should be Empty
    getFreeVariables (Application (Variable "x", Variable "y")) |> should equal ["x"; "y"]
    getFreeVariables (Application (Abstraction ("x", Variable "x"), Variable "y"))
        |> should equal ["y"]

[<Test>]
let ``getNewVar Tests`` () =
    getNewVar "x" 0 ["x"; "y"; "z"] |> should equal "x0"
    getNewVar "x" 0 ["x"; "x0"; "y"] |> should equal "x1"

[<Test>]
let ``substitute Tests`` () =
    substitute (Variable "x") "x" (Variable "y") |> should equal (Variable "y")

    substitute (Application (Variable "x", Variable "y")) "x" (Variable "z")
    |> should equal (Application (Variable "z", Variable "y"))

    substitute (Abstraction ("x", Variable "x")) "x" (Variable "y")
    |> should equal (Abstraction ("x", Variable "x"))

    substitute (Abstraction ("x", Application (Variable "x", Variable "y"))) "y" (Variable "x")
    |> should equal (Abstraction ("x0", Application (Variable "x0", Variable "x")))

[<Test>]
let ``betaReduction Tests`` () =
    let expr = Application (Abstraction ("x", Variable "x"), Variable "y")
    betaReduction expr |> should equal (Variable "y")

    let expr = Application (
        Abstraction ("x", Abstraction ("y", Application (Variable "x", Variable "y"))),
        Variable "z"
    )
    betaReduction expr 
    |> should equal (Abstraction ("y", Application (Variable "z", Variable "y")))

    let expr = Application (Variable "x", Variable "y")
    betaReduction expr |> should equal (Application (Variable "x", Variable "y"))

    let expr = Application (
        Abstraction ("x", Abstraction ("y", Application (Variable "x", Variable "y"))),
        Variable "y"
    )
    betaReduction expr |> should equal (Abstraction ("y0", Application (Variable "y", Variable "y0")))
