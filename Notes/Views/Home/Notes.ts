import * as standardCrudPage from 'htmlrapier.widgets/src/StandardCrudPage';
import * as startup from 'Client/Libs/startup';
import * as deepLink from 'htmlrapier/src/deeplink';
import { NoteCrudInjector } from 'Client/Libs/NoteCrudInjector';

var injector = NoteCrudInjector;

var builder = startup.createBuilder();
deepLink.addServices(builder.Services);
standardCrudPage.addServices(builder, injector);
standardCrudPage.createControllers(builder, new standardCrudPage.Settings());