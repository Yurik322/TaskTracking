export interface Issue {
  id?: number;
  title: string;
  description: string;
  issueType: number;
  priority: number;
  statusType: number;
  createdAt?: string;
  updatedAt?: string;
  creator?: string;
}
