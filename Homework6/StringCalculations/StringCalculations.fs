namespace StringCalculations

module Workflow =
    type StringCalculationsBuilder() =
        member this.Bind(m: string, f) = 
            match System.Int32.TryParse(m) with
            | (true, value) -> f value
            | _ -> None
        
        member this.Return(x) = 
            Some x
    let calculate = StringCalculationsBuilder()
