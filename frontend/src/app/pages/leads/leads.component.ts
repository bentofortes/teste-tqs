import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { switchMap, catchError, finalize } from 'rxjs/operators';

import { LeadService } from '../../core/lead.service';
import { LeadDto, LeadStatus, PagedResult } from '../../core/lead.model';

@Component({
  selector: 'app-leads',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './leads.component.html',
  styleUrls: ['./leads.component.css']
})
export class LeadsComponent implements OnInit {

  private leadService = inject(LeadService);

  private reload$ = new BehaviorSubject<void>(undefined);

  leads$!: Observable<PagedResult<LeadDto> | null>;

  loading = false;
  error?: string;

  searchTerm = '';
  statusFilter: LeadStatus | null = null;
  page = 1;
  pageSize = 10;

  ngOnInit() {
  this.leads$ = this.reload$.pipe(
    switchMap(() =>
      this.leadService
        .getLeads(this.searchTerm, this.statusFilter, this.page, this.pageSize)
        .pipe(
          catchError((err) => {
            console.error('Error fetching leads', err);
            this.error = 'Failed to load leads';
            return of(null);
          })
        )
    )
  );

    this.reload();
  }

  reload() {
    this.reload$.next();
  }


  loadLeads() {
    this.page = 1; 
    this.reload();
  }

  changePage(delta: number) {
    this.page += delta;
    this.reload();
  }

  createLead() {
    const name = prompt('Lead name');
    if (!name) return;

    const email = prompt('Lead email');
    if (!email) return;

    this.leadService.createLead({ name, email })
      .subscribe({
        next: () => this.reload(),
        error: (err) => {
          console.error('Error creating lead', err);
          this.error = 'Failed to create lead';
        }
      });
  }

  editLead(lead: LeadDto) {
  const name = prompt('Edit name', lead.name);
  if (!name) return;

  const email = prompt('Edit email', lead.email);
  if (!email) return;

  this.leadService.updateLead(lead.id, {
    name,
    email
  }).subscribe({
    next: () => this.reload(),
    error: (err) => {
      console.error('Error updating lead', err);
      this.error = 'Failed to update lead';
    }
  });
}

deleteLead(lead: LeadDto) {
  const confirmDelete = confirm(`Delete lead "${lead.name}"?`);
  if (!confirmDelete) return;

  this.leadService.deleteLead(lead.id)
    .subscribe({
      next: () => this.reload(),
      error: (err) => {
        console.error('Error deleting lead', err);
        this.error = 'Failed to delete lead';
      }
    });
}

  getStatusName(status: number): string {
    const statuses = ['New', 'Contacted', 'Qualified', 'Lost'];
    return statuses[status] || 'Unknown';
  }
}