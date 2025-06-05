namespace Lazy

open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a

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
