module AdventOfCode_2021.Shared

open System
open Xunit.Abstractions

type OutputWriter(output: ITestOutputHelper) =
    let agent =
        MailboxProcessor.Start
            (fun inbox ->
                async {
                    while true do
                        let! message = inbox.Receive ()
                        let now = DateTime.Now.ToString("HH:mm:ss:fff")
                        output.WriteLine $"{now} -> {message}"
                })

    member _.writeToOutput text = agent.Post text