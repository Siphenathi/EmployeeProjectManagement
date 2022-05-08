import { EmployeeModel } from "./employeeModel";
import { SortingOrder } from "./sortingOrder";

export type EmployeeEnhancedTableProps = {
    numSelected: number;
    onRequestSort: (event: React.MouseEvent<unknown>, property: keyof EmployeeModel) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    order: SortingOrder;
    orderBy: string;
    rowCount: number;
  }