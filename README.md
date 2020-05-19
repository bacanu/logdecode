# Log decode based on Fable.Elmish.React Template



## Install & Dev server

* Install JS dependencies: `npm install`
* `npm start` (or `npx webpack-dev-server` to run manually)
* In your browser, open: http://localhost:8080/

If you are using VS Code + [Ionide](http://ionide.io/), you can also use the key combination: Ctrl+Shift+B (Cmd+Shift+B on macOS) instead of typing the `npx webpack-dev-server` command. This also has the advantage that Fable-specific errors will be highlighted in the editor along with other F# errors.

Any modification you do to the F# code will be reflected in the web page after saving. When you want to output the JS code to disk, run `npx webpack` and you'll get all the files minified in `deploy` folder.
