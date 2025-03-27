namespace LocalNetwork

open System

type OperatingSystem =
    | Windows
    | Linux
    | MacOS
    member this.InfectionProbability =
        match this with
        | Windows -> 0.8
        | Linux -> 0.2
        | MacOS -> 0.4

type Computer(id: int, os: OperatingSystem, isInfected: bool) =
    let mutable isInfected = isInfected
    
    member this.Id = id
    member this.OS = os
    member this.IsInfected = isInfected
    
    member this.TryInfect(random: Random) =
        if not isInfected && random.NextDouble() < os.InfectionProbability then
            isInfected <- true

type Network(computers: Computer[], net: bool[,]) =
    let random = Random()

    let isFinished =
        { 0 .. computers.Length - 1 }
        |> Seq.forall (fun i ->
            { 0 .. computers.Length - 1 }
            |> Seq.forall (fun j ->
                not net[i,j] || computers[i].IsInfected = computers[j].IsInfected))

    let printState =
        for computer in computers do
            printfn "Computer %d (%A): %s" computer.Id computer.OS (if computer.IsInfected then "Infected" else "Healthy")
        printfn ""

    let spreadInfection =
        for i = 0 to computers.Length - 1 do
            if computers[i].IsInfected then
                for j = 0 to computers.Length - 1 do
                    if net[i,j] && not computers[j].IsInfected then
                        computers[j].TryInfect(random)

    member this.SpreadInfection() = spreadInfection

    member this.Model =
        printState
        while not isFinished do
            spreadInfection
            printState
