namespace Homework2

/// Function module for counting the number of even numbers.
module CountEvenNumbers =
    /// The function of counting the number of even numbers in the list.
    let firstCountEvenNumbers list =
        list |> List.filter (fun x -> x % 2 = 0) |> List.length

    /// The function of counting the number of even numbers in the list.
    let secondCountEvenNumbers list =
        list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0

    /// The function of counting the number of even numbers in the list.
    let thirdCountEvenNumbers list =
        list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum
    