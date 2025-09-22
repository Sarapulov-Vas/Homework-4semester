namespace LocalNetwork

open System

/// Operating system interface.
type IOperatingSystem =
    /// A method that returns the probability of the computer being infected.
    abstract InfectionProbability : float

/// Discriminated Union implementing IOperatingSystem.
type OperatingSystem =
    | Windows
    | Linux
    | MacOS
    interface IOperatingSystem with
        member this.InfectionProbability =
            match this with
            | Windows -> 0.8
            | Linux -> 0.2
            | MacOS -> 0.4

/// Class of computer.
type Computer(id: int, os: IOperatingSystem, isInfected: bool) =
    let mutable isInfected = isInfected
    
    /// Computer Id.
    member this.Id = id
    /// Computer operating system.
    member this.OS = os
    /// Whether the computer is infected.
    member this.IsInfected with get() = isInfected
    
    /// Method of attempting to infect a computer.
    member this.TryInfect(random: Random) =
        if not isInfected && random.NextDouble() < os.InfectionProbability then
            isInfected <- true

type Network(computers: list<Computer>, net: list<list<bool>>) =
    let random = Random()

    let isFinished() =
        { 0 .. computers.Length - 1 }
        |> Seq.forall (fun i ->
            { 0 .. computers.Length - 1 }
            |> Seq.forall (fun j ->
                not (net[i][j]) || computers[i].IsInfected = computers[j].IsInfected))

    let printState() =
        for computer in computers do
            printfn "Computer %d (%A): %s" computer.Id computer.OS (if computer.IsInfected then "Infected" else "Healthy")
        printfn ""

    let spreadVirus() =
        let infectList = 
            [
                for i = 0 to computers.Length - 1 do
                    if computers[i].IsInfected then
                        for j = 0 to computers.Length - 1 do
                            if net[i][j] && not computers[j].IsInfected then
                                yield computers[j]
            ]
        infectList |> List.iter (fun i -> i.TryInfect(random))

    /// A method of performing the step of spreading a virus in a network.
    member this.SpreadVirus = spreadVirus

    /// A method for printing the status of computers.
    member this.PrintState = printState

    /// A method for running simulations of virus propagation in a network.
    member this.Model =
        printState()
        while not (isFinished()) do
            spreadVirus()
            printState()
