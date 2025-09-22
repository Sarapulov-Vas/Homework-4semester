open System
open PhoneBook.Commands

let rec mainLoop book =
    printfn "\nSelect action:"
    printfn "1. Add entry"
    printfn "2. Find a phone by name"
    printfn "3. Find a name by phone"
    printfn "4. Display all contents of the phone book"
    printfn "5. Save data to a file"
    printfn "6. Load data from a file"
    printfn "0. Exit"
    printf "> "
    match Console.ReadLine() with
    | "1" ->
        printf "Enter a name: "
        let name = Console.ReadLine()
        printf "Enter the phone: "
        let phone = Console.ReadLine()
        let newBook = addEntry name phone book
        printfn "Entry added."
        mainLoop newBook
    | "2" ->
        printf "Enter a name: "
        let name = Console.ReadLine()
        match findPhoneByName name book with
            | Some entry -> printfn "Phone: %s" entry.Phone
            | None -> printfn "No entry found."
        mainLoop book
    | "3" ->
        printf "Enter phone: "
        let phone = Console.ReadLine()
        match findNameByPhone phone book with
            | Some entry -> printfn "Name: %s" entry.Name
            | None -> printfn "No entry found."
        mainLoop book
    | "4" ->
        printfn "Current contents of the phone book:"
        book |> List.iter (fun entry -> printfn "%s: %s" entry.Name entry.Phone)
        mainLoop book
    | "5" ->
        printf "Enter the path to the file: "
        let path = Console.ReadLine()
        saveToFile path book
        printfn "Data saved."
        mainLoop book
    | "6" ->
        printf "Enter the path to the file: "
        let path = Console.ReadLine()
        let newBook = loadFromFile path
        match newBook with
            | Some book -> 
                printfn "Data uploaded."
                mainLoop book
            | None -> printfn "File not found."
    | "0" ->
        printfn "Exit."
    | _ ->
        printfn "incorrect input!"
        mainLoop book

mainLoop []
