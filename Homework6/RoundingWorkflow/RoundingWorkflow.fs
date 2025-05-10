namespace RoundingWorkflow

module Rounding =
    type RoundingBuilder(precision: int) =
        member this.Bind(x: float, f) = 
            f (System.Math.Round(x, precision))
        member this.Return(x: float) = 
            System.Math.Round(x, precision)
        
    let rounding precision = new RoundingBuilder(precision)
