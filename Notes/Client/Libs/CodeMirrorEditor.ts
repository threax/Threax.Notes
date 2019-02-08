import * as controller from "hr.controller";
import * as client from 'clientlibs.ServiceClient';
import { TimedTrigger } from 'hr.timedtrigger';

declare function CodeMirror(element, config);

export function addServices(services: controller.ServiceCollection) {
    services.addShared(CodeMirrorEditor, CodeMirrorEditor);
}

export function createStandard(builder: controller.InjectedControllerBuilder) {
    builder.create("codemirror", CodeMirrorEditor);
}

export interface IFirstLineChangedListener {
    firstLineChanged(firstLine: string);
}

export class CodeMirrorEditor {
    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [controller.BindingCollection, client.EntryPointInjector];
    }

    private codeMirror: any;
    private handle: HTMLElement;
    private trigger: TimedTrigger<any>;
    private note: client.NoteResult;
    private firstLine: string;
    private ignoreSave: boolean = false;
    private listener: IFirstLineChangedListener;

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

    public changeNote(note: client.NoteResult) {
        this.note = note;
        this.codeMirror.setValue(note.data.text);
        this.ignoreSave = true;
        this.firstLine = this.getFirstLine(note.data.text);
    }

    public setFirstLineChangedListener(listener: IFirstLineChangedListener) {
        this.listener = listener;
    }

    private async save(): Promise<void> {
        if (!this.ignoreSave) {
            var text = this.codeMirror.getValue(); 
            this.note = await this.note.update({
                text: text
            });
            var firstLine = this.getFirstLine(text);
            if (firstLine != this.firstLine) {
                if (this.listener) {
                    this.listener.firstLineChanged(text);
                }
                this.firstLine = firstLine;
            }
        }
        this.ignoreSave = false;
    }

    public getFirstLine(text: string) {
        var firstLineEnd = text.indexOf('\n');
        if (firstLineEnd != -1) {
            text = text.substr(0, firstLineEnd);
        }
        return text;
    }
}