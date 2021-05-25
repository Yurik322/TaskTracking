import {Employee} from '../../_interfaces/employee.model';

export interface Company {
  id: number;
  name: string;
  fullAddress: string;

  employees?: Employee[];
}
