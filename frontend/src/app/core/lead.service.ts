import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeadDto, LeadStatus, PagedResult, LeadDetailsDto } from './lead.model'; 

@Injectable({ providedIn: 'root' })
export class LeadService {
  private http = inject(HttpClient);
  
  private apiUrl = 'http://localhost:5000/leads'; 

  getLeads(search: string, status: LeadStatus | null, page: number, pageSize: number): Observable<PagedResult<LeadDto>> {
    let params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);

    if (search) params = params.set('search', search);
    if (status !== null) params = params.set('status', status);

    return this.http.get<PagedResult<LeadDto>>(this.apiUrl, { params });
  }

  getLeadById(id: number) {
    return this.http.get<LeadDetailsDto>(`${this.apiUrl}/${id}`);
  }

  createLead(dto: any) {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateLead(id: number, dto: any) {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }

  deleteLead(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}