import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private http = inject(HttpClient);
  
  private apiUrl = 'http://localhost:5000'; 

  createTask(leadId: number, dto: any) {
    return this.http.post(`${this.apiUrl}/leads/${leadId}/tasks`, dto);
  }

  updateTask(leadId: number, taskId: number, dto: any) {
    return this.http.put(`${this.apiUrl}/leads/${leadId}/tasks/${taskId}`, dto);
  }

  deleteTask(leadId: number, taskId: number) {
    return this.http.delete(`${this.apiUrl}/leads/${leadId}/tasks/${taskId}`);
  }
}