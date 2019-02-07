import * as standardCrudPage from 'hr.widgets.StandardCrudPage';
import * as startup from 'clientlibs.startup';
//import * as deepLink from 'hr.deeplink';
import { NoteCrudInjector } from 'clientlibs.NoteCrudInjector';
import * as codemirroreditor from 'clientlibs.CodeMirrorEditor';

var builder = startup.createBuilder();
codemirroreditor.addServices(builder.Services);
codemirroreditor.createStandard(builder);

var injector = NoteCrudInjector;

//deepLink.addServices(builder.Services);
standardCrudPage.addServices(builder, injector);
standardCrudPage.createControllers(builder, new standardCrudPage.Settings());