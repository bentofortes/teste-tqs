export enum LeadStatus {
  New = 0,
  Contacted = 1,
  Qualified = 2,
  Lost = 3
}

export enum TaskStatus {
  Todo = 0,
  Doing = 1,
  Done = 2
}

export interface TaskDto {
  id: number;
  title: string;
  status: TaskStatus;
  dueDate: string
}

export interface LeadDetailsDto {
  id: number;
  name: string;
  email: string;
  status: LeadStatus;
  createdAt: string;
  updatedAt: string;
  tasks: TaskDto[];
}

export interface LeadDto {
  id: number;
  name: string;
  email: string;
  status: LeadStatus;
  createdAt: string;
  updatedAt: string;
  tasksCount: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}