declare function CodeMirror(element, config);

var myCodeMirror = CodeMirror(document.body, {
    value: "",
    mode: "markdown",
    theme: "threax-notes",
    lineNumbers: true,
    lineWrapping: true
});