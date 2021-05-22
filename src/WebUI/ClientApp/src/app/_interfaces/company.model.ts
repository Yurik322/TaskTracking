import {Employee} from './employee.model';

export interface Company {
  id: number;
  name: string;
  fullAddress: string;

  employees?: Employee[];
}
