namespace PhoneBook

open System.IO

module Commands =
    type PhoneBookEntry = { Name: string; Phone: string }
    type PhoneBook = PhoneBookEntry list

    /// Function to add an entry to the phone book.
    let addEntry name phone book =
        { Name = name; Phone = phone } :: book

    /// Function to search for a entry by name.
    let findPhoneByName name book =
        book |> List.tryFind (fun entry -> entry.Name = name)

    /// Function to search for a entry by phone.
    let findNameByPhone phone book =
        book |> List.tryFind (fun entry -> entry.Phone = phone)

    /// The function to save the phone book to a file.
    let saveToFile path book =
        let lines = book |> List.map (fun entry -> sprintf "%s;%s" entry.Name entry.Phone)
        File.WriteAllLines(path, lines)

    /// Function for downloading the phone book from a file.
    let loadFromFile path =
        if File.Exists(path) then
            File.ReadAllLines(path)
            |> Array.map (fun line -> line.Split(';'))
            |> Array.map (fun parts -> { Name = parts.[0]; Phone = parts.[1] })
            |> Array.toList
            |> Some
        else
            None
