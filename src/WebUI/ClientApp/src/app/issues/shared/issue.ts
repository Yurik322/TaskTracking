export interface Issue {
  issueId?: number;
  title: string;
  description: string;
  taskType: number;
  priority: number;
  statusType: number;
  createdAt?: string;
  updatedAt?: string;
  projectId?: number;
}
