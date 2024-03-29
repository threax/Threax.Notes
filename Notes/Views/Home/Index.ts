import * as standardCrudPage from 'htmlrapier.widgets/src/StandardCrudPage';
import * as startup from 'Client/Libs/startup';
//import * as deepLink from 'htmlrapier.deeplink';
import { NoteCrudInjector } from 'Client/Libs/NoteCrudInjector';
import * as codemirroreditor from 'Client/Libs/CodeMirrorEditor';
import * as crudpage from 'htmlrapier.widgets/src/CrudPage';

import * as controller from 'htmlrapier/src/controller';
import * as client from 'Client/Libs/ServiceClient';
import * as events from 'htmlrapier/src/eventdispatcher';

class RowExt extends crudpage.CrudTableRowControllerExtensions {
    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [codemirroreditor.CodeMirrorEditor];
    }

    public constructor(private editor: codemirroreditor.CodeMirrorEditor) {
        super();
    }

    private data: client.NoteListingResult;
    private firstLineView: controller.IView<any>;

    public rowConstructed(row: crudpage.CrudTableRowController, bindings: controller.BindingCollection, data: client.NoteListingResult): void {
        this.data = data;
        bindings.setListener(this);
        this.firstLineView = bindings.getView("firstLineView");
        this.firstLineView.setData(this.data.data.firstLine);
    }

    public async editOnPage(evt: Event): Promise<void> {
        var note = await this.data.refresh();
        this.editor.changeNote(note);
    }
}

class TableExt extends crudpage.CrudTableControllerExtensions {
    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [codemirroreditor.CodeMirrorEditor, client.EntryPointInjector, crudpage.ICrudService, NotePageService];
    }

    private noteListToggle: controller.OnOffToggle;

    public constructor(private editor: codemirroreditor.CodeMirrorEditor, private injector: client.EntryPointInjector, private crudService: crudpage.ICrudService, private notePageService: NotePageService) {
        super();
        this.notePageService.toggleNoteListEvent.add(() => {
            if (this.noteListToggle.currentState === "off") {
                this.crudService.refreshPage();
            }
            this.noteListToggle.toggle();
        });
    }

    public setupBindings(bindings: controller.BindingCollection): void {
        bindings.setListener(this);
        this.noteListToggle = bindings.getToggle("noteList");
    }

    public async addOnPage(evt: Event): Promise<void> {
        var entry = await this.injector.load();
        var newNote = await entry.addNote({ text: "A New Note" });
        this.editor.changeNote(newNote);
        this.crudService.refreshPage();
    }
}

class NotePageService {
    private toggleNoteListDispatcher = new events.ActionEventDispatcher<void>();

    public get toggleNoteListEvent() {
        return this.toggleNoteListDispatcher.modifier;
    }

    public fireToggleNoteList() {
        this.toggleNoteListDispatcher.fire(undefined);
    }

    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [];
    }

    public constructor() {

    }
}

class HomeButton {
    public static get InjectorArgs(): controller.DiFunction<any>[] {
        return [controller.BindingCollection, NotePageService];
    }

    public constructor(bindings: controller.BindingCollection, private service: NotePageService) {
        
    }

    public async clicked(evt: Event): Promise<void> {
        evt.preventDefault();
        this.service.fireToggleNoteList();
    }
}

var builder = startup.createBuilder();
codemirroreditor.addServices(builder.Services);
codemirroreditor.createStandard(builder);

var injector = NoteCrudInjector;

//deepLink.addServices(builder.Services);
builder.Services.addTransient(HomeButton, HomeButton);
builder.Services.addShared(NotePageService, NotePageService);
builder.Services.addTransient(crudpage.CrudTableRowControllerExtensions, RowExt);
builder.Services.addTransient(crudpage.CrudTableControllerExtensions, TableExt);
standardCrudPage.addServices(builder, injector);
standardCrudPage.createControllers(builder, new standardCrudPage.Settings());
builder.create("homeButtonController", HomeButton);