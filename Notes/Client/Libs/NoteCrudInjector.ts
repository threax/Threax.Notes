import * as client from 'clientlibs.ServiceClient';
import * as hyperCrud from 'hr.widgets.HypermediaCrudService';
import * as di from 'hr.di';

export class NoteCrudInjector extends hyperCrud.AbstractHypermediaPageInjector {
    public static get InjectorArgs(): di.DiFunction<any>[] {
        return [client.EntryPointInjector];
    }

    constructor(private injector: client.EntryPointInjector) {
        super();
    }

    async list(query: any): Promise<hyperCrud.HypermediaCrudCollection> {
        var entry = await this.injector.load();
        return entry.listNotes(query);
    }

    async canList(): Promise<boolean> {
        var entry = await this.injector.load();
        return entry.canListNotes();
    }

    public getDeletePrompt(item: client.NoteResult): string {
        return "Are you sure you want to delete the note?";
    }

    public getItemId(item: client.NoteResult): string | null {
        return String(item.data.noteId);
    }

    public createIdQuery(id: string): client.NoteQuery | null {
        return {
            noteId: id
        };
    }
}