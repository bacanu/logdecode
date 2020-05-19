module Home.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Types

let listMessages dispatch model = 
    model.Messages
    |> Array.filter (fun (s: string) ->
        Seq.forall (fun needle -> s.Contains(needle)) model.MustContain
        &&
        Seq.forall ((fun needle -> s.Contains(needle)) >> not) model.MustNotContain
    )
    |> List.ofArray 
    |> List.map (fun m -> 
        div 
            [ ] 
            [ str m ]
    )

let unordedList dispatch elements =
    let listItems = 
        elements
        |> Set.toList
        |> List.map (fun e -> li [ OnClick (fun _ -> dispatch e)] [ str e ] )
    
    ul [] listItems

let filters model dispatch = 
    div
        [ classList [("filters", true); ("hidden", model.FiltersHidden)] ]
        [ i
            [ classList [ ("filter-toggle fas", true); ("fa-angle-double-down", not model.FiltersHidden); ("fa-angle-double-up", model.FiltersHidden)]
              OnClick (fun _ -> ToggleFilters |> dispatch )]
            [ ]
          div
            [ ClassName "filters-container" ]
            [ div
                [ ClassName "must-contain"]
                [ (unordedList (RemoveMustContainFilter >> dispatch)  model.MustContain)
                  input 
                    [ Placeholder "Must Contain" 
                      OnChange (fun ev -> ev?target?value |> UpdateMustContainTemp |> dispatch)
                      Value model.MustContainTemp  ]
                  button
                    [ OnClick (fun _ -> AddMustContainFilter model.MustContainTemp |> dispatch) ] 
                    [ str "Add" ] ]

              div
                [ ClassName "must-not-contain" ]
                [ (unordedList (RemoveMustNotContainFilter >> dispatch)  model.MustNotContain)
                  input 
                    [ Placeholder "Must Not Contain" 
                      OnChange (fun ev -> ev?target?value |> UpdateMustNotContainTemp |> dispatch)
                      Value model.MustNotContainTemp ]
                  button
                    [ OnClick (fun _ -> AddMustNotContainFilter model.MustNotContainTemp |> dispatch) ] 
                    [ str "Add" ] ] ] ]

let root (model : Model) dispatch =
  div 
    [ ]
    [ div
        [ classList [("hidden", model.Messages <> Array.empty); ("info", true)] ]
        [ span [] [ str """A *very simple* log viewer and explorer for log files in the "[%datetime%] %channel%.%level_name%: %message% %context% %extra%\n" format."""]
          span [] [ str "Useful for the default log files from Laravel, Symfony, CakePHP and other frameworks that use Monolog." ]
          span [] [ str "The file is processed locally, on your machine. The file is NOT uploaded to any server!"]        
          span [] [ str "For feature requests, bugs and such: Cosmin Bacanu <bacanu.c[at]gmail[dot]com>" ]
          input
            [ Type "file" 
              OnChange (fun ev -> ev?target?files?(0) |> FileSelected |> dispatch) ] ]

      div
        [ classList [("hidden", model.Messages <> Array.empty); ("file-help", true)] ]
        [ str "Step 1: Add a log file here"
          span [] [ str "→" ] ]
      
      div
        [ classList [("hidden", model.Messages <> Array.empty); ("filter-help", true)] ]
        [ str "Step 2: Use the filters here" 
          span [] [ str "↓" ]]
      
      (filters model dispatch)
      div
        [ ClassName "lines" ]
        (listMessages dispatch model) ]
