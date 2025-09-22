module LocalNetwork.Tests

open NUnit.Framework
open FsUnit
open LocalNetwork

type MokOperationSystem =
    | First
    | Second
    interface IOperatingSystem with
        member this.InfectionProbability =
            match this with
            | First -> 1
            | Second -> 0

[<Test>]
let ``Virus spread test with probability 1`` () =
    let computers: Computer list = [
        Computer(0, First, true);
        Computer(1, First, false);
        Computer(2, First, false);
    ]

    let net = [
        [false; true; false];
        [true; false; true];
        [false; true; false];
    ]

    let network = Network(computers, net)
    network.SpreadVirus()
    computers[0].IsInfected |> should be True
    computers[1].IsInfected |> should be True
    computers[2].IsInfected |> should be False
    network.SpreadVirus()
    computers[0].IsInfected |> should be True
    computers[1].IsInfected |> should be True
    computers[2].IsInfected |> should be True

[<Test>]
let ``Virus spread test with probability 0`` () =
    let computers: Computer list = [
        Computer(0, Second, true);
        Computer(1, Second, false);
    ]

    let net = [
        [false; true];
        [true; false];
    ]

    let network = Network(computers, net)
    network.SpreadVirus()
    computers[0].IsInfected |> should be True
    computers[1].IsInfected |> should be False

[<Test>]
let ``Virus spread modeling test`` () =
    let computers = [
        Computer(0, Windows, false);
        Computer(1, MacOS, true);
        Computer(2, Linux, false);
    ]

    let net = [
        [false; true; true];
        [true; false; true];
        [true; true; false];
    ]

    let network = Network(computers, net)
    network.Model
    computers |> List.forall(fun x -> x.IsInfected) |> should be True

[<Test>]
let ``Virus free network test`` () =
    let computers = [
        Computer(0, Windows, false);
        Computer(1, MacOS, false);
        Computer(2, Linux, false);
    ]

    let net = [
        [false; true; true];
        [true; false; true];
        [true; true; false];
    ]

    let network = Network(computers, net)
    network.Model
    computers |> List.forall(fun x -> not x.IsInfected) |> should be True

[<Test>]
let ``Test of virus propagation in two unconnected networks`` () =
    let computers = [
        Computer(0, Windows, false);
        Computer(1, MacOS, true);
        Computer(2, Linux, false);
        Computer(3, Linux, false);
    ]

    let net = [
        [false; true; false; false];
        [true; false; false; false];
        [false; false; false; true];
        [false; false; true; false];
    ]

    let network = Network(computers, net)
    network.Model
    computers[0].IsInfected |> should be True
    computers[1].IsInfected |> should be True
    computers[2].IsInfected |> should be False
    computers[3].IsInfected |> should be False
