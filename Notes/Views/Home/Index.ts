declare function CodeMirror(element, config);

var myCodeMirror = CodeMirror(document.body, {
    value: "",
    mode: "markdown"
});