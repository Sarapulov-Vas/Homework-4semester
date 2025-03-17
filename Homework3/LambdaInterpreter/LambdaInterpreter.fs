namespace LambdaInterpreter

module Interpreter =
    type LambdaTerm =
        | Variable of string
        | Application of LambdaTerm * LambdaTerm
        | Abstraction of string * LambdaTerm

    let rec getFreeVariables expression =
        match expression with
            | Variable var -> [var]
            | Abstraction (var, term) -> 
                getFreeVariables term |> List.filter (fun x -> x <> var)
            | Application (leftTerm, rightTerm) -> 
                getFreeVariables leftTerm @ getFreeVariables rightTerm

    let rec getNewVar var index freeVars =
        let newVar = sprintf "%s%d" var index
        if List.contains newVar freeVars then 
            getNewVar var (index + 1) freeVars
        else
            newVar

    let rec substitute expression var newTerm =
        match expression with
            | Variable v -> if v = var then newTerm else Variable v
            | Application (leftTerm, rightTerm) ->
                Application (substitute leftTerm var newTerm, substitute rightTerm var newTerm)
            | Abstraction (v, term) ->
                if v = var then
                    Abstraction (v, term)
                else
                    let termFreeVars = getFreeVariables term
                    let newTermFreeVars = getFreeVariables newTerm
                    if not(List.contains var termFreeVars) || not(List.contains v newTermFreeVars) then
                        Abstraction (v, substitute term var newTerm)
                    else
                        let newVar = getNewVar v 0 (termFreeVars @ newTermFreeVars) 
                        Abstraction (newVar, substitute (substitute term v (Variable newVar)) var newTerm)

    let rec betaReduction expression =
        match expression with
            | Variable _ -> expression
            | Abstraction (var, term) -> Abstraction(var, betaReduction term)
            | Application (Abstraction(var, absTerm), term) -> 
                betaReduction (substitute absTerm var term)
            | Application (leftTerm, rightTerm) ->
                let reducedLeftTerm = betaReduction leftTerm
                match reducedLeftTerm with
                    | Abstraction _ -> betaReduction (Application(reducedLeftTerm, rightTerm))
                    | _ -> Application (reducedLeftTerm, betaReduction rightTerm)
