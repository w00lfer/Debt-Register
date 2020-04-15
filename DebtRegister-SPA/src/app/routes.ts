import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { SummaryComponent } from './summary/summary.component';
import { LentComponent } from './lent/lent.component';
import { BorrowedComponent } from './borrowed/borrowed.component';
import { ContactsComponent } from './contacts/contacts.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'summary', component: SummaryComponent },
            { path: 'lent', component: LentComponent },
            { path: 'borrowed', component: BorrowedComponent },
            { path: 'contacts', component: ContactsComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
