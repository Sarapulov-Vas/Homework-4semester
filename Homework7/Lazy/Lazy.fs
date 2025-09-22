namespace Lazy

open System.Threading

/// lazy interface
type ILazy<'a> =
    /// Method for obtaining the value of a function
    abstract member Get: unit -> 'a

/// Single Thread Lazy
type SingleThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                let value = supplier()
                result <- Some value
                value

/// Thread-safe implementation of lazy computation with locking
type MultiThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    let lockObj = obj()
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                lock lockObj (fun () ->
                    match result with
                    | Some value -> value
                    | None ->
                        let value = supplier()
                        result <- Some value
                        value)

/// Threaded lock-free implementation of lazy computation
type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member this.Get() =
            match result with
            | Some value -> value
            | None ->
                let value = supplier()
                Interlocked.CompareExchange(&result, Some value, None) |> ignore
                match result with
                | Some v -> v
                | None -> value
