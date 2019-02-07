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

    public rowConstructed(row: crudpage.CrudTableRowController, bindings: controller.BindingCollection, data: client.NoteListingResult): void {
        this.data = data;
        bindings.setListener(this);
    }

    public async editOnPage(evt: Event): Promise<void> {
        var note = await this.data.refresh();
        this.editor.changeNote(note);
    }
}

var builder = startup.createBuilder();
codemirroreditor.addServices(builder.Services);
codemirroreditor.createStandard(builder);

var injector = NoteCrudInjector;

//deepLink.addServices(builder.Services);
builder.Services.addTransient(crudpage.CrudTableRowControllerExtensions, RowExt);
standardCrudPage.addServices(builder, injector);
standardCrudPage.createControllers(builder, new standardCrudPage.Settings());