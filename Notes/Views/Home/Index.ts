declare function CodeMirror(element, config);

var myCodeMirror = CodeMirror(document.getElementById("codemirror"), {
    value: "",
    mode: "markdown",
    theme: "threax-notes",
    lineNumbers: true,
    lineWrapping: true
});