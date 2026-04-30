import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import { LeadService } from '../../core/lead.service';
import { TaskService } from '../../core/task.service';
import { LeadDetailsDto, TaskDto, TaskStatus } from '../../core/lead.model';

@Component({
  selector: 'app-lead-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './lead-detail.component.html',
  styleUrls: ['./lead-detail.component.css']
})
export class LeadDetailsComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private leadService = inject(LeadService);
  private taskService = inject(TaskService);

  loading = false;
  error: string | null = null;

  leadId!: number;

  private reload$ = new BehaviorSubject<void>(undefined);

  lead$!: Observable<LeadDetailsDto | null>;

  TaskStatus = TaskStatus;
  taskStatuses = Object.values(TaskStatus).filter(v => typeof v === 'number');

  ngOnInit() {
    this.leadId = Number(this.route.snapshot.paramMap.get('id'));

    this.lead$ = this.reload$.pipe(
      switchMap(() =>
        this.leadService.getLeadById(this.leadId).pipe(
          catchError((err) => {
            console.error('Error loading lead', err);
            return of(null);
          })
        )
      )
    );
  }

  reload() {
    this.reload$.next();
  }

  createTask() {
    const title = prompt('Task title');
    if (!title) return;

    const dueDate = prompt('Due date (YYYY-MM-DD)');
    if (!dueDate) return;

    this.taskService.createTask(this.leadId, {
      title,
      dueDate,
      status: 0
    }).subscribe({
      next: () => this.reload(),
      error: (err) => console.error('Error creating task', err)
    });
  }

  deleteTask(task: TaskDto) {
    this.taskService.deleteTask(this.leadId, task.id).subscribe({
      next: () => this.reload(),
      error: (err) => console.error('Error deleting task', err)
    });
  }

  updateTaskStatus(task: TaskDto) {
    this.taskService.updateTask(this.leadId, task.id, task).subscribe({
      next: () => this.reload(),
      error: (err) => console.error('Error updating task', err)
    });
  }

  getLeadStatusName(status: number): string {
    const statuses = ['New', 'Qualified', 'Won', 'Lost'];
    return statuses[status] || 'Unknown';
  }

  getTaskStatusName(status: number): string {
    const statuses = ['Todo', 'Doing', 'Done'];
    return statuses[status] || 'Unknown';
  }
}