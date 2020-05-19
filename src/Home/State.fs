module Home.State 

open Elmish
open Types
open Fable.Core.JsInterop
open System

let makeNewReader () = importMember "../interops.js"


let init () : Model * Cmd<Msg> =
    {
        Messages =  Array.empty
        SelectedText = None;
        MustContain = Set.empty
        MustContainTemp = ""
        MustNotContain = Set.empty
        MustNotContainTemp = ""
        FiltersHidden = false
    }, [] 

let update msg model : Model * Cmd<Msg> =
    match msg with
    | AddMustContainFilter str ->
        { model with MustContain = Set.add str model.MustContain; MustContainTemp = "" }, []
    | RemoveMustContainFilter str ->
        { model with MustContain = Set.remove str model.MustContain }, [] 
    | UpdateMustContainTemp str ->
        { model with MustContainTemp = str},  []

    | AddMustNotContainFilter str ->
        { model with MustNotContain = Set.add str model.MustNotContain; MustNotContainTemp = "" }, []
    | RemoveMustNotContainFilter str ->
        { model with MustNotContain = Set.remove str model.MustNotContain }, [] 
    | UpdateMustNotContainTemp str ->
        { model with MustNotContainTemp = str},  []

    | ToggleFilters ->
        { model with FiltersHidden = not model.FiltersHidden }, []    

    | FileRead strs ->
        let strs = 
            strs
            |> Array.filter (fun (s: string) -> s.StartsWith("["))
            |> Array.filter (fun (s: string) -> s.StartsWith("[stacktrace]") |> not)

        { model with Messages = strs }, []    
    | FileSelected file ->
        let sub dispatch = 
            
            let reader = makeNewReader()

            reader?onload <- (fun () ->
                let result = !!reader?result : string
                let splitter : array<string> = [| Environment.NewLine |]
                let w = result.Split(splitter, StringSplitOptions.None)
                dispatch (FileRead w) 
            )

            reader?readAsText file |> ignore            

        model, Cmd.ofSub sub
