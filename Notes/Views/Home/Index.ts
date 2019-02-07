import * as controller from "hr.controller";
import * as client from 'clientlibs.ServiceClient';
import * as startup from 'clientlibs.startup';
import { TimedTrigger } from 'hr.timedtrigger';

declare function CodeMirror(element, config);

class CodeMirrorEditor {
    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [controller.BindingCollection, client.EntryPointInjector];
    }

    private codeMirror: any;
    private handle: HTMLElement;
    private trigger: TimedTrigger<any>;
    private note: client.NoteResult;

    public constructor(bindings: controller.BindingCollection, private injector: client.EntryPointInjector) {
        this.handle = bindings.getHandle("codemirror");
        this.trigger = new TimedTrigger<any>(500);

        this.setup();
    }

    private async setup(): Promise<void> {
        var entry = await this.injector.load();
        var notes = await entry.listNotes({});
        var noteListing = notes.items[0];
        this.note = await noteListing.refresh();

        this.codeMirror = CodeMirror(this.handle, {
            mode: "markdown",
            theme: "threax-notes",
            lineNumbers: true,
            lineWrapping: true,
            value: this.note.data.text
        });

        var self = this;
        this.codeMirror.on("changes", (instance, changes) => {
            self.trigger.fire(self);
        });

        //Do this here so everything else is registered
        this.trigger.addListener(a => this.save());
    }

    private async save(): Promise<void> {
        this.note = await this.note.update({
            text: this.codeMirror.getValue()
        });
    }
}

var builder = startup.createBuilder();
builder.Services.addTransient(CodeMirrorEditor, CodeMirrorEditor);
builder.create("codemirror", CodeMirrorEditor);