export interface IssueForCreation {
  title: string;
  description: string;
  taskType: number;
  priority: number;
  statusType: number;
  createdAt?: string;
  updatedAt?: string;
}
