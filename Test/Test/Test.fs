namespace Test

module Test =
    /// Binary tree type.
    type Tree<'a> =
        | Node of 'a * Tree<'a> * Tree<'a>
        | Leaf

    /// Binary tree element filtering function.
    /// Arguments: filter function, binary tree.
    /// Return: list of filtered items.
    let rec filter f binTree =
        match binTree with
            | Leaf -> []
            | Node(value, left, right) ->
                if f value then
                    value :: filter f left @ filter f right
                else
                    filter f left @ filter f right

    /// Infinite sequence of the form [1; -1; 1; -1; ...]
    let firstSeq = Seq.initInfinite (fun x -> if x % 2 = 0 then 1 else -1)
    /// Infinite sequence of the form [1; -2; 3; -4; ...]
    let secondSeq = 
        Seq.zip (Seq.initInfinite ((+) 1)) firstSeq |>Seq.map (fun (x, y) -> x * y)
    
    /// Priority queue class.
    type PriorityQueue<'a>() =
        let mutable queue = List.empty<'a * int>

        /// Method to add an element to the priority queue.
        /// Arguments: item, priority.
        member this.Enqueue (item, priority) =
            let queueStart = List.filter (fun (x, p) -> p >= priority) queue
            let queueTail = List.filter (fun (x, p) -> p < priority) queue
            queue <- queueStart @ (item, priority) :: queueTail

        /// method of extracting an element from a queue.
        /// Exception on empty heap.
        member this.Dequeue =
            match queue with
                | [] -> failwith "Empty queue"
                | (item, _) :: tail -> 
                    queue <- tail
                    item
