import { Routes } from '@angular/router';
import { WelcomeComponent } from './welcome/welcome.component';
import { ApplyComponent } from './apply/apply.component';
import { ListComponent } from './list/list.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export const routes: Routes = [
    { path: '', component: WelcomeComponent },
    { path: 'welcome', component: WelcomeComponent },
    { path: 'apply', component: ApplyComponent },
    { path: 'list', component: ListComponent },
    { path: '**', component: PageNotFoundComponent },
];
