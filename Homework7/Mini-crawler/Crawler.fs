namespace MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

module Crawler =
    let httpClient = new HttpClient()

    /// Asynchronously loads the contents of a web page at URL
    /// Returns: page contents
    let downloadPage (url: string) =
        async {
            let! response = httpClient.GetAsync(url) |> Async.AwaitTask
            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return content
        }

    /// Extracts all absolute HTTP links from HTML page
    /// Returns: list of found URLs
    let extractLinks page =
        let pattern = "<a\\s+[^>]*href=\"(http[^\"]*)\"[^>]*>"
        let matches = Regex.Matches(page, pattern)
        [ for i in matches do
            if i.Groups.Count > 1 then
                yield i.Groups.[1].Value ]

    /// Processes a single page: loads and returns its URL and size
    /// Returns: tuple (URL, content size)
    let processPage url =
        async {
            let size = (Async.RunSynchronously (downloadPage(url))).Length
            return url, size
        }

    /// Main function: scans the initial page and all links found on it
    /// Returns: list of tuples (URL, size)
    let crawlPage startUrl =
        async {
            let page = downloadPage startUrl
            let links = Async.RunSynchronously page |> extractLinks
            let! results = 
                links
                |> List.map processPage
                |> Async.Parallel
            return results |> Array.toList
        }

    /// Function to output page information
    let printInfo startUrl=
        for (url, size) in Async.RunSynchronously (crawlPage startUrl) do
            printfn "%s - %d" url size
