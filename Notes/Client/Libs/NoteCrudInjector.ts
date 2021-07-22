import * as client from 'Client/Libs/ServiceClient';
import * as hyperCrud from 'htmlrapier.widgets/src/HypermediaCrudService';
import * as di from 'htmlrapier/src/di';

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

    public getDeletePrompt(item: client.NoteListingResult): string {
        return "Delete " + item.data.firstLine + "?";
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