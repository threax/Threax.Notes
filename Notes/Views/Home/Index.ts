import * as standardCrudPage from 'hr.widgets.StandardCrudPage';
import * as startup from 'clientlibs.startup';
//import * as deepLink from 'hr.deeplink';
import { NoteCrudInjector } from 'clientlibs.NoteCrudInjector';
import * as codemirroreditor from 'clientlibs.CodeMirrorEditor';
import * as crudpage from 'hr.widgets.CrudPage';

import * as controller from "hr.controller";
import * as client from 'clientlibs.ServiceClient';

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
        return [codemirroreditor.CodeMirrorEditor, client.EntryPointInjector, crudpage.ICrudService];
    }

    public constructor(private editor: codemirroreditor.CodeMirrorEditor, private injector: client.EntryPointInjector, private crudService: crudpage.ICrudService) {
        super();
        this.editor.setFirstLineChangedListener(this);
    }

    public setupBindings(bindings: controller.BindingCollection): void {
        bindings.setListener(this);
    }

    public async addOnPage(evt: Event): Promise<void> {
        var entry = await this.injector.load();
        var newNote = await entry.addNote({ text: "A New Note" });
        this.editor.changeNote(newNote);
        this.crudService.refreshPage();
    }

    public firstLineChanged(firstLine: string) {
        this.crudService.refreshPage();
    }
}

var builder = startup.createBuilder();
codemirroreditor.addServices(builder.Services);
codemirroreditor.createStandard(builder);

var injector = NoteCrudInjector;

//deepLink.addServices(builder.Services);
builder.Services.addTransient(crudpage.CrudTableRowControllerExtensions, RowExt);
builder.Services.addTransient(crudpage.CrudTableControllerExtensions, TableExt);
standardCrudPage.addServices(builder, injector);
standardCrudPage.createControllers(builder, new standardCrudPage.Settings());