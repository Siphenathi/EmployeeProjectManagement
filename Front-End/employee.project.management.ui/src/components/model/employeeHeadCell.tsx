import { EmployeeModel } from "./employeeModel";

export type EmployeeHeadCell = {
    id: keyof EmployeeModel;
    label: string;
};
