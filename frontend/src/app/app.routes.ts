import { Routes } from '@angular/router';
import { LeadsComponent } from './pages/leads/leads.component';
import { LeadDetailsComponent } from './pages/lead-detail/lead-detail.component';

export const routes: Routes = [
    { path: '', redirectTo: 'leads', pathMatch: 'full' },
    { path: 'leads', component: LeadsComponent },
    { path: 'leads/:id', component: LeadDetailsComponent},
];
