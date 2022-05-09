import { ProjectModel } from "./projectModel";
import { SortingOrder } from "./sortingOrder";

export type ProjectEnhancedTableProps = {
    numSelected: number;
    onRequestSort: (event: React.MouseEvent<unknown>, property: keyof ProjectModel) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    order: SortingOrder;
    orderBy: string;
    rowCount: number;
  }